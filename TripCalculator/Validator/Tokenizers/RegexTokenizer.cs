using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Exceptions;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.Validator.Tokenizers
{
    public class RegexTokenizer : ITokenizer
    {
        private List<TokenDefinition> _tokenDefinitions;

        public RegexTokenizer()
        {
            //All Accepted tokens
            _tokenDefinitions = new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Quantity, @"^(?!0*?$)(\d+$)"),
                new TokenDefinition(TokenType.Amount, @"^\d+\.\d{2}$"),
                new TokenDefinition(TokenType.Zero, @"^(0$)")
            };
        }

        /// <summary>
        /// Create a list of tokens after read all the values
        /// Check if any value is not valid
        /// </summary>
        /// <param name="listValue"></param>
        /// <returns></returns>
        public IEnumerable<Token> Tokenize(List<string> listValue)
        {
            var tokens = new List<Token>();

            try
            {
                int line = 1;
                foreach (string value in listValue)
                {
                    var match = FindMatch(value);
                    if (match.IsMatch)
                    {
                        tokens.Add(new Token(match.TokenType, match.Value));
                    }
                    else
                    {
                        throw new InvalidTokenException(line, value);
                    }

                    line++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tokens;
        }

        /// <summary>
        /// Try to find a match token for the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private TokenMatch FindMatch(string value)
        {
            try
            {
                foreach (var tokenDefinition in _tokenDefinitions)
                {
                    var match = tokenDefinition.Match(value);
                    if (match.IsMatch)
                        return match;
                }

                return new TokenMatch() { IsMatch = false };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
