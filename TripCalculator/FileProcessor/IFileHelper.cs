using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Model;

namespace TripCalculator.FileProcessor
{
    public interface IFileHelper
    {
        void ReadFile();
        void ValidateFile();
        string CreateOutput();
        List<Expense> GetExpenses();
    }
}
