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


public class BankAccount
{
	private static int accountNumberSeed = 1000000;
	public string Number { get; }
	public string Owner { get; set; }
	public decimal Balance
	{
		get
		{
			decimal balance = 0;
			foreach (var item in allTransactions)
			{
				balance += item.Amount;
			}

			return balance;
		}
	}

	private List<Transaction> allTransactions = new List<Transaction>();

	public BankAccount(string name, decimal initialBalance)
	{
		this.Number = accountNumberSeed.ToString();
		accountNumberSeed++;

		this.Owner = name;

		if (initialBalance != 0)
		{
			MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
		}
	}

	public void MakeDeposit(decimal amount, DateTime date, string note)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
		}
		var deposit = new Transaction(amount, date, note);
		allTransactions.Add(deposit);
	}

	public void MakeWithdrawal(decimal amount, DateTime date, string note)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
		}
		if (Balance - amount < 0)
		{
			throw new InvalidOperationException("Not sufficient funds for this withdrawal");
		}
		var withdrawal = new Transaction(-amount, date, note);
		allTransactions.Add(withdrawal);
	}

	public string GetAccountHistory()
	{
		var report = new System.Text.StringBuilder();

		decimal balance = 0;
		report.AppendLine("Date\t\tAmount\tBalance\tNote");
		foreach (var item in allTransactions)
		{
			balance += item.Amount;
			report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
		}

		return report.ToString();
	}
}

public class Transaction
{
	public decimal Amount { get; }
	public DateTime Date { get; }
	public string Notes { get; }

	public Transaction(decimal amount, DateTime date, string note)
	{
		this.Amount = amount;
		this.Date = date;
		this.Notes = note;
	}
}