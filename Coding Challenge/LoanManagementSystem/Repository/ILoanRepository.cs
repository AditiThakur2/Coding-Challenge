using LoanManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Repository
{
    internal interface ILoanRepository
    {
        List<Customer> GetAllCustomer();
        void ApplyLoan(Loan loan);
        decimal CalculateInterest(int loanId);
        Loan GetLoanById(int loanId);
        void LoanStatus(int loanId);
        decimal CalculateEMI(int loanId);
        int LoanRepayment(int loanId, decimal amount);
        List<Loan> GetAllLoans();


    }
}
