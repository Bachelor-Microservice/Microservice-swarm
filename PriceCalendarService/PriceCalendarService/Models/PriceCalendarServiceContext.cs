using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PriceCalendarService.Models
{
    public partial class PriceCalendarServiceContext : DbContext
    {
        public PriceCalendarServiceContext()
        {
        }

        public PriceCalendarServiceContext(DbContextOptions<PriceCalendarServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemDay> ItemDay { get; set; }
        public virtual DbSet<ItemPriceAndCurrencyResponse> ItemPriceAndCurrencyResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=docker-testing.database.windows.net;Initial Catalog=PriceCalendarService;User ID=skakkristiansen;Password=Bjørnemosevej241;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GroupsToItemPriceAndCurrencyResponse");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ItemToGroups");
            });

            modelBuilder.Entity<ItemDay>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CustomerDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PricePackage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemDay)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ItemDayToItem");
            });

            modelBuilder.Entity<ItemPriceAndCurrencyResponse>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Currency)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
