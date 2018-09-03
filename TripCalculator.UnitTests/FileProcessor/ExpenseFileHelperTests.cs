using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.FileProcessor;
using TripCalculator.Model;

namespace TripCalculator.UnitTests.FileProcessor
{
    [TestClass]
    public class ExpenseFileHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFile_NoExists()
        {
            IFileHelper expenseFileHelper = new ExpenseFileHelper("NoFile.txt");
            expenseFileHelper.ReadFile();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFile_FileIsEmpty()
        {
            string fileName = "EmptyFile.txt";
            CreateEmptyInputFile(fileName);
            IFileHelper expenseFileHelper = new ExpenseFileHelper(fileName);
            expenseFileHelper.ReadFile();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateFile_NothingToProcess()
        {
            string fileName = "FileNothingToProcess.txt";
            CreateInputFileNothingToProcess(fileName);
            IFileHelper expenseFileHelper = new ExpenseFileHelper(fileName);
            expenseFileHelper.ReadFile();
            expenseFileHelper.ValidateFile();
        }

        [TestMethod]
        public void ValidateFile_CheckExpenses()
        {
            string fileName = "TestExpenses.txt";
            CreateInputFile(fileName);
            IFileHelper expenseFileHelper = new ExpenseFileHelper(fileName);
            expenseFileHelper.ReadFile();
            expenseFileHelper.ValidateFile();
            List<Expense> expenses = expenseFileHelper.GetExpenses();

            Assert.AreEqual(expenses.Count, 1);
            Assert.AreEqual(expenses[0].Participants.Count, 2);
            Assert.AreEqual(expenses[0].Participants[0].TotalCharge, 14.00);
            Assert.AreEqual(expenses[0].Participants[1].TotalCharge, 15.95);
        }

        [TestMethod]
        public void CreateOutput_CheckOutputName()
        {
            string fileName = "TestExpenses.txt";
            CreateInputFile(fileName);
            IFileHelper expenseFileHelper = new ExpenseFileHelper(fileName);
            expenseFileHelper.ReadFile();
            expenseFileHelper.ValidateFile();
            expenseFileHelper.CreateOutput();

            Assert.IsTrue(FileExists(fileName + ".out"));
        }

        private void CreateInputFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            DeleteFile(fileName);

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
        private void CreateEmptyInputFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            DeleteFile(fileName);

            using (FileStream fs = File.Create(path))
            {

            }
        }
        private void CreateInputFileNothingToProcess(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            DeleteFile(fileName);

            using (FileStream fs = File.Create(path))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write("0");
                }
            }
        }
        private void DeleteFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (FileExists(fileName))
            {
                File.Delete(path);
            }
        }

        private bool FileExists(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.Exists(path);
        }
    }
}
