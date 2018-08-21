using Microsoft.EntityFrameworkCore;
using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRich.Entities
{
    public class Context : DbContext
    {
        public Context() {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Networks>().HasOne(n => n.SocialNetworks).WithMany().HasForeignKey(n => n.SNId).HasConstraintName("FK_SocialNetwok");
            //modelBuilder.Entity<Networks>().HasOne(n => n.Users).WithMany().HasForeignKey(n => n.UserId).HasConstraintName("FK_User");
            // modelBuilder.Entity<Users>().HasOne(s => s.SocialNetwork).WithOne(s => s.User).HasForeignKey<SocialNetwork>(u => u.Id);
            //modelBuilder.Entity<SocialNetwork>().HasOne(s => s.User).WithOne(s => s.SocialNetwork).HasForeignKey<Users>(u => u.FavouriteNetwork);
            modelBuilder.Entity<Users>().HasOne(u => u.SocialNetwork).WithOne();
            modelBuilder.Entity<SocialNetwork>().HasKey(s => new { s.Id });
            modelBuilder.Entity<Networks>().HasKey(n => new { n.Id });
            modelBuilder.Entity<Users>().HasKey(u => new { u.Id });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=APENAS-LPT\\SQLEXPRESS;Database=PrimeraPrueba;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<SocialNetwork> SocialNetwork { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Networks> Networks { get; set; }
    }
}
