using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking2
{
	public class CreditLineAccount : BankAccount
	{
		public CreditLineAccount(string name, decimal initialBalance, decimal creditLimit = 100000)
			: base(name, initialBalance)
		{
			this.minimalBalance = -creditLimit;
		}

		public override void ProcessMonthlyActions()
		{
			base.ProcessMonthlyActions();

			if (Balance < 0)
			{
				// Negate the balance to get a positive interest charge:
				var interest = -Balance * 0.07m;
				MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
			}
		}
	}
}
