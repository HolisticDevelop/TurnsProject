using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Support.Data
{
    public class SupportContext : DbContext
    {
        public SupportContext (DbContextOptions<SupportContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Turns)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .IsRequired();
        }


        public DbSet<Support.Models.User> User { get; set; } = default!;

        public DbSet<Support.Models.Turn> Turn { get; set; } = default!;
    }
}
