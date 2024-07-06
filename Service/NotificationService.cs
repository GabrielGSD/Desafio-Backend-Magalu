using DesafioMagalu.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMagalu.Service
{
	public class NotificationService : INotificationService
	{
		public Task CreateNotification(ScheduleNotificationDto notification)
		{
			Console.WriteLine("Criando notificação");
			return Task.CompletedTask;
		}
	}
}
