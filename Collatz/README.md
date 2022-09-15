# Collatzův problém

Viz [Collatzův problém na Wikipedii](https://cs.wikipedia.org/wiki/Collatz%C5%AFv_probl%C3%A9m).

## Úkol
Vytvořte program, který ze zadaného celého kladného čísla generuje posloupnost čísel následujícím postupem:
* Je-li číslo sudé, vyděl ho dvěma.
* Je-li naopak číslo liché, vynásob ho třemi a přičti jedničku.
* Tento postup opakuj tak dlouho, až se dostaneš k hodnotě 1.

Dále vypíše, kolik výpočtů bylo třeba učinit, než se k číslu 1 dostane.

Zadá-li tedy uživatel např. číslo 8, vznikne posloupnost 8, 4, 2, 1, tj. budou třeba 3 výpočty (z 8 na 4, ze 4 na 2 a z 2 na 1). Zadá-li uživatel např. číslo 9, vznikne posloupnost 9, 28, 14, 7, 22, 11, 34, 17, 52, 26, 13, 40, 20, 10, 5, 16, 8, 4, 2, 1, která ke svému vygenerování potřebuje 19 kroků.

### Rozšíření
1. Zjistěte, pro které z čísel v rozmezí 2 až 1000 je vygenerovaná posloupnost nejdelší (tj. je třeba učinit nejvíc kroků).
2. Zjistěte nejmenší číslo, pro které je potřeba k vygenerování posloupnosti učinit alespoň 500 kroků.
3. Jak je z ilustračního příkladu v zadání vidět, pro zadané číslo 9 je ve vygenerované posloupnosti největší hodnotou 52. Najděte, které z čísel 2 až 100000 má ve své posloupnosti absolutně nejvyšší hodnotu ze všech a jaká hodnota to je. 
4. Najděte nejmenší tři bezprostředně po sobě jdoucí čísla taková, že k vygenerování posloupností z nich je zapotřebí stejný počet kroků.

## Inspirace
```csharp
int number = 9;
Console.WriteLine(number);

int step = 0;

while (number != 1)
{
	if (number % 2 == 0)
	{
		number = number / 2;
	}
	else
	{
		number = number * 3 + 1;
	}

	Console.WriteLine(number);

	step++;
}

Console.WriteLine("Number of steps: " + step);
```