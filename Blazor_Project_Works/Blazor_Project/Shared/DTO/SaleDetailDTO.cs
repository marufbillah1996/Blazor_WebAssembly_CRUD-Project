using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_Project.Shared.DTO
{
    public class SaleDetailDTO
    {
        [ForeignKey("Sale")]
        public int SaleID { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
