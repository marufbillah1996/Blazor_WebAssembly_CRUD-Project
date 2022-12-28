using Blazor_Project.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_Project.Shared.DTO
{
    public class BookInput
    {
        [Key]
        public int BookId { get; set; }
        [Required, StringLength(50)]
        public string BookName { get; set; } = default!;
        [Required, Column(TypeName = "date"),
         Display(Name = "Publish Date"),
             DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
             ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; } = DateTime.Today;
        [Required, DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool Available { get; set; }
        [Required]
        public int PublisherID { get; set; }
        public Publisher? Publisher { get; set; } = default!;
    }
}
