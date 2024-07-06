namespace DesafioMagalu.Models
{
	public class ChannelModel
	{
		public int Id { get; set; }
		public string Description { get; set; } // Email, SMS, Push, WhatsApp
		public List<NotificationModel> Notifications { get; set; }
	}
}
