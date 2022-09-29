using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	public class Kostka
	{
		int pocetSten;

		public Kostka(int pocetSten)
		{
			this.pocetSten = pocetSten;
		}

		public int Hod()
		{
			var x = Random.Shared.Next(1, pocetSten + 1);
			Console.WriteLine($"Kostka hodila {x}.");
			return x;
		}
	}
}
