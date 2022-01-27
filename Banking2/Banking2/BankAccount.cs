public class BankAccount
{
	private static int accountNumberSeed = 1000000;

	protected decimal minimalBalance = 0;
	
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

	public BankAccount(string name) : this(name, 0)
	{
	}

	public virtual void ProcessMonthlyActions()
	{
		MakeDeposit(2, DateTime.Now, "Bonus superbanky za lásku");
	}

	public void MakeDeposit(decimal amount, DateTime date, string note)
	{
		CheckAmount(amount);
		var deposit = new Transaction(amount, date, note);
		allTransactions.Add(deposit);
	}

	private void CheckAmount(decimal amount)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
		}
	}

	public void MakeWithdrawal(decimal amount, DateTime date, string note)
	{
		CheckAmount(amount);
		if (Balance - amount < minimalBalance)
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
