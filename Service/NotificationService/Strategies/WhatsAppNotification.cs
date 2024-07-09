using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService.Strategies
{
	public class WhatsAppNotification : INotificationStrategy
	{
		public void SendNotification(NotificationModel notification)
		{
			Console.WriteLine($"Sending whatsapp to {notification.Destination} with message: {notification.Message}");
		}
	}
}
