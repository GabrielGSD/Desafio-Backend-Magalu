using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService.Strategies
{
	public class SmsNotification : INotificationStrategy
	{
		public void SendNotification(NotificationModel notification)
		{
			Console.WriteLine($"Sending sms to {notification.Destination} with message: {notification.Message}");
		}
	}
}
