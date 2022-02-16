# Unikátní číslo v řadě

Na vstupu je řada čísel. Všechna čísla jsou stejná až na jedno. Najděte ho!

```
1, 2, 2, 2 => 1
1, 2, 1, 1 => 2
-2, 2, 2, 2 => -2
2, 2, 14, 2, 2 => 14
3, 3, 3, 3, 4 => 4
```

Je zaručeno, že na vstupu jsou minimálně tři čísla. Je zaručeno, že vstup odpovídá zadání (např. neobsahuje třetí hodnotu).

Pozor, na vstupu mohou být i velmi velké řady čísel. **Najděte co nejefektivnější postup, jak unikátní číslo co nejdříve objevit.**

(Nepoužívejte LINQ, sestavte algoritmus pomocí primitivních operací - porovnání a přiřazení.)

## Challenge
1. Algoritmus sestavte nad tzv. streamem, tj. v každém kroku algoritmu máte k dispozici vždy jen poslední hodnotu řady, vše ostatní si musíte obstarat sami. Implementovat lze pomocí postupného cyklu s `Console.ReadLine()`, nad `IEnumerable<>`, nebo pomocí třídy `Stream`.
2. Rozšiřte algoritmus o detekci chybných vstupů - třetího různého čísla, malého počtu čísel, atp.
3. Přemýšlejte nad detekcí dvojnásobného výskytu hledaného čísla. Máme dlouho řadu stejných čísel a jedno jiné číslo je v řadě zamíchání 2x. Které to je?

## Inspirace
```csharp
Console.WriteLine(GetUnique(new int[] { 1, 2, 2, 2 }));     // 1
Console.WriteLine(GetUnique(new int[] { 1, 2, 1, 1 }));     // 2
Console.WriteLine(GetUnique(new int[] { -2, 2, 2, 2 }));    // -2
Console.WriteLine(GetUnique(new int[] { 2, 2, 14, 2, 2 })); // 14
Console.WriteLine(GetUnique(new int[] { 3, 3, 3, 3, 4 }));  // 4

int GetUnique(IEnumerable<int> numbers)
{
	int? prevNum = null;
	int? lastNum = null;
	bool diff = false;
	foreach (int n in numbers)
	{
		if (diff && (n == lastNum))
		{
			return prevNum.Value;
		}
		if (n != lastNum)
		{
			if (lastNum != null)
			{
				diff = true;
			}
			if ((prevNum == lastNum) && lastNum.HasValue)
			{
				return n;
			}
			else if (prevNum == n)
			{
				return lastNum.Value;
			}
			else if ((lastNum == n) && prevNum.HasValue)
			{
				return prevNum.Value;
			}
		}
		prevNum = lastNum;
		lastNum = n;
	}
	return -1;
}
```

