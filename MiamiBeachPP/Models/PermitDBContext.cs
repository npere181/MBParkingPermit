using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MiamiBeachPP.Models;

namespace MiamiBeachPP.Models
{
    public partial class PermitDBContext : DbContext
    {
        public virtual DbSet<ParkingAreas> ParkingAreas { get; set; }
        public virtual DbSet<ParkingAreaTypes> ParkingAreaTypes { get; set; }
        public virtual DbSet<ParkingPermits> ParkingPermits { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-G6G37B1\SQLEXPRESS;Initial Catalog=PermitDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingAreas>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParkingAreaName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ParkingAreaType)
                    .WithMany(p => p.ParkingAreas)
                    .HasForeignKey(d => d.ParkingAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParkingAreas_ParkingAreaTypes");
            });

            modelBuilder.Entity<ParkingAreaTypes>(entity =>
            {
                entity.Property(e => e.ParkingAreaTypeDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ParkingPermits>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ParkingArea)
                    .WithMany(p => p.ParkingPermits)
                    .HasForeignKey(d => d.ParkingAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParkingPermits_ParkingAreas");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("sysUser");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }

        public DbSet<MiamiBeachPP.Models.SpModel> SpModel { get; set; }
    }
}
