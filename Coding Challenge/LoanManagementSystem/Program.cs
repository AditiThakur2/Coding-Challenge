

using LoanManagementSystem.Service;

ILoanService loanServiceImpl = new LoanServiceImpl();

while(true)
{
    Console.WriteLine("1. GetAllCustomer");
    Console.WriteLine("2. ApplyLoan");
    Console.WriteLine("3. CalculateInterest");
    Console.WriteLine("4. GetLoanById");
    Console.WriteLine("5. LoanStatus");
    Console.WriteLine("6. CalculateEMI");
    Console.WriteLine("7. LoanRepayment");
    Console.WriteLine("8. GetAllLoans");
    Console.WriteLine("\nEnter your Choice:: \n");
    int choice = int.Parse(Console.ReadLine());

    switch(choice)
    {
        case 1:
            loanServiceImpl.GetAllCustomer();
            break;
        case 2:
            loanServiceImpl.ApplyLoan();
            break;
        case 3:
            loanServiceImpl.CalculateInterest();
            break;
        case 4:
            loanServiceImpl.GetLoanById();
            break;
        case 5:
            loanServiceImpl.LoanStatus();
            break;
        case 6:
            loanServiceImpl.CalculateEMI();
            break;
        case 7:
            loanServiceImpl.LoanRepayment();
            break;
        case 8:
            loanServiceImpl.GetAllLoans();
            break;
    }
}