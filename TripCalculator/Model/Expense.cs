using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Model
{
    public class Expense
    {
        public List<Participant> Participants { get; private set; }
        public double TotalExpense { get; set; }

        public Expense()
        {
            Participants = new List<Participant>();
        }

        public void AddParticipant(Participant p)
        {
            Participants.Add(p);
            //Increment total expense
            TotalExpense += p.TotalCharge;
        }
    }
}
