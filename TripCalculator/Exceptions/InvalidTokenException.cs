using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.Exceptions
{
    [Serializable]
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(int line, string value)
    : base(HumanReadable(line, value))
        {

        }

        private static string HumanReadable(int line, string value)
        {
            return string.Format("Input '{0}' on line {1} is not valid", value, line);
        }

        public InvalidTokenException(int line, string value, string expectedValue)
: base(HumanReadable(line, value, expectedValue))
        {

        }

        private static string HumanReadable(int line, string value, string expectedValue)
        {
            return string.Format("Input '{0}' on line {1} is not valid. Expecting {2}", value, line, expectedValue);
        }
    }
}
