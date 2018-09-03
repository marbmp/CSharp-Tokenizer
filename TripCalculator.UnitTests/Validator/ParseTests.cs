using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Exceptions;
using TripCalculator.Model;
using TripCalculator.Validator;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.UnitTests.Validator
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidEndOfFileException))]
        public void Parse_InvalidEndOfFile_ExpectingParticipant()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Zero, "0"));

            Parser p = new Parser();
            List<Expense> expenses = p.Parse(tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEndOfFileException))]
        public void Parse_InvalidEndOfFile_ExpectingCharge()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Quantity, "1"));
            tokens.Add(new Token(TokenType.Zero, "0"));

            Parser p = new Parser();
            List<Expense> expenses = p.Parse(tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void Parse_InvalidTokenException_ExpectingParticipant()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Amount, "10.00"));

            Parser p = new Parser();
            List<Expense> expenses = p.Parse(tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void Parse_InvalidTokenException_ExpectingAmount()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Quantity, "1"));
            tokens.Add(new Token(TokenType.Amount, "1"));

            Parser p = new Parser();
            List<Expense> expenses = p.Parse(tokens);
        }

        [TestMethod]
        public void Parse_Parsed()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Amount, "1.00"));
            tokens.Add(new Token(TokenType.Amount, "1.00"));
            tokens.Add(new Token(TokenType.Quantity, "2"));
            tokens.Add(new Token(TokenType.Amount, "2.00"));
            tokens.Add(new Token(TokenType.Amount, "2.00"));
            tokens.Add(new Token(TokenType.Zero, "0"));

            Parser p = new Parser();
            List<Expense> expenses = p.Parse(tokens);
        }
    }
}
