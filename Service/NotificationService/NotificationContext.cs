using DesafioMagalu.Models;

namespace DesafioMagalu.Service.NotificationService
{
	public class NotificationContext
	{
		private INotificationStrategy _strategy;

		public void SetStrategy(INotificationStrategy strategy)
		{
			_strategy = strategy;
		}

		public void SendNotification(NotificationModel notification)
		{
			_strategy.SendNotification(notification);
		}
	}
}
