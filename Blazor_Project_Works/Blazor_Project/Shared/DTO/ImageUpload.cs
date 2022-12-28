using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_Project.Shared.DTO
{
    public class ImageUpload
    {
        public string FileName { get; set; } = default!;
        public string StoredFileName { get; set; } = default!;
    }
}
