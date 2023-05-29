using System;
using System.Diagnostics.CodeAnalysis;

namespace ObjectLayer
{
    [ExcludeFromCodeCoverage]
    public class FileProperties
    {
        public string Name { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
