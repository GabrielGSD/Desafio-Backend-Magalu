namespace DesafioMagalu.Enums
{
	public enum NotificationStatus
	{
		Pending = 1,
        Success = 2,
        Error = 3,
        Canceled = 4
	}

	public static class EnumExtensions
	{
		public static int ToStatus(this NotificationStatus status)
		{
			return (int)status;
		}
	}
}
