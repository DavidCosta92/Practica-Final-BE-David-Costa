using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectBakary.Persistence.Entities;

namespace FinalProjectBakary.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Office> Offices { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Bread> Breads { get; set; }
        public DbSet<Baguette> Baguettes { get; set; }  
        public DbSet<HamburgerBun> HamburgerBuns { get; set; } 
        public DbSet<MilkBread> MilkBreads { get; set; }  
        public DbSet<WhiteBread> WhiteBreads { get; set; } 
        public ApplicationDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Office
            modelBuilder.Entity<Office>()
                .HasOne(o => o.Menu)
                .WithMany()
                .HasForeignKey(o => o.MenuId);
            modelBuilder.Entity<Office>()
                .OwnsOne(o => o.Audit);

            // Menu
            modelBuilder.Entity<Menu>()
                .OwnsOne(m => m.Audit);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.AvailableBreads)
                .WithMany()
                .UsingEntity(j => j.ToTable("MenuBreads"));
           
            // Order
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Audit);

            // Bread
            modelBuilder.Entity<Bread>()
                .OwnsOne(b => b.Audit, audit =>
                {
                    audit.Property(a => a.CreatedAt).IsRequired();
                    audit.Property(a => a.ModifiedAt).IsRequired();
                });

            // OrderBread (many to many)
            modelBuilder.Entity<OrderBread>()
                .HasKey(ob => new { ob.OrderId, ob.BreadId });

            modelBuilder.Entity<OrderBread>()
                .HasOne(ob => ob.Order)
                .WithMany(o => o.OrderBreads)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<OrderBread>()
                .HasOne(ob => ob.Bread)
                .WithMany()
                .HasForeignKey(ob => ob.BreadId);
        }

    }
}
