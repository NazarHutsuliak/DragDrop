using Microsoft.EntityFrameworkCore;

namespace DragDrop.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<AccountsModelWithSumBalance> Accounts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountsModelWithSumBalance>((pc =>
            {
                pc.HasNoKey();
            }));
        }
    }
}