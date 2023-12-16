using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entities
{
    internal class HomeLoan:Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }

        // Overloaded constructor with parameters
        public HomeLoan(int loanID, int customerID, decimal principalAmount, decimal interestRate, int loanTerm, string loanStatus, string propertyAddress, int propertyValue)
            : base(loanID, customerID, principalAmount, interestRate, loanTerm, "HomeLoan", loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, PropertyAddress: {PropertyAddress}, PropertyValue: {PropertyValue}";
        }
    }
}
