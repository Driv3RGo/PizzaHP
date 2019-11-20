namespace PizzaHP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PizzaContext : DbContext
    {
        public PizzaContext()
            : base("name=PizzaContext")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Composition> Composition { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_line> Order_line { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.Client_FK);

            modelBuilder.Entity<Ingredient>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Ingredient>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Ingredient>()
                .HasMany(e => e.Composition)
                .WithRequired(e => e.Ingredient)
                .HasForeignKey(e => e.Ingredient_FK);

            modelBuilder.Entity<Kategori>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Ingredient)
                .WithRequired(e => e.Kategori)
                .HasForeignKey(e => e.Kategori_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Skidka)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_line)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Order_FK);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Composition)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_FK);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_line)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.Staff)
                .HasForeignKey(e => e.Staff_FK);

            modelBuilder.Entity<Status>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.Status_FK)
                .WillCascadeOnDelete(false);
        }
    }
}
