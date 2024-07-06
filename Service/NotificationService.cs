using DesafioMagalu.Database;
using DesafioMagalu.Dtos;
using DesafioMagalu.Enums;
using DesafioMagalu.Exceptions;
using DesafioMagalu.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DesafioMagalu.Service
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
				ChannelId = NotificationChannel.FromName(notification.Channel),
				StatusId = NotificationStatus.Pending.ToStatus()
			};

			_context.Notifications.Add(newNotification);
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
			notification.StatusId = NotificationStatus.Canceled.ToStatus();

			_context.Notifications.Update(notification);
			await _context.SaveChangesAsync();

			return new NoContentResult();
		}
	}
}
