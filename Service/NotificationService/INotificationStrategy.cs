using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService
{
	public interface INotificationStrategy
	{
		void SendNotification(NotificationModel notification);
	}
}
