# Nejdelší společné části dvou textů (Longest Common Subsequence, LCS)

Představ si, že potřebuješ vytvořit DIFF dvou souborů. Zobrazit, jaké řádky byly přidány, jaké odebrány a jaké zůstaly stejné. Nebo najít společné znaky v sekvenci DNA. V úlohách tohoto typu se používá algoritmus hledání nejdelší společné podposloupnosti (LCS).

## Zadání k procvičení algoritmu LCS
Napiš program, který najde nejdelší společnou podposloupnost řádků mezi dvěma textovými soubory.
Řádky se musí vyskytovat **ve stejném pořadí**, ale **nemusí být za sebou**.

### Vstup  
Dva textové soubory `verzeA.txt` a `verzeB.txt`.

**verzeA.txt:**
```
Start
Console.WriteLine("Hello");
Console.WriteLine("World");
Console.WriteLine("!");
End
```

**verzeB.txt:**
```
Init
Console.WriteLine("World");
Console.WriteLine("Hello");
Console.WriteLine("!");
Finish
```

### Výstup  
Výpis nejdelší společné podposloupnosti řádků:

```
Console.WriteLine("World");
Console.WriteLine("!");
```

### Požadavky
- Implementuj **LCS algoritmus** (Longest Common Subsequence).
- Porovnávej celé řádky (nejen znaky).
- Načítej vstup ze souborů, výstup piš do konzole.

## Jak funguje LCS algoritmus

**LCS – Longest Common Subsequence** znamená *nejdelší společná podposloupnost*.  
Hledáš z daných dvou seznamů (např. řádků souboru) takové řádky, které se vyskytují **v obou** a **ve stejném pořadí**, ale **nemusí být hned za sebou**.

1. **Představ si tabulku**, kde řádky odpovídají prvkům z prvního seznamu (např. řádky ze souboru A) a sloupce druhému seznamu (soubor B).
2. Do každého políčka `dp[i][j]` zapíšeš délku nejdelší společné podposloupnosti pro první `i` řádků z A a první `j` řádků z B.
3. Tabulku vyplňuješ zleva doprava a shora dolů podle těchto pravidel:

   - **Když jsou řádky A[i-1] a B[j-1] stejné:**  
     → přidej 1 k hodnotě vlevo nahoře `dp[i-1][j-1]`.  
     Znamená to: "máme další shodu, prodluž LCS o 1".

   - **Když se řádky neshodují:**  
     → vezmi větší hodnotu z políčka vlevo nebo nahoře  
     (tedy buď bez aktuálního řádku z A, nebo bez řádku z B).  
     Znamená to: "aktuální řádky se neshodují, tak vyber delší LCS z dosud možných cest".

Funkčnost algoritmu si nejlépe prohlédni na vizualizaci https://www.cs.usfca.edu/~galles/visualization/DPLCS.html (zadej S1: "ABCDABCD", S2: "ACDBACDB" a použij variantu "LCS Table").

Výpočet probíhá takto:

```csharp
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        if (A[i] == B[j])
            dp[i + 1, j + 1] = dp[i, j] + 1;
        else
            dp[i + 1, j + 1] = Math.Max(dp[i + 1, j], dp[i, j + 1]);
    }
}
```

### Jak získat samotnou LCS posloupnost (rekonstrukce)

Když máš tabulku `dp`, ve které je pro každý řádek a každý sloupec spočítaná délka nejdelší společné podposloupnosti do toho bodu, můžeš teď **najít konkrétní řádky**, které do LCS patří.

Postup:

1. **Začni v pravém dolním rohu tabulky** – to je výsledek pro celé dva soubory (řádky A i B).
2. **Porovnáváš řádky souborů A a B pozpátku.**
   - Pokud jsou stejné (A[i-1] == B[j-1]), tak **tento řádek patří do LCS** – přidej ho do výsledku a posuň se ↖️ (nahoru a doleva).
   - Pokud stejné nejsou, podívej se, jestli `dp[i-1, j] > dp[i, j-1]`:
     - Ano → posuň se nahoru (i--).
     - Ne → posuň se doleva (j--).
3. **Opakuješ, dokud nedojdeš na začátek tabulky** (horní řádek nebo levý sloupec).
4. Výsledná posloupnost bude pozpátku – tak ji na konci otoč.

## Challenge (volitelné)
Napiš kompletní "DIFF" program, který porovná dva soubory (po řádcích) a vypíše, které řádky byly přidány (prefix `+`), odebrány (prefix (`-`) a které zůstaly stejné (prefix ` `).