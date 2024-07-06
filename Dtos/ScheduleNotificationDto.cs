namespace DesafioMagalu.Dtos
{
	public class ScheduleNotificationDto
	{
		public DateTime DateTime { get; set; }
		public string Destination { get; set; }
		public string Message { get; set; }
		public int ChannelId { get; set; }
	}
}
