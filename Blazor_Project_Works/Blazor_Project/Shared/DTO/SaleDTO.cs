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
    public class SaleDTO
    {
        [Key]
        public int OrderID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Sale Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime SaleDate { get; set; } = DateTime.Today;
        
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; } = Status.NotPaid;
        [Required]
        public int CustomerID { get; set; }

        public virtual ICollection<SaleDetailDTO> SaleDetails { get; set; } = new List<SaleDetailDTO>();
    }
}
