using DesafioMagalu.Dtos;
using DesafioMagalu.Service;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMagalu.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationInterface;
		public NotificationController(INotificationService notificationInterface)
		{
			_notificationInterface = notificationInterface;
		}

		[HttpPost]
		public async Task<IActionResult> CreateNotification([FromBody] ScheduleNotificationDto notification)
		{
			await _notificationInterface.CreateNotification(notification);
			return Ok();
		}
	}
}
