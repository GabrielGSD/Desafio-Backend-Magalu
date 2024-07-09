using DesafioMagalu.Database;
using DesafioMagalu.Dtos;
using DesafioMagalu.Enums;
using DesafioMagalu.Exceptions;
using DesafioMagalu.Models;
using DesafioMagalu.Service.NotificationService.Strategies;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioMagalu.Service.NotificationService
{
	public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<NotificationModel> GetNotificationById(int id, bool asNoTracking = false)
        {
            var query = _context.Notifications
                .Include(n => n.Channel)
                .Include(n => n.Status)
                .AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var notification = await query.FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
            {
                throw new NotFoundException("Notification not found");
            }

            return notification;
        }

        public async Task<ScheduleNotificationResponseDto> CreateNotification(ScheduleNotificationDto notification)
        {
		    var newNotification = new NotificationModel
            {
                DateTime = notification.DateTime,
                Destination = notification.Destination,
                Message = notification.Message,
				ChannelId = (int)Enum.Parse(typeof(NotificationChannel), notification.Channel.ToUpper(), true),
				StatusId = NotificationStatus.Pending.ToStatus(),
                JobId = string.Empty
			};

			_context.Notifications.Add(newNotification);
			await _context.SaveChangesAsync();

			string JobId = BackgroundJob.Schedule(() => SendNotification(newNotification.Id), DateTimeOffset.Parse(notification.DateTime.ToString()));

			newNotification.JobId = JobId;
			_context.Notifications.Update(newNotification);
			await _context.SaveChangesAsync();

			var createdNotification = await GetNotificationById(newNotification.Id);

            return new ScheduleNotificationResponseDto
            {
                Id = createdNotification.Id,
                DateTime = createdNotification.DateTime,
                Destination = createdNotification.Destination,
                Message = createdNotification.Message,
                Channel = new ChannelResponseDto
                {
                    Description = createdNotification.Channel.Description
                },
                Status = new StatusResponseDto
                {
                    Description = createdNotification.Status.Description
                }
            };
        }

        public async Task<ScheduleNotificationResponseDto> FindNotificationById(int id)
        {
            var notification = await GetNotificationById(id);

            return new ScheduleNotificationResponseDto
            {
                Id = notification.Id,
                DateTime = notification.DateTime,
                Destination = notification.Destination,
                Message = notification.Message,
                Channel = new ChannelResponseDto
                {
                    Description = notification.Channel.Description
                },
                Status = new StatusResponseDto
                {
                    Description = notification.Status.Description
                }
            };
        }

        public async Task<IActionResult> CancelNotification(int id)
        {
            var notification = await GetNotificationById(id);

            int[] validStatus = { NotificationStatus.Success.ToStatus(), NotificationStatus.Canceled.ToStatus() };

			if (string.IsNullOrEmpty(notification.JobId) &&
                 validStatus.Contains(notification.StatusId))
            {
                throw new ValidationException("Notification cannot be canceled");
            }
			
            BackgroundJob.Delete(notification.JobId);
			notification.StatusId = NotificationStatus.Canceled.ToStatus();

            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

		public void SendNotification(int notificationId)
        {
			var notificationContext = new NotificationContext();
            var notification = _context.Notifications.Find(notificationId);
			try
			{
				switch (notification.ChannelId)
				{
					case (int)NotificationChannel.EMAIL:
						notificationContext.SetStrategy(new EmailNotification());
						break;
					case (int)NotificationChannel.SMS:
						notificationContext.SetStrategy(new SmsNotification());
						break;
					case (int)NotificationChannel.WHATSAPP:
						notificationContext.SetStrategy(new WhatsAppNotification());
						break;
					case (int)NotificationChannel.PUSH:
						notificationContext.SetStrategy(new PushNotification());
						break;
					default:
						throw new ValidationException("Invalid channel");
				}
				notificationContext.SendNotification(notification);
				notification.StatusId = NotificationStatus.Success.ToStatus();
			}
			catch
			{
				notification.StatusId = NotificationStatus.Error.ToStatus();
				throw;
			}
			finally
			{
				if (notification.JobId == null)
				{
					notification.JobId = ""; 
				}
				_context.Notifications.Update(notification);
				_context.SaveChanges();
			}
		}
	}
}
