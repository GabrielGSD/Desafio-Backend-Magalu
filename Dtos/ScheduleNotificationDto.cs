namespace DesafioMagalu.Dtos
{
	public class ScheduleNotificationDto
	{
		public DateTime DateTime { get; set; }
		public string Destination { get; set; }
		public string Message { get; set; }
		public string Channel { get; set; }
	}

	public class ScheduleNotificationResponseDto
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public string Destination { get; set; }
		public string Message { get; set; }
		public ChannelResponseDto Channel { get; set; }
		public StatusResponseDto Status { get; set; }
	}
}
