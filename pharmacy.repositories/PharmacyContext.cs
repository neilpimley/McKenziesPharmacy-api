using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pharmacy.Models;

namespace Pharmacy.Repositories
{
    public partial class PharmacyContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<CollectScript> CollectScript { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Drug> Drug { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Favourite> Favourite { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderLine> OrderLine { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Practice> Practice { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }
        public virtual DbSet<ReminderOrder> ReminderOrder { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Title> Title { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId)
                    .HasColumnName("AddressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLine1).IsRequired();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Postcode).IsRequired();
            });

            modelBuilder.Entity<CollectScript>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasIndex(e => e.DoctorId)
                    .HasName("IX_DoctorID");

                entity.HasIndex(e => e.ShopId)
                    .HasName("IX_ShopID");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Customer).IsRequired();

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.CollectScript)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_dbo.CollectScript_dbo.Doctor_DoctorID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.CollectScript)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_dbo.CollectScript_dbo.Shop_ShopID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Firstname).IsRequired();

                entity.Property(e => e.Lastname).IsRequired();

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Sex).IsRequired();

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_dbo.Customer_dbo.Address_AddressID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Customer_dbo.Doctor_DoctorID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_dbo.Customer_dbo.Shop_ShopID");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Customer_dbo.Title_TitleID");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(e => e.PracticeId)
                    .HasName("IX_PracticeID");

                entity.HasIndex(e => e.TitleId)
                    .HasName("IX_TitleID");

                entity.Property(e => e.DoctorId)
                    .HasColumnName("DoctorID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Firstname).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PracticeId).HasColumnName("PracticeID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.HasOne(d => d.Practice)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.PracticeId)
                    .HasConstraintName("FK_dbo.Doctor_dbo.Practice_PracticeID");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Doctor_dbo.Title_TitleID");
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.Property(e => e.DrugId)
                    .HasColumnName("DrugID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DrugDose).HasMaxLength(256);

                entity.Property(e => e.DrugName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerID");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CcAddress).HasMaxLength(128);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FromAddress).HasMaxLength(128);

                entity.Property(e => e.Subject).HasMaxLength(256);

                entity.Property(e => e.ToAddress).HasMaxLength(128);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.EmailNavigation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Email_dbo.Customer_CustomerID");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerID");

                entity.HasIndex(e => e.DrugId)
                    .HasName("IX_DrugID");

                entity.Property(e => e.FavouriteId)
                    .HasColumnName("FavouriteID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DrugId).HasColumnName("DrugID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Favourite_dbo.Customer_CustomerID");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_dbo.Favourite_dbo.Drug_DrugID");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.NoteId)
                    .HasColumnName("NoteID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerID");

                entity.HasIndex(e => e.NoteId)
                    .HasName("IX_NoteID");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Order_dbo.Customer_CustomerID");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("FK_dbo.Order_dbo.Note_NoteID");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasIndex(e => e.DrugId)
                    .HasName("IX_DrugID");

                entity.HasIndex(e => e.OrderId)
                    .HasName("IX_OrderID");

                entity.Property(e => e.OrderLineId)
                    .HasColumnName("OrderLineID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DrugId).HasColumnName("DrugID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_dbo.OrderLine_dbo.Drug_DrugID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_dbo.OrderLine_dbo.Order_OrderID");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasIndex(e => e.OrderId)
                    .HasName("IX_OrderID");

                entity.Property(e => e.OrderStatusId)
                    .HasColumnName("OrderStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.StatusSetDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatusNavigation)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_dbo.OrderStatus_dbo.Order_OrderID");
            });

            modelBuilder.Entity<Practice>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("IX_AddressID");

                entity.Property(e => e.PracticeId)
                    .HasColumnName("PracticeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(128);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PracticeName).HasMaxLength(256);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Practice)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Practice_dbo.Address_AddressID");
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.Property(e => e.ReminderId)
                    .HasColumnName("ReminderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.Sent).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ReminderOrder>(entity =>
            {
                entity.Property(e => e.ReminderOrderId)
                    .HasColumnName("ReminderOrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ReminderId).HasColumnName("ReminderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ReminderOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dbo.ReminderOrder__dbo.Reminder_OrderID");

                entity.HasOne(d => d.Reminder)
                    .WithMany(p => p.ReminderOrder)
                    .HasForeignKey(d => d.ReminderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReminderO__Remin__3E52440B");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("IX_AddressID");

                entity.Property(e => e.ShopId)
                    .HasColumnName("ShopID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Shop)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Shop_dbo.Address_AddressID");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.TitleId)
                    .HasColumnName("TitleID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });
        }
    }
}
