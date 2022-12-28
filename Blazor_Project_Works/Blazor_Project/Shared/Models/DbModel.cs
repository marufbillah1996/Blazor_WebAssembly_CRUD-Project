using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Blazor_Project.Shared.Models
{
    public enum Status { Paid = 1, NotPaid, Due }
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required, StringLength(30), Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = default!;
        [Required, StringLength(150)]
        public string Address { get; set; } = default!;
        [Required, StringLength(30), Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; } = default!;
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = default!;
        
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    

    public class Sale
    {
        public int SaleID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Sale Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime SaleDate { get; set; }
        
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; } = default!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    }

    public class SaleDetail
    {
        [ForeignKey("Sale")]
        public int SaleID { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Sale Sale { get; set; } = default!;
        public virtual Book Book { get; set; } = default!;

    }
    public class Publisher
    {
        public int PublisherID { get; set; }
        [Required, StringLength(30), Display(Name = "Publisher Name")]
        public string PublisherName { get; set; } = default!;
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
    public class Book
    {
        public int BookID { get; set; }
        [Required, StringLength(30), Display(Name = "Book Name")]
        public string BookName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"),
            Display(Name = "Publish Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool Available { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public virtual Publisher? Publisher { get; set; } = default!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Sale> Sales { get; set; } = default!;
        public DbSet<SaleDetail> SaleDetails { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleDetail>().HasKey(ba => new { ba.SaleID, ba.BookID });
        }

    }
}
