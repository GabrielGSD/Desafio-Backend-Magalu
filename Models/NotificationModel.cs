namespace DesafioMagalu.Models
{
	public class NotificationModel
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public string Destination { get; set; }
		public string Message { get; set; }
		public int ChannelId { get; set; }
		public ChannelModel Channel { get; set; }
		public int StatusId { get; set; }
		public StatusModel Status { get; set; }
	}
}
