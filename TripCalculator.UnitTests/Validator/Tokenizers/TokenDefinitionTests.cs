using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Validator.Tokenizers;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.UnitTests.Validator.Tokenizers
{
    [TestClass]
    public class TokenDefinitionTests
    {
        [TestMethod]
        public void MatchToken_QuantityMatch()
        {
            var match = getTokenDefinitions()[0].Match("1");

            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void MatchToken_QuantityNoMatch()
        {
            var match = getTokenDefinitions()[0].Match("1.1");
            var match2 = getTokenDefinitions()[0].Match("abc");
            var match3 = getTokenDefinitions()[0].Match("0.1");
            var match4 = getTokenDefinitions()[0].Match("0");

            Assert.IsFalse(match.IsMatch);
            Assert.IsFalse(match2.IsMatch);
            Assert.IsFalse(match3.IsMatch);
            Assert.IsFalse(match4.IsMatch);
        }

        [TestMethod]
        public void MatchToken_AmountMatch()
        {
            var match = getTokenDefinitions()[1].Match("1.00");

            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void MatchToken_AmountNoMatch()
        {
            var match = getTokenDefinitions()[1].Match("1.1");
            var match2 = getTokenDefinitions()[1].Match("abc");
            var match3 = getTokenDefinitions()[1].Match("0.1");
            var match4 = getTokenDefinitions()[1].Match("0");

            Assert.IsFalse(match.IsMatch);
            Assert.IsFalse(match2.IsMatch);
            Assert.IsFalse(match3.IsMatch);
            Assert.IsFalse(match4.IsMatch);
        }

        [TestMethod]
        public void MatchToken_ZeroMatch()
        {
            var match = getTokenDefinitions()[2].Match("0");

            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void MatchToken_ZeroNoMatch()
        {
            var match = getTokenDefinitions()[2].Match("1.1");
            var match2 = getTokenDefinitions()[2].Match("abc");
            var match3 = getTokenDefinitions()[2].Match("0.1");
            var match4 = getTokenDefinitions()[2].Match("1");

            Assert.IsFalse(match.IsMatch);
            Assert.IsFalse(match2.IsMatch);
            Assert.IsFalse(match3.IsMatch);
            Assert.IsFalse(match4.IsMatch);
        }

        private List<TokenDefinition> getTokenDefinitions()
        {
            return new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Quantity, @"^(?!0*?$)(\d+$)"),
                new TokenDefinition(TokenType.Amount, @"^\d+\.\d{2}$"),
                new TokenDefinition(TokenType.Zero, @"^(0$)")
            };
        }
    }
}
