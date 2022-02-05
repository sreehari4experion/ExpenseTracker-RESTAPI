using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseTracker.Models
{
    public partial class ExpenseTrackerDBContext : DbContext
    {
        public ExpenseTrackerDBContext()
        {
        }

        public ExpenseTrackerDBContext(DbContextOptions<ExpenseTrackerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemsDescription> ItemsDescription { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SREEHARIPRATHAP\\SQLEXPRESS;Database=ExpenseTrackerDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category1)
                    .HasColumnName("category")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.HasKey(e => e.ExpId)
                    .HasName("PK__Expenses__F2AC96B007337E9A");

                entity.HasIndex(e => e.ItemsId)
                    .HasName("UQ__Expenses__AD73E24BAC73E70D")
                    .IsUnique();

                entity.Property(e => e.ExpId)
                    .HasColumnName("expID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.ExpenseAmount).HasColumnName("expenseAmount");

                entity.Property(e => e.ItemsId).HasColumnName("itemsID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Expenses__catego__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Expenses__userID__403A8C7D");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.ItemId)
                    .HasColumnName("itemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemName)
                    .HasColumnName("itemName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ItemPrice).HasColumnName("itemPrice");
            });

            modelBuilder.Entity<ItemsDescription>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.ItemsId).HasColumnName("itemsID");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Login__66DCF95DCF91D24F");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__CB9A1CDFBF78C29C");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Users__userName__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
