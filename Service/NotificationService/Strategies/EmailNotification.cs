using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService.Strategies
{
	public class EmailNotification : INotificationStrategy
	{
		public void SendNotification(NotificationModel notification)
		{
			Console.WriteLine($"Sending email to {notification.Destination} with message: {notification.Message}");
		}
	}
}