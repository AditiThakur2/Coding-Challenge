using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Service
{
    internal interface ILoanService
    {
        void GetAllCustomer();
        void ApplyLoan();
        void CalculateInterest();
        void LoanStatus();
        void CalculateEMI();
        void LoanRepayment();
        void GetAllLoans();
        void GetLoanById();

    }
}
