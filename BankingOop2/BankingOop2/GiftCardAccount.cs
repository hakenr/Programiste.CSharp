namespace Banking;

public class GiftCardAccount : BankAccount
{
	private decimal monthlyDeposit = 0m;

	public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
	{ 
		this.monthlyDeposit = monthlyDeposit;
	}

	public override void PerformMonthEndTransactions()
	{
		if (monthlyDeposit != 0)
		{
			MakeDeposit(monthlyDeposit, DateTime.Now, "Add monthly deposit");
		}
	}
}
