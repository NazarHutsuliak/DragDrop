using Microsoft.EntityFrameworkCore;

namespace DragDrop.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AccountsModelWithSumBalance> Accounts { get; set; }
    }
}