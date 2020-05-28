using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StringLetterCount
{
	class Program
	{
		// https://www.codewars.com/kata/59e19a747905df23cb000024/
		// Spočítej počet výskytů jednotlivých písmen a-z ve vstupním textu
		// Výsledek vrať v podobě "1a2b1c" (1x A, 2x B, 1xC) pro vstup "babc".

		public static string StringLetterCount3(string str)
		{
			return str
				.Select(c1 => Char.ToLower(c1))
				.Where(c2 => c2 >= 'a' && c2 <= 'z')
				.GroupBy(c3 => c3)
				.OrderBy(g => g.Key)
				.Aggregate("", (output, next) => output += next.Count().ToString() + next.Key);
		}

		public static string StringLetterCount(string str)
		{
			string returnStr = "";

			int[] poctyVyskytuPismen = new int[26];

			foreach (char c in str)
			{
				char x = Char.ToLower(c);
				if (x >= 'a' && x <= 'z')
				{
					poctyVyskytuPismen[x - 'a']++; 
				}
			}

			for (int i = 0; i < 26; i++)
			{
				if (poctyVyskytuPismen[i] > 0)
				{
					returnStr += poctyVyskytuPismen[i].ToString() + (char)(i + 'a');
				}
			}

			return returnStr;
		}

		public static string StringLetterCount2(string str)
		{
			string returnStr = "";

			Dictionary<char, int> poctyVyskytuPismen = new Dictionary<char, int>();

			foreach (char c in str)
			{
				char x = Char.ToLower(c);
				if (x >= 'a' && x <= 'z')
				{
					if (poctyVyskytuPismen.TryGetValue(x, out int dosavadniPocet))
					{
						poctyVyskytuPismen[x] = dosavadniPocet + 1;
					}
					else
					{
						poctyVyskytuPismen[x] = 1;
					}
				}
			}

			foreach (var item in poctyVyskytuPismen.OrderBy(i => i.Key))
			{
				returnStr += item.Value.ToString() + item.Key;
			}

			return returnStr;
		}

		public static string StringLetterCount1(string str)
		{
			str = str.ToLower();
			string alphabet = "abcdefghijklmnopqrstuvwxyz";
			string returnStr = "";
			foreach (char c in alphabet)
			{
				int count = str.Count(x => x == c);
				if (count > 0)
				{
					returnStr += count.ToString() + c.ToString();
				}
			}
			return returnStr;
		}

		static void Main(string[] args)
		{
			Assert.AreEqual("1a1b1c1d3e1f1g2h1i1j1k1l1m1n4o1p1q2r1s2t2u1v1w1x1y1z", StringLetterCount("The quick brown fox jumps over the lazy dog."));
			Assert.AreEqual("2a1d5e1g1h4i1j2m3n3o3s6t1u2w2y", StringLetterCount("The time you enjoy wasting is not wasted time."));
			Assert.AreEqual("", StringLetterCount("./4592#{}()"));
			Console.WriteLine("Testy OK! ...gratuluji.");
		}
	}
}
