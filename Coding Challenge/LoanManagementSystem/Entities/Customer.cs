using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entities
{
    internal class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }


        public Customer(int customerID, string name, string email, string phoneNumber, string address, int creditScore)
        {
            CustomerID = customerID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditScore = creditScore;
        }

        public Customer()
        {
        }

        public override string ToString()
        {
            return $"CustomerID:: {CustomerID}, Name:: {Name}, CreditScore:: {CreditScore}";
        }
    }
}
