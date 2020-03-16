using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockScore.Models;

namespace StockScore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
        }

        public DbSet<Favorites> Favorites { get; set; }

        public DbSet<Searches> Searches { get; set; }

        public DbSet<Top_Stocks> Top_Stocks { get; set; }

        public DbSet<User_Stocks> User_Stocks { get; set; }
    }
}
