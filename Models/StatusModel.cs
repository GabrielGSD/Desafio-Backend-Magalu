namespace DesafioMagalu.Models
{
	public class StatusModel
	{
		public int Id { get; set; }
		public string Description { get; set; } // Sent, Received, Read
		public List<NotificationModel> Notifications { get; set; }
	}
}
