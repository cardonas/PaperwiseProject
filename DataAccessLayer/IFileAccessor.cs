using ObjectLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IFileAccessor
    {
        List<FileProperties> GetFileInfos(string[] filePaths);

        void WriteFileInfoCSV(string filePath, List<FileProperties> fileInfos, StreamWriter writer = null);
    }
}
