using DesafioMagalu.Exceptions;

namespace DesafioMagalu.Enums
{
	public class NotificationChannel
	{
		public int Id { get; private set; }
		public string Name { get; private set; }

		private NotificationChannel(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public static readonly NotificationChannel Email = new NotificationChannel(1, "email");
		public static readonly NotificationChannel SMS = new NotificationChannel(2, "sms");
		public static readonly NotificationChannel Push = new NotificationChannel(3, "push");
		public static readonly NotificationChannel WhatsApp = new NotificationChannel(4, "whatsapp");

		public static IEnumerable<NotificationChannel> List()
		{
			return new[] { Email, SMS, Push, WhatsApp };
		}

		public static NotificationChannel FromId(int id)
		{
			return List().SingleOrDefault(r => r.Id == id);
		}

		public static int FromName(string name)
		{
			var channel = List().SingleOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (channel == null)
			{
				throw new BadRequestException($"Invalid channel name: {name}.");
			}
			return channel.Id;
		}
	}
}
