using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Model
{
    public class Participant
    {
        public double TotalCharge { get; private set; }
        public double Balance { get; set; }

        public void AddCharge(double charge)
        {
            //Increment total charge
            TotalCharge += charge;
        }
    }
}
