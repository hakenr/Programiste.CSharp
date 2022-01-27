using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking2
{
	public class InterestEarningAccount : BankAccount
	{
		private decimal interestRate;

		public InterestEarningAccount(string name, decimal initialBalance, decimal interestRate = 0.02m)
			: base(name, initialBalance)
		{
			this.interestRate = interestRate;
		}

		public override void ProcessMonthlyActions()
		{
			var interest = Balance * interestRate / 12m;
			MakeDeposit(interest, DateTime.Now, "apply monthly interest");

			base.ProcessMonthlyActions();
		}
	}
}
