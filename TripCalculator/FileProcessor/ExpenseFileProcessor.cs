using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Calculator;
using TripCalculator.Model;
using TripCalculator.Validator;
using TripCalculator.Validator.Tokenizers;

namespace TripCalculator.FileProcessor
{
    public class ExpenseFileProcessor: IFileProcessor
    {
        #region "Properties"
        private string _fileName;
        private string _fileOutput;
        #endregion

        #region "Methods"
        public ExpenseFileProcessor(string fileName)
        {
            _fileName = fileName;
        }

        public void ProcessFile()
        {
            IFileHelper expenseFileHelper = new ExpenseFileHelper(_fileName);

            //Process Input
            expenseFileHelper.ReadFile();
            expenseFileHelper.ValidateFile();

            //Calculate
            ICalculator calc = new ExpenseCalculator(expenseFileHelper.GetExpenses());
            calc.Calculate();

            //Process Output
            _fileOutput = expenseFileHelper.CreateOutput();

        }

        public string GetFileOutput()
        {
            return _fileOutput;
        }
        #endregion
    }
}
