using Domain.DEMODATA;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace Domain.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Coordinate> Coordinates { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }


    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<SubCategory> SubCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.ToTable("Availability");

            entity.Property(e => e.CloseTime).HasPrecision(0);
            entity.Property(e => e.OpenTime).HasPrecision(0);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TimeSlot)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Studio).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StudioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Studio");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Coordinate>(entity =>
        {
            entity.Property(e => e.Latitude).HasColumnType("numeric(10, 8)");
            entity.Property(e => e.Longitude).HasColumnType("numeric(11, 8)");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Coordinates).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CoordinatesId)
                .HasConstraintName("FK_Location_Coordinates");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.ToTable("Studio");

            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PricePerHour).HasColumnType("numeric(10, 2)");
            entity.Property(e => e.Rating).HasColumnType("numeric(2, 1)");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Availability).WithMany(p => p.Studios)
                .HasForeignKey(d => d.AvailabilityId)
                .HasConstraintName("FK_Studio_Availability");

            entity.HasOne(d => d.Contact).WithMany(p => p.Studios)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_Studio_Contact");

            entity.HasOne(d => d.Location).WithMany(p => p.Studios)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Studio_Location");
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(100).IsUnicode(false);
        });
        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryName).HasMaxLength(100).IsUnicode(false);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");
        });


    }
}
