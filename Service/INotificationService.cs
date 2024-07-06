
using DesafioMagalu.Dtos;

namespace DesafioMagalu.Service
{
	public interface INotificationService
	{
		Task CreateNotification(ScheduleNotificationDto notification);
	}
}
