using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entities
{
    internal class Loan
    {
        public int LoanID { get; set; }
        public int CustomerID { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan(int loanID, int customerID, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanID = loanID;
            CustomerID = customerID;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public Loan()
        {
        }

        public override string ToString()
        {
            return $"LoanID: {LoanID}, CustomerID: {CustomerID}, PrincipalAmount: {PrincipalAmount}, InterestRate: {InterestRate}, " +
                   $"LoanTerm: {LoanTerm}, LoanType: {LoanType}, LoanStatus: {LoanStatus}";
        }

    }
}
