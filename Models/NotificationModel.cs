using System.Text.Json.Serialization;

namespace DesafioMagalu.Models
{
	public class NotificationModel
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public string Destination { get; set; }
		public string Message { get; set; }
		public int ChannelId { get; set; }
		public int StatusId { get; set; }
		
		[JsonIgnore]
		public ChannelModel Channel { get; set; }

		[JsonIgnore]
		public StatusModel Status { get; set; }
	}
}
