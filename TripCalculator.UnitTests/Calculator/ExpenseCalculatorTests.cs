using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Calculator;
using TripCalculator.Model;

namespace TripCalculator.UnitTests.Calculator
{
    [TestClass]
    public class ExpenseCalculatorTests
    {
        [TestMethod]
        public void CalculateBill()
        {
            Participant p1 = new Participant();
            p1.AddCharge(10.00);
            p1.AddCharge(20.00);

            Participant p2 = new Participant();
            p2.AddCharge(15.00);
            p2.AddCharge(15.01);
            p2.AddCharge(3.00);
            p2.AddCharge(3.01);

            Participant p3 = new Participant();
            p3.AddCharge(5.00);
            p3.AddCharge(9.00);
            p3.AddCharge(4.00);

            Expense e = new Expense();
            e.AddParticipant(p1);
            e.AddParticipant(p2);
            e.AddParticipant(p3);

            List<Expense> eList = new List<Expense> { e };

            ICalculator c = new ExpenseCalculator(eList);
            c.Calculate();

            Assert.AreEqual(Math.Round(eList[0].Participants[0].Balance, 2), -1.99);
            Assert.AreEqual(Math.Round(eList[0].Participants[1].Balance, 2), -8.01);
            Assert.AreEqual(Math.Round(eList[0].Participants[2].Balance, 2), 10.01);
        }
    }
}
