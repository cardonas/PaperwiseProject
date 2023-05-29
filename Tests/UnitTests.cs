using DataAccessLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ObjectLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Policy;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnitTests
    {
        private FileManager fileManager;
        private FileAccessor fileAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            fileManager = new FileManager();
            fileAccessor = new FileAccessor();
        }

        [TestMethod]
        public void TestFileRetrieval()
        {
            var files = new[]
            {
                "fake_path_1",
                "fake_path_2",
                "fake_path_3"
            };

            var rootPath = "C:\\Input";
            var service = new Mock<IDirectoryAccessor>();
            service.Setup(_ => _.GetFiles(rootPath)).Returns(files).Verifiable();

            fileManager = new FileManager(service.Object);

            fileManager.GetFiles(rootPath);

            service.Verify();
        }


        [TestMethod]
        public void TestFileInformation()
        {
            var rootPath = @"..\..\ProjectDocuments";

            fileManager.GetFiles(rootPath);

            fileManager.GetFileInfos();

            List<FileProperties> fileInfos = fileManager.fileProperties;

            List<FileProperties> expectedList = new List<FileProperties>()
            {
                new FileProperties()
                {
                    Name = "1 page.txt",
                    Length = 78,
                    Extension = ".txt",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "10 page color.tif",
                    Length = 27983102,
                    Extension = ".tif",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")

                },
                new FileProperties()
                {
                    Name = "big.jpg",
                    Length = 194256,
                    Extension = ".jpg",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test1.pdf",
                    Length = 49231,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test2.pdf",
                    Length = 49230,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test3.pdf",
                    Length = 49229,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test4.pdf",
                    Length = 49506,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test5.pdf",
                    Length = 49551,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test6.pdf",
                    Length = 49745,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
            };

            for (int i = 0; i < fileInfos.Count; i++)
            {
                Assert.AreEqual(expectedList[i].Name, fileInfos[i].Name);
                Assert.AreEqual(expectedList[i].Length, fileInfos[i].Length);
                Assert.AreEqual(expectedList[i].Extension, fileInfos[i].Extension);
                Assert.AreEqual(expectedList[i].CreationTime.ToString(), fileInfos[i].CreationTime.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestWriteFileException()
        {
            List<FileProperties> fileProperties = new List<FileProperties>()
            {
                new FileProperties()
                {
                    Name = "1 page.txt",
                    Length = 78,
                    Extension = ".txt",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "10 page color.tif",
                    Length = 27983102,
                    Extension = ".tif",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")

                },
                new FileProperties()
                {
                    Name = "big.jpg",
                    Length = 194256,
                    Extension = ".jpg",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test1.pdf",
                    Length = 49231,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test2.pdf",
                    Length = 49230,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test3.pdf",
                    Length = 49229,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test4.pdf",
                    Length = 49506,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test5.pdf",
                    Length = 49551,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
                new FileProperties()
                {
                    Name = "Test6.pdf",
                    Length = 49745,
                    Extension = ".pdf",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
            };

            fileAccessor.WriteFileInfoCSV("C:\\Test", fileProperties);
        }

        [TestMethod]
        public void TestWriteFile()
        {
            List<FileProperties> fileProperties = new List<FileProperties>()
            {
                new FileProperties()
                {
                    Name = "1 page.txt",
                    Length = 78,
                    Extension = ".txt",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
            };
            var rootPath = @"..\..\ProjectDocuments\test.csv";


            fileAccessor.WriteFileInfoCSV(rootPath, fileProperties);

            Assert.IsTrue(File.Exists(rootPath));

            File.Delete(rootPath);

        }

        [TestMethod]
        public void TestFileManagerWriteCSV()
        {
            List<FileProperties> fileProperties = new List<FileProperties>()
            {
                new FileProperties()
                {
                    Name = "1 page.txt",
                    Length = 78,
                    Extension = ".txt",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
            };
            var rootPath = @"..\..\ProjectDocuments\test.csv";


            fileManager.fileProperties = fileProperties;
            fileManager.WriteCSV(rootPath);

            Assert.IsTrue(File.Exists(rootPath));

            File.Delete(rootPath);

        }

        [TestMethod]
        [ExpectedException(typeof(FileManagerException))]
        public void TestFileManagerWriteCSException()
        {
            List<FileProperties> fileProperties = new List<FileProperties>()
            {
                new FileProperties()
                {
                    Name = "1 page.txt",
                    Length = 78,
                    Extension = ".txt",
                    CreationTime = DateTime.Parse("05/27/2023 16:25:24")
                },
            };
            var rootPath = @"C:Test\Test.csv";


            fileManager.fileProperties = fileProperties;
            fileManager.WriteCSV(rootPath);


        }

    }
}