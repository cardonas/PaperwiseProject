using DataAccessLayer;
using ObjectLayer;
using System.Collections.Generic;
using System.IO;

namespace LogicLayer
{
    public class FileManager
    {
        private readonly IDirectoryAccessor directoryAccessor;
        private readonly IFileAccessor fileAccessor;
        private string[] files;
        public List<FileProperties> fileProperties;

        public FileManager()
        {
            this.directoryAccessor = new DirectoryAccessor();
            this.fileAccessor = new FileAccessor();
        }

        public FileManager(IDirectoryAccessor directoryAccessor)
        {
            this.directoryAccessor = directoryAccessor;
            this.fileAccessor = new FileAccessor();
        }

        public void GetFileInfos()
        {
            this.fileProperties = this.fileAccessor.GetFileInfos(this.files);
        }

        public void GetFiles(string directoryPath)
        {
            this.files = this.directoryAccessor.GetFiles(directoryPath);
        }

        public void WriteCSV(string outputDir)
        {
            try
            {
                this.fileAccessor.WriteFileInfoCSV(outputDir, this.fileProperties);
            }
            catch (FileNotFoundException fnfe)
            {
                throw new FileManagerException(fnfe.Message, fnfe.InnerException);
            }
        }
    }
}
