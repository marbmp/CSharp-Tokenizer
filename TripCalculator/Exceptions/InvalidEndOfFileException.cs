using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Exceptions
{
    [Serializable]
    public class InvalidEndOfFileException : Exception
    {
        public InvalidEndOfFileException()
: base("Invalid end of file")
        {

        }
    }
}
