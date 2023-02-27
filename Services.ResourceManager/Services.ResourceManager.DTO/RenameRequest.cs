using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResourceManager.DTO
{
    public class RenameRequest
    {
        public string? Path { get; set; }
        public string? NewPath { get; set; }
    }
}
