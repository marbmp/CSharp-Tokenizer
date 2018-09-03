using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.FileProcessor;

namespace TripCalculator.UnitTests.FileProcessor
{
    [TestClass]
    public class ExpenseFileProcessorTests
    {
        [TestMethod]
        public void ProcessFile_CheckOutputName()
        {
            string fileName = "Test.txt";
            CreateInputFile(fileName);

            IFileProcessor file = new ExpenseFileProcessor("Test.txt");
            file.ProcessFile();

            Assert.AreEqual(file.GetFileOutput(), Path.Combine(Directory.GetCurrentDirectory(), fileName + ".out"));
        }

        [TestMethod]
        public void ProcessFile_CheckOutputBalances()
        {
            string fileName = "Test.txt";
            CreateInputFile(fileName);

            IFileProcessor file = new ExpenseFileProcessor("Test.txt");
            file.ProcessFile();

            List<string> _fileContent = File.ReadAllLines(file.GetFileOutput()).ToList();

            Assert.AreEqual(_fileContent[0], "$0.98");
            Assert.AreEqual(_fileContent[1], "($0.98)");
        }

        private void CreateInputFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("2");
                    sw.WriteLine("2");
                    sw.WriteLine("8.00");
                    sw.WriteLine("6.00");
                    sw.WriteLine("2");
                    sw.WriteLine("9.20");
                    sw.WriteLine("6.75");
                    sw.Write("0");
                }
            }

        }
    }
}
