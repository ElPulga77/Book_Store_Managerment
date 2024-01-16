using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Book_Store_Memoir.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Shipper> Shipper { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<DeliveryReceipt> DeliveryReceipts { get; set; }
        public DbSet<ReceiptDetails> ReceiptDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
            modelBuilder.Entity<Orders>()
               .HasOne(ba => ba.OrderStatus)
               .WithMany(a => a.Orders)
               .HasForeignKey(ba => ba.OrderStatusId);
            modelBuilder.Entity<Orders>()
              .HasOne(ba => ba.Shipper)
              .WithMany(a => a.Orders)
              .HasForeignKey(ba => ba.ShipperId);
            modelBuilder.Entity<DeliveryReceipt>()
                 .HasOne(ba => ba.Orders)
                 .WithMany(a => a.DeliveryReceipts)
                 .HasForeignKey(ba => ba.OrderId);
            modelBuilder.Entity<DeliveryReceipt>()
                .HasOne(ba => ba.Shipper)
                .WithMany(a => a.DeliveryReceipts)
                .HasForeignKey(ba => ba.ShipperId);
            modelBuilder.Entity<Orders>()
               .HasOne(ba => ba.Coupon)
               .WithMany(a => a.Orders)
               .HasForeignKey(ba => ba.CouponId);
            modelBuilder.Entity<Book>()
               .HasMany(ba => ba.Reviews)        // Mỗi sản phẩm có nhiều đánh giá
               .WithOne(a => a.Book)        // Mỗi đánh giá thuộc về một sản phẩm
               .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<Customers>()
               .HasMany(ba => ba.Reviews)        // Mỗi sản phẩm có nhiều đánh giá
               .WithOne(a => a.Customers)        // Mỗi đánh giá thuộc về một người dùng
               .HasForeignKey(ba => ba.CustomerId);
            /*modelBuilder.Entity<ReceiptDetails>()
            .HasKey(dr => new { dr.DeliveryReceiptId, dr.BookId });

            modelBuilder.Entity<ReceiptDetails>()
                .HasOne(dr => dr.Book)
                .WithMany(d => d.ReceiptDetails)
                .HasForeignKey(dr => dr.BookId);

            modelBuilder.Entity<ReceiptDetails>()
                .HasOne(dr => dr.DeliveryReceipt)
                .WithMany(b => b.ReceiptDetails)
                .HasForeignKey(dr => dr.DeliveryReceiptId);*/
        }
    }
}
