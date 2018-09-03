using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Exceptions;
using TripCalculator.Validator.Tokenizers;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.UnitTests.Validator.Tokenizers
{
    [TestClass]
    public class RegexTokenizerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void Tokenize_IsInvalidTextToken()
        {
            List<string> listValue = new List<string>();
            listValue.Add("abc");

            ITokenizer tokenizer = new RegexTokenizer();
            IEnumerable<Token> tokens = tokenizer.Tokenize(listValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void Tokenize_IsInvalidDecimalToken()
        {
            List<string> listValue = new List<string>();
            listValue.Add(".01");

            ITokenizer tokenizer = new RegexTokenizer();
            IEnumerable<Token> tokens = tokenizer.Tokenize(listValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void Tokenize_IsInvalidZeroToken()
        {
            List<string> listValue = new List<string>();
            listValue.Add(".00");

            ITokenizer tokenizer = new RegexTokenizer();
            IEnumerable<Token> tokens = tokenizer.Tokenize(listValue);
        }
    }
}
