using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLayer
{
    public class FileProperties
    {
        public string Name { get; set; }
        public byte Length { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
