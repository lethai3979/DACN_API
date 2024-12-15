﻿using GoWheels_WebAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoWheels_WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Amenity> Amentities { get; set; } = null!;
        public DbSet<CarType> CarTypes { get; set; } = null!;
        public DbSet<CarTypeDetail> CarTypeDetails { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<PostPromotion> PostPromotions { get; set; } = null!;
        public DbSet<PostAmenity> PostAmenities { get; set; } = null!;
        public DbSet<Favorite> Favorites { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<PostImage> PostImages { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<ReportType> ReportTypes { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;
        public DbSet<Notify> Notifications { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Driver)
                .WithOne(p => p.User)
                .HasForeignKey<Driver>(p => p.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
