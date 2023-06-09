﻿using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TmpBalance> TmpBalances { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        //Required to create a migration without considering the Wallets table
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Wallet>()
        //        .ToTable("Wallets", t => t.ExcludeFromMigrations());
        //}
    }
}
