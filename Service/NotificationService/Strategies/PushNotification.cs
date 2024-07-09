using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService.Strategies
{
	public class PushNotification : INotificationStrategy
	{
		public void SendNotification(NotificationModel notification)
		{
			Console.WriteLine($"Sending push to {notification.Destination} with message: {notification.Message}");
		}
	}
}