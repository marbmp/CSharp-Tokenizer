using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Model;
using TripCalculator.Validator;
using TripCalculator.Validator.Tokenizers;

namespace TripCalculator.FileProcessor
{
    public class ExpenseFileHelper: IFileHelper
    {
        private const string OUTPUT = ".out";

        #region "Properties"
        private string _fileName;
        private List<string> _fileContent;
        public List<Expense> _expenses;
        #endregion

        #region "Methods"
        public ExpenseFileHelper(string fileName)
        {
            _fileName = fileName;
        }
        public void ReadFile()
        {
            try
            {
                //Get path + file name
                string path = Path.Combine(Directory.GetCurrentDirectory(), _fileName);
                //Read file
                _fileContent = File.ReadAllLines(path).ToList();
                //Check file is empty
                if (_fileContent.Count == 0)
                    throw new Exception("File is Empty!");
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new FileNotFoundException("File Not Found!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidateFile()
        {
            try
            {
                //Get all lines of the file as tokens
                ITokenizer tokenizer = new RegexTokenizer();
                var tokenSequence = tokenizer.Tokenize(_fileContent).ToList();
                //Parse all tokens
                Parser p = new Parser();
                _expenses = p.Parse(tokenSequence);

                if (_expenses.Count == 0)
                    throw new Exception("Nothing to Process");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CreateOutput()
        {
            try
            {
                //Get path + file name
                string path = Path.Combine(Directory.GetCurrentDirectory(), _fileName);
                //Add the output extension
                path = path += OUTPUT;

                //If file already exists, delete it
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                //Create File
                using (FileStream fs = File.Create(path))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        WriteOutput(sw);
                    }
                }

                //return output file name
                return path;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WriteOutput(StreamWriter sw)
        {
            try
            {
                Expense lastExpense = _expenses.Last();
                foreach (Expense e in _expenses)
                {
                    Participant lastParticipant = e.Participants.Last();
                    foreach (Participant p in e.Participants)
                    {
                        //Write the balance for each particioant
                        string balance = p.Balance.ToString("$0.00;($0.00)");
                        if (p.Equals(lastParticipant))
                            sw.Write(balance);
                        else
                            sw.WriteLine(balance);
                    }

                    if (!e.Equals(lastExpense))
                        sw.WriteLine(sw.NewLine);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Expense> GetExpenses()
        {
            return _expenses;
        }
        #endregion
    }
}
