using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pract03._16.ModelsBD;

public partial class AeroFlotContext : DbContext
{
    public AeroFlotContext()
    {
    }

    public AeroFlotContext(DbContextOptions<AeroFlotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=AeroFlot; User=исп-31; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("flights");

            entity.Property(e => e.ArrivalTime)
                .HasMaxLength(50)
                .HasColumnName("arrival_time");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CountFreePlaces).HasColumnName("count_free_places");
            entity.Property(e => e.DepartureTime)
                .HasMaxLength(50)
                .HasColumnName("departure_time");
            entity.Property(e => e.Destination)
                .HasMaxLength(250)
                .HasColumnName("destination");
            entity.Property(e => e.FlightId).HasColumnName("flight_ID");
            entity.Property(e => e.TypePlane)
                .HasMaxLength(50)
                .HasColumnName("type_plane");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
