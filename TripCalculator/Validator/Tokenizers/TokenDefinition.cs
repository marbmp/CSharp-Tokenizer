using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.Validator.Tokenizers
{
    public class TokenDefinition
    {
        private Regex _regex;
        private readonly TokenType _returnsToken;

        public TokenDefinition(TokenType returnsToken, string regexPattern)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            _returnsToken = returnsToken;
        }

        /// <summary>
        /// Execute the match for the given value
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public TokenMatch Match(string inputString)
        {
            try
            {
                var match = _regex.Match(inputString);
                if (match.Success)
                {
                    return new TokenMatch()
                    {
                        IsMatch = true,
                        TokenType = _returnsToken,
                        Value = match.Value
                    };
                }
                else
                {
                    return new TokenMatch() { IsMatch = false };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
