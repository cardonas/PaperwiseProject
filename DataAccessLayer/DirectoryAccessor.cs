using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DirectoryAccessor : IDirectoryAccessor
    {
        public string[] GetFiles(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            return files;
        }
    }
}
