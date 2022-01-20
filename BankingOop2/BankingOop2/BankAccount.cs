namespace Banking;

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
	private readonly decimal minimumBalance;

	public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
	{
		this.Number = accountNumberSeed.ToString();
		accountNumberSeed++;

		this.Owner = name;
		this.minimumBalance = minimumBalance;

		if (initialBalance != 0)
		{
			MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
		}
	}

	public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
	{
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
		var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);
		var withdrawal = new Transaction(-amount, date, note);
		allTransactions.Add(withdrawal);
		if (overdraftTransaction != null)
		{
			allTransactions.Add(overdraftTransaction);
		}
	}

	protected virtual Transaction CheckWithdrawalLimit(bool isOverdrawn)
	{
		if (isOverdrawn)
		{
			throw new InvalidOperationException("Not sufficient funds for this withdrawal");
		}
		else
		{
			return null;
		}
	}

	public string GetAccountHistory()
	{
		var report = new System.Text.StringBuilder();

		decimal balance = 0;
		report.AppendLine("Date                    Amount        Balance   Note");
		foreach (var item in allTransactions)
		{
			balance += item.Amount;
			report.AppendLine($"{item.Date.ToShortDateString(), -15}{item.Amount.ToString("n2"),15}{balance.ToString("n2"),15}   {item.Notes}");
		}

		return report.ToString();
	}

	public virtual void PerformMonthEndTransactions() { }
}
