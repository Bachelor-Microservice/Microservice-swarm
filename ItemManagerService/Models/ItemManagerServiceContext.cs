using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ItemManagerService.Models
{
    public partial class ItemManagerServiceContext : DbContext
    {
        public ItemManagerServiceContext()
        {
        }

        public ItemManagerServiceContext(DbContextOptions<ItemManagerServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Items> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=docker-testing.database.windows.net;Initial Catalog=ItemManagerService;User ID=skakkristiansen;Password=Bjørnemosevej241;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CatalogNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionTo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ItemFee)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PriceModel)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PriceModelFrom)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Switches)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tags)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TicketForm)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TicketType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
