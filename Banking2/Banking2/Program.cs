using Banking2;

var accounts = new List<BankAccount>();

var creditAccount = new CreditLineAccount("Pepa", 0, 1000);
accounts.Add(creditAccount);
creditAccount.MakeWithdrawal(10, DateTime.Now, "Půjčka 10 Kč výběr");
Console.WriteLine(creditAccount.GetAccountHistory());

var bankAccount = new BankAccount("Pepa", 0);
accounts.Add(bankAccount);
bankAccount.MakeDeposit(1000, DateTime.Now, "Vklad 1000 Kč");
Console.WriteLine(bankAccount.GetAccountHistory());


var interestEarningAccount = new InterestEarningAccount("Pepa", 0);
accounts.Add(interestEarningAccount);
interestEarningAccount.MakeDeposit(1000, DateTime.Now, "Vklad 1000 Kč");
Console.WriteLine(interestEarningAccount.GetAccountHistory());

// měsíční zpracování
foreach (var account in accounts)
{
	account.ProcessMonthlyActions();
}

Console.WriteLine("CREDIT ACCOUNT:");
Console.WriteLine(creditAccount.GetAccountHistory());


Console.WriteLine("BANK ACCOUNT:");
Console.WriteLine(bankAccount.GetAccountHistory());

Console.WriteLine("InterestEarningAccount ACCOUNT:");
Console.WriteLine(interestEarningAccount.GetAccountHistory());
