using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entities
{
    internal class CarLoan:Loan
    {
        public string CarModel { get; set; }
        public int CarValue { get; set; }


        // Overloaded constructor with parameters
        public CarLoan(int loanID, int customerID, decimal principalAmount, decimal interestRate, int loanTerm, string loanStatus, string carModel, int carValue)
            : base(loanID, customerID, principalAmount, interestRate, loanTerm, "CarLoan", loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, CarModel: {CarModel}, CarValue: {CarValue}";
        }
    }
}
