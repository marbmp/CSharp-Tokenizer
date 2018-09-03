using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.FileProcessor;
using TripCalculator.Model;

namespace TripCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Read file name
                string fileName = Console.ReadLine();
                //Exit if no name is inputed
                if (string.IsNullOrEmpty(fileName))
                    Environment.Exit(0);

                //Process file
                IFileProcessor file = new ExpenseFileProcessor(fileName);
                file.ProcessFile();
                //Write output file name
                Console.WriteLine(String.Format("Output created at {0}", file.GetFileOutput()));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
