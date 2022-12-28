using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_Project.Shared.DTO
{
    public class PublisherDTO
    {
        [Key]
        public int PublisherID { get; set; }
        [Required, StringLength(30), Display(Name = "Publisher Name")]
        public string PublisherName { get; set; } = default!;
        public bool CanDelete { get; set; }
    }
}
