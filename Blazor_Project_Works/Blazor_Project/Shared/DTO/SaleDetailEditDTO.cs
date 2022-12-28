using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_Project.Shared.DTO
{
    public class SaleDetailEditDTO
    {
        [Key]
        public int SaleID { get; set; }

        [Required]
        public int BookID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
