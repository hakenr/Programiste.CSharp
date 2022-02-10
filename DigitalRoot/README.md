# Digital Root - Číslicový kořen (suma číslic)

## Úloha
Mějme na vstupu číslo *n*.
Vytvoříme součet jeho číslic.
Pokud je tento součet víceciferný, opakujeme celý proces a opět sečteme číslice.
Pokračujeme takto sčítáním číslic, dokud výsledek není jednociferný.

Příklady:
```
    16  -->  1 + 6 = 7
   942  -->  9 + 4 + 2 = 15  -->  1 + 5 = 6
132189  -->  1 + 3 + 2 + 1 + 8 + 9 = 24  -->  2 + 4 = 6
493193  -->  4 + 9 + 3 + 1 + 9 + 3 = 29  -->  2 + 9 = 11  -->  1 + 1 = 2
```

## Rekurze
*Rekurzivní algoritmy* jsou takové algoritmy, v jejichž postupu je podprogram, který volá sám sebe dříve, než je dokončeho jeho předchozí volání.

Každý rekurzivní algoritmus potřebuje kromě volání sama sebe i nějakou "zarážku" - podmínku, za které se další volání sama sebe neprovede a další rekurze se zastaví.

Každý rekurzivní algoritmus lze převést do iterativní podoby (cyklus), rekurzivní algoritmy bývají však oblíbené pro jejich přehlednost.

## Challenges (volitelné)
1. Umožněte volbu číselní soustavy (desítková, šestnáctková, dvojková, atp.)
2. Přemýšlejte nad podobou, která zvládne na vstupu velmi vysoké číslo, např. 100 číslic.
2. Pokud jste algoritmus implementovali cyklem, vyzkoušete vytvořit jeho rekurzivní variantu a porovnejte je. A naopak, pokud jste pro implementaci použili rekurzi, implementujte algoritmus cyklem.
3. Zjistěte, jestli existuje nějaký matematický postup (vzorec), nebo jiná možnost, jak celý výpočet zefektivnit.

## Inspirace
```csharp
Console.WriteLine("Zadejte číslo:");
var input = Console.ReadLine();


var output = DigitalRoot1_StringBased(input);
Console.WriteLine($"Digital root: {output}");


var output2 = DigitalRoot2_Numeric(Convert.ToInt64(input));
Console.WriteLine($"Digital root: {output2}");

string DigitalRoot1_StringBased(string number)
{
	if (number.Length == 1)
	{
		return number;
	}
	long sum = 0;
	foreach (char digit in number)
	{
		if (Char.IsDigit(digit))
		{
			sum = sum + (int)Char.GetNumericValue(digit);
		}
	}
	return DigitalRoot1_StringBased(sum.ToString());
}

long DigitalRoot2_Numeric(long number)
{
	if (number < 10)
	{
		return number;
	}
	long sum = 0;
	do
	{
		sum = sum + number % 10;
		number = number / 10;
	}
	while (number > 1);
	sum = sum + number;

	return DigitalRoot2_Numeric(sum);
}
```

