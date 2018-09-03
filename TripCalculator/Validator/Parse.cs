using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Exceptions;
using TripCalculator.Model;
using TripCalculator.Validator.Tokens;

namespace TripCalculator.Validator
{
    public class Parser
    {
        private Stack<Token> _tokenSequence;
        private Token _lookaheadFirst;
        private Token _lookaheadSecond;
        private List<Expense> _expenses;
        private int _line = 1;

        public List<Expense> Parse(List<Token> tokens)
        {
            LoadSequenceStack(tokens);
            PrepareLookaheads();
            _expenses = new List<Expense>();

            ProcessExpense();

            return _expenses;
        }

        /// <summary>
        /// Put tokens into a stack
        /// </summary>
        /// <param name="tokens"></param>
        private void LoadSequenceStack(List<Token> tokens)
        {
            _tokenSequence = new Stack<Token>();
            int count = tokens.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                _tokenSequence.Push(tokens[i]);
            }
        }

        /// <summary>
        /// Gets the first and second items in the stack
        /// </summary>
        private void PrepareLookaheads()
        {
            _lookaheadFirst = _tokenSequence.Pop();
            if (_lookaheadFirst.TokenType != TokenType.Zero)
                _lookaheadSecond = _tokenSequence.Pop();
        }

        /// <summary>
        /// Discart token from stack
        /// </summary>
        private void DiscardToken()
        {
            _lookaheadFirst = _lookaheadSecond.Clone();

            if (_lookaheadFirst.TokenType != TokenType.Zero && _tokenSequence.Count >= 1)
                _lookaheadSecond = _tokenSequence.Pop();

            _line++;
        }

        private void ProcessExpense()
        {
            try
            {
                //Check if token is correct
                if (_lookaheadFirst.TokenType == TokenType.Quantity)
                {
                    //Get quantity of participants
                    int participantsQtt = int.Parse(_lookaheadFirst.Value);
                    //Add new expense
                    Expense expense = new Expense();
                    _expenses.Add(expense);
                    //Discart current token
                    DiscardToken();

                    //Process participants
                    for (int i = 0; i < participantsQtt; i++)
                    {
                        Participant p = new Participant();
                        ProcessParticipant(p);
                        expense.AddParticipant(p);
                    }

                    //Try to process new expense
                    ProcessExpense();
                }
                else if (_lookaheadFirst.TokenType == TokenType.Zero)
                {
                    //End of file
                    return;
                }
                else if (_tokenSequence.Count == 0)
                {
                    throw new InvalidEndOfFileException();
                }
                else
                {
                    throw new InvalidTokenException(_line, _lookaheadFirst.Value, "Number of participants or 0 to end file");
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        private void ProcessParticipant(Participant participant)
        {
            //Check if token is correct
            if (_lookaheadFirst.TokenType == TokenType.Quantity)
            {
                //Get quantity of charges
                int chargeQtt = int.Parse(_lookaheadFirst.Value);
                //Discart current token
                DiscardToken();

                //Process charges
                for (int i = 0; i < chargeQtt; i++)
                {
                    ProcessCharge(participant);
                }
            }
            else if (_lookaheadFirst.TokenType == TokenType.Zero)
            {
                throw new InvalidEndOfFileException();
            }
            else
            {
                throw new InvalidTokenException(_line, _lookaheadFirst.Value, "Number of charges");
            }
        }

        private void ProcessCharge(Participant participant)
        {
            //Check if token is correct
            if (_lookaheadFirst.TokenType == TokenType.Amount)
            {
                //Get the charge amount
                double charge = double.Parse(_lookaheadFirst.Value);
                //Add charge to participant
                participant.AddCharge(charge);
                //Discart current token
                DiscardToken();
            }
            else if (_lookaheadFirst.TokenType == TokenType.Zero)
            {
                throw new InvalidEndOfFileException();
            }
            else
            {
                throw new InvalidTokenException(_line, _lookaheadFirst.Value, "Charge amount");
            }
        }

    }
}
