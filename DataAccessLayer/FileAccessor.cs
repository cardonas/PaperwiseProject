using ObjectLayer;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer
{
    public class FileAccessor : IFileAccessor
    {
        public List<FileProperties> GetFileInfos(string[] filePaths)
        {
            List<FileProperties> fileProps = new List<FileProperties>();
            foreach (string file in filePaths)
            {
                FileInfo fs = new FileInfo(file);
                FileProperties fprops = new FileProperties()
                {
                    Name = fs.Name,
                    Length = fs.Length,
                    Extension = fs.Extension,
                    CreationTime = fs.CreationTime,
                };
                fileProps.Add(fprops);
            }
            return fileProps;
        }

        public void WriteFileInfoCSV(string filePath, List<FileProperties> fileInfos, StreamWriter writer = null)
        {
            try
            {
                if (writer == null)
                {
                    writer = new StreamWriter(filePath, true);
                }

                using (var sw = writer)
                {
                    sw.WriteLine("Name,Length,Extension,Creation Time");
                    foreach (var file in fileInfos)
                    {
                        string csvLine = $"{file.Name},{file.Length},{file.Extension},{file.CreationTime}";
                        sw.WriteLine(csvLine);
                    }
                }
            }
            catch
            {
                throw new FileNotFoundException($"Path Not Found: {filePath}");
            };
        }
    }
}
