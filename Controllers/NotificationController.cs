using DesafioMagalu.Dtos;
using DesafioMagalu.Exceptions;
using DesafioMagalu.Service.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMagalu.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateNotification([FromBody] ScheduleNotificationDto notification)
		{
			try
			{
				var result = await _notificationService.CreateNotification(notification);
				return CreatedAtAction(nameof(CreateNotification), new { id = result.Id }, result);
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (ValidationException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(500, new { message = "An unexpected error occurred." });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetNotification([FromQuery] int notificationId)
		{
			try
			{
				var result = await _notificationService.FindNotificationById(notificationId);
				return Ok(result);
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (ValidationException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(500, new { message = "An unexpected error occurred." });
			}
		}

		[HttpDelete]
		public async Task<IActionResult> CancelNotification([FromQuery] int notificationId)
		{
			try
			{
				return await _notificationService.CancelNotification(notificationId);
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (ValidationException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(500, new { message = "An unexpected error occurred." });
			}
		}

	}
}
