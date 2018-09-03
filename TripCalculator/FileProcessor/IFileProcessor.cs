using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.FileProcessor
{
    public interface IFileProcessor
    {       
        void ProcessFile();
        string GetFileOutput();
    }
}
