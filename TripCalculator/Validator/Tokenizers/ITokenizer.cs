using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.Validator.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(List<string> listValue);
    }
}
