using DesafioMagalu.Dtos;
using DesafioMagalu.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMagalu.Service.NotificationService
{
	public interface INotificationService
	{
		Task<ScheduleNotificationResponseDto> CreateNotification(ScheduleNotificationDto notification);
		Task<ScheduleNotificationResponseDto> FindNotificationById(int id);
		Task<IActionResult> CancelNotification(int id);
	}
}
