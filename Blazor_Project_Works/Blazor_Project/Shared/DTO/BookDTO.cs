using Blazor_Project.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_Project.Shared.DTO
{
    public class BookDTO
    {
        public int BookID { get; set; }
        [Required, StringLength(30), Display(Name = "Book Name")]
        public string BookName { get; set; } = default!;
        [Required, DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Publish Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; } = DateTime.Today;
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool Available { get; set; }
        [Required, ForeignKey(nameof(Publisher))]
        public int PublisherID { get; set; }
        public string PublisherName { get; set; } = default!;
    }
}
