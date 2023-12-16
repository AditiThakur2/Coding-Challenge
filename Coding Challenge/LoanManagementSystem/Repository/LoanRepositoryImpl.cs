using LoanManagementSystem.Entities;
using LoanManagementSystem.Exception;
using LoanManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Repository
{
    internal class LoanRepositoryImpl:ILoanRepository
    {
        public string connectionString;
        SqlCommand cmd = null;

        public LoanRepositoryImpl()
        {
            connectionString = DBConnection.GetConnectionString();
            cmd = new SqlCommand();
        }

        //Customer Management
        #region List of All Customer

        public List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "SELECT * FROM Customers";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerID = (int)reader["customer_id"];
                    customer.Name = (string)reader["name"];
                    customer.Email = (string)reader["email_address"];
                    customer.PhoneNumber = (string)reader["phone_number"];
                    customer.Address = (string)reader["address"];
                    customer.CreditScore = (int)reader["credit_score"];
                    customers.Add(customer);
                }
                return customers;
            }
        }
        #endregion
        

        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Do you want to apply for the loan? (Yes/No)");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "yes")
            {
                loan.LoanStatus = "Pending";
                InsertLoanIntoDatabase(loan);
                Console.WriteLine("Loan application submitted successfully!");
            }
            else
            {
                Console.WriteLine("Loan application cancelled.");
            }
        }


        private void InsertLoanIntoDatabase(Loan loan)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO Loans VALUES(@CustomerID, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";
                    cmd.Parameters.AddWithValue("@CustomerID", loan.CustomerID);
                    cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    cmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateInterest(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            decimal interest = (decimal)(((double)principalAmount * (double)interestRate * loanTerm) / 12.0);
            return interest;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM Loans WHERE loan_id = @LoanID";
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Loan
                            {
                                LoanID = (int)reader["loan_id"],
                                CustomerID = (int)reader["customer_id"],
                                PrincipalAmount = (decimal)reader["principal_amount"],
                                InterestRate = (decimal)reader["interest_rate"],
                                LoanTerm = (int)reader["loan_term_months"],
                                LoanType = (string)reader["loan_type"],
                                LoanStatus = (string)reader["loan_status"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public void LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            int creditScore = GetCustomerCreditScore(loan.CustomerID);

            if (creditScore > 650)
            {
                UpdateLoanStatus(loanId, "Approved");
                Console.WriteLine($"Loan with ID {loanId} is approved.");
            }
            else
            {
                UpdateLoanStatus(loanId, "Rejected");
                Console.WriteLine($"Loan with ID {loanId} is rejected due to low credit score.");
            }
        }
        private int GetCustomerCreditScore(int customerId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT credit_score FROM Customers WHERE customer_id = @CustomerID";
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    object creditScoreObj = cmd.ExecuteScalar();

                    if (creditScoreObj != null && int.TryParse(creditScoreObj.ToString(), out int creditScore))
                    {
                        return creditScore;
                    }
                    return 0; // Default credit score if not found
                }
            }
        }
        private void UpdateLoanStatus(int loanId, string status)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE Loans SET loan_status = @Status WHERE loan_id = @LoanID";
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            double monthlyInterestRate = (double)interestRate / 12.0 / 100.0;
            int numberOfPayments = loanTerm;

            double emi = ((double)principalAmount * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, numberOfPayments))
                         / (Math.Pow(1 + monthlyInterestRate, numberOfPayments) - 1);

            return (decimal)emi;
        }


        public int LoanRepayment(int loanId, decimal amount)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            decimal emi = CalculateEMI(loan.PrincipalAmount,loan.InterestRate,loan.LoanTerm);

            if (amount < emi)
            {
                Console.WriteLine("Payment rejected. Amount is less than the EMI.");
                return 0;
            }

            int numberOfPaymentsMade = (int)(amount / emi);

            Console.WriteLine($"Successfully paid {numberOfPaymentsMade} EMI(s).");
            return numberOfPaymentsMade;
        }


        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "SELECT * FROM Loans";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Loan loan = new Loan();
                    loan.LoanID = (int)reader["loan_id"];
                    loan.CustomerID = (int)reader["customer_id"];
                    loan.PrincipalAmount = (decimal)reader["principal_amount"];
                    loan.InterestRate = (decimal)reader["interest_rate"];
                    loan.LoanTerm = (int)reader["loan_term_months"];
                    loan.LoanType = (string)reader["loan_type"];
                    loan.LoanStatus = (string)reader["loan_status"];
                    loans.Add(loan);
                }
                return loans;
            }
        }
    }
}
