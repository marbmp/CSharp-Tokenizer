using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCalculator.Model;

namespace TripCalculator.Calculator
{
    public class ExpenseCalculator: ICalculator
    {
        public List<Expense> _expenses { get; private set; }
        public ExpenseCalculator(List<Expense> expenses)
        {
            _expenses = expenses;
        }

        /// <summary>
        /// Set the balance for each participant of the expenses
        /// </summary>
        public void Calculate()
        {
            List<string> output = new List<string>();

            foreach (Expense e in _expenses)
            {
                double average = e.TotalExpense / e.Participants.Count();
                foreach (Participant p in e.Participants)
                {
                    p.Balance = average - p.TotalCharge;
                }
            }
        }
    }
}
