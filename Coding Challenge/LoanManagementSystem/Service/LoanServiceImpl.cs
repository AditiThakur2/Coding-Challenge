using LoanManagementSystem.Entities;
using LoanManagementSystem.Exception;
using LoanManagementSystem.Repository;
using LoanManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Service
{
    internal class LoanServiceImpl:ILoanService
    {
        ILoanRepository loanRepository = new LoanRepositoryImpl();

        public void GetAllCustomer()
        {
            var customers = loanRepository.GetAllCustomer();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
        public void ApplyLoan()
        {
            Loan newLoan = new Loan
            {
                CustomerID = 1, // Assuming CustomerID 1 exists in the database
                PrincipalAmount = 50000.00M,
                InterestRate = 6.2M,
                LoanTerm = 36,
                LoanType = "CarLoan",
                LoanStatus = "Pending"
            };

            loanRepository.ApplyLoan(newLoan);
        }
        
        public void CalculateInterest()
        {
            int loanIdToCalculateInterest = 1; // Assuming there's a loan with ID 1 in the database

            try
            {
                decimal interest = loanRepository.CalculateInterest(loanIdToCalculateInterest);
                Console.WriteLine($"Interest for Loan ID {loanIdToCalculateInterest}: {interest:C}");
            }
            catch (InvalidLoanException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LoanStatus()
        {
            int loanIdForStatus = 2; // Assuming there's a loan with ID 2 in the database
            loanRepository.LoanStatus(loanIdForStatus);
        }

        public void CalculateEMI()
        {
            int loanIdForEMI = 3; // Assuming there's a loan with ID 3 in the database

            try
            {
                decimal emi = loanRepository.CalculateEMI(loanIdForEMI);
                Console.WriteLine($"EMI for Loan ID {loanIdForEMI}: {emi:C}");
            }
            catch (InvalidLoanException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LoanRepayment()
        {
            int loanIdForRepayment = 4; // Assuming there's a loan with ID 4 in the database
            decimal amountPaid = 2000.00M;

            int numberOfEMIPaid = loanRepository.LoanRepayment(loanIdForRepayment, amountPaid);
            Console.WriteLine($"Number of EMI(s) paid: {numberOfEMIPaid}");
        }

        public void GetAllLoans()
        {
            var allLoans = loanRepository.GetAllLoans();
            Console.WriteLine("All Loans:");
            foreach (var loan in allLoans)
            {
                Console.WriteLine($"Loan ID: {loan.LoanID}, Customer ID: {loan.CustomerID}, Principal Amount: {loan.PrincipalAmount:C}, Loan Status: {loan.LoanStatus}");
            }
        }

        public void GetLoanById()
        {
            int loanIdToRetrieve = 5; // Assuming there's a loan with ID 5 in the database

            try
            {
                var retrievedLoan = loanRepository.GetLoanById(loanIdToRetrieve);
                Console.WriteLine($"Loan Details for ID {loanIdToRetrieve}:");
                Console.WriteLine($"Customer ID: {retrievedLoan.CustomerID}, Principal Amount: {retrievedLoan.PrincipalAmount:C}, Loan Status: {retrievedLoan.LoanStatus}");
            }
            catch (InvalidLoanException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
