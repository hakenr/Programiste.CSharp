var account1 = new BankAccount("Adam", 0);
Console.WriteLine($"Account {account1.Number} was created for {account1.Owner} with {account1.Balance} initial balance.");

account1.MakeDeposit(500, DateTime.Now, "Friend paid me back");
Console.WriteLine(account1.Balance);

account1.MakeWithdrawal(100, DateTime.Now, "Rent payment");
Console.WriteLine(account1.Balance);

Console.WriteLine(account1.GetAccountHistory());


var account2 = new BankAccount("David", 100);
Console.WriteLine($"Account {account2.Number} was created for {account2.Owner} with {account2.Balance} initial balance.");

account2.MakeDeposit(200, DateTime.Now, "Gift from parents");
account2.MakeWithdrawal(30, DateTime.Now, "Games subscription");
account2.MakeDeposit(1800, DateTime.Now, "Salary");

Console.WriteLine(account2.GetAccountHistory());
