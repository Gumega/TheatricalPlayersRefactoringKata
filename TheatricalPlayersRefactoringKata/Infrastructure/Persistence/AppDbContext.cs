using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<StatementEntity> Statements => Set<StatementEntity>();
		public DbSet<StatementItemEntity> StatementItems => Set<StatementItemEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StatementEntity>()
				.HasMany(s => s.Items)
				.WithOne()
				.HasForeignKey(i => i.StatementId);
		}
	}
}
