﻿using Microsoft.EntityFrameworkCore;

namespace DragDrop.Models
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<AccountsModelWithSumBalance> Accounts { get; set; }

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}