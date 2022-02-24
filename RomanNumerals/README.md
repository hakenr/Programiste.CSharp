# Římské číslice

Římské číslice představují způsob zápisu čísel pomocí několika vybraných písmen latinské abecedy ([Wikipedia](https://cs.wikipedia.org/wiki/%C5%98%C3%ADmsk%C3%A9_%C4%8D%C3%ADslice)).

Naprogramujte převodník římských čísel (`string`) na číselnou podobu (`int`).

Otestujte svůj algoritmus minimálně na následujících příkladech:
```csharp
Console.WriteLine(calculator.RomanToArabic("I")); // 1
Console.WriteLine(calculator.RomanToArabic("VIII")); // 8
Console.WriteLine(calculator.RomanToArabic("XXI")); // 21
Console.WriteLine(calculator.RomanToArabic("CLXXIII")); // 173
Console.WriteLine(calculator.RomanToArabic("MDCLXVI")); // 1666
Console.WriteLine(calculator.RomanToArabic("MDCCCXXII")); // 1822
Console.WriteLine(calculator.RomanToArabic("MCMLIV")); // 1954
Console.WriteLine(calculator.RomanToArabic("MCMXC")); // 1990
Console.WriteLine(calculator.RomanToArabic("MMVIII")); // 2008
Console.WriteLine(calculator.RomanToArabic("MMXIV")); // 2014
```

## Challenges
1. Implementujte jako třídu, která budou funkčnost zapouzdřovat.
2. Vytvořte i opačný směr převodu, z číselné hodnoty `int` na římskou podobu `string`.
3. Vytvořte normalizátor zápisu, který převede libovolný římský vstup na standardní podobu, např. z `IIX` na `VIII`, nebo `IIII` na `IV`, atp.

## Inspirace
```csharp
var calculator = new RomanNumeralsCalculator();

Console.WriteLine(calculator.RomanToArabic("I")); // 1
Console.WriteLine(calculator.RomanToArabic("VIII")); // 8
Console.WriteLine(calculator.RomanToArabic("XXI")); // 21
Console.WriteLine(calculator.RomanToArabic("CLXXIII")); // 173
Console.WriteLine(calculator.RomanToArabic("MDCLXVI")); // 1666
Console.WriteLine(calculator.RomanToArabic("MDCCCXXII")); // 1822
Console.WriteLine(calculator.RomanToArabic("MCMLIV")); // 1954
Console.WriteLine(calculator.RomanToArabic("MCMXC")); // 1990
Console.WriteLine(calculator.RomanToArabic("MMVIII")); // 2008
Console.WriteLine(calculator.RomanToArabic("MMXIV")); // 2014


public class RomanNumeralsCalculator
{
	private Dictionary<char, int> charValues;

	public RomanNumeralsCalculator()
	{
		charValues = new Dictionary<char, int>();
		charValues.Add('I', 1);
		charValues.Add('V', 5);
		charValues.Add('X', 10);
		charValues.Add('L', 50);
		charValues.Add('C', 100);
		charValues.Add('D', 500);
		charValues.Add('M', 1000);
	}

	public int RomanToArabic(string roman)
	{
		if (roman.Length == 0) return 0;
		roman = roman.ToUpper();

		int total = 0;
		int lastValue = 0;
		for (int i = roman.Length - 1; i >= 0; i--)
		{
			int new_value = charValues[roman[i]];

			// přičítáme nebo odečítáme?
			if (new_value < lastValue)
			{
				total -= new_value;
			}
			else
			{
				total += new_value;
				lastValue = new_value;
			}
		}

		return total;
	}
}
```

