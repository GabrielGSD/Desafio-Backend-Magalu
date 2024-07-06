using DesafioMagalu.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioMagalu.Database
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

		public DbSet<Models.ChannelModel> Channels { get; set; }
		public DbSet<Models.StatusModel> Status { get; set; }
		public DbSet<Models.NotificationModel> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ChannelModel>().HasData(
				new Models.ChannelModel { Id = 1, Description = "email" },
				new Models.ChannelModel { Id = 2, Description = "sms" },
				new Models.ChannelModel { Id = 3, Description = "push" },
				new Models.ChannelModel { Id = 4, Description = "whatsapp" }
			);

			modelBuilder.Entity<StatusModel>().HasData(
				new Models.StatusModel { Id = 1, Description = "pending" },
				new Models.StatusModel { Id = 2, Description = "success" },
				new Models.StatusModel { Id = 3, Description = "error" },
				new Models.StatusModel { Id = 4, Description = "canceled" }
			);
		}
	}
}
