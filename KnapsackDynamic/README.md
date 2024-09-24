# Zloděj v klenotnictví - Dynamické programování

Úlohu [Zloděj v klenotnictví](https://github.com/hakenr/Programiste.CSharp/tree/master/KnapsackGreedy#readme) lze řešit prostřednictvím dynamického programování.
Řešení dynamickým programováním vede na rozdíl od greedy algoritmů na celkově optimální řešení, ale je výpočetně i implementačně náročnější.

## Dynamické programování
Dynamické programování je metoda optimalizace, která řeší problémy tím, že je rozděluje na menší podproblémy, které se postupně řeší a jejich výsledky se ukládají _(memoizace)_ pro budoucí použití. Tím se zabrání opakovanému řešení stejných podproblémů, což výrazně zlepšuje časovou složitost algoritmu.

Principy dynamického programování:
1. **Optimalizační princip** – celková optimální řešení problému závisí na optimálních řešeních jeho podproblémů.
2. **Překrývající se podproblémy** – velký problém se dá rozložit na několik menších, často se opakujících podproblémů.
3. **Memoizace/Tabulace** – výsledky těchto podproblémů se ukládají (do tabulky nebo jiné struktury) pro pozdější použití, čímž se sníží počet výpočtů.

Typický příklad dynamického programování je výpočet [Fibonacciho čísel](https://cs.wikipedia.org/wiki/Fibonacciho_posloupnost), `fib(n) = fin(n-1) + fib(n-2)`. Bez optimalizace by byl rekurzivní algoritmus velmi neefektivní, protože by opakovaně počítal stejné hodnoty. Pomocí dynamického programování (memoizace) lze výsledky průběžně ukládat a znovu použít, což dramaticky snižuje počet operací.

```csharp
int Fibonacci(int n)
{
    if (n <= 1) return n;

    int[] fib = new int[n + 1];
    fib[0] = 0;
    fib[1] = 1;

    for (int i = 2; i <= n; i++)
    {
        fib[i] = fib[i - 1] + fib[i - 2];
    }

    return fib[n];
}
```

Dynamické programování je často používáno pro řešení optimalizačních problémů, například náš problém batohu (knapsack problem), úpravu řetězců (edit distance), nebo hledání nejkratší cesty v grafu.

## Řešení úlohy pomocí dynamického programování:

### Definice problému:
- Máme seznam šperků, z nichž každý má objem a cenu.
- Máme tašku s danou kapacitou.
- Chceme vybrat šperky tak, aby celková cena byla co největší a celkový objem nepřekročil kapacitu.

### Postup:
- Vytvoříme 2D pole `dp[i][w]`, kde `i` je index šperku a `w` je kapacita tašky.
- Hodnota `dp[i][w]` bude představovat maximální cenu, kterou lze získat z prvních `i` šperků s kapacitou `w`.
- Pro každý šperk máme dvě možnosti:
    1. **Nevzít šperk**: Hodnota zůstane stejná jako pro předchozí šperky, tedy `dp[i][w] = dp[i-1][w]`.
    2. **Vzít šperk**: Přidáme cenu šperku a zmenšíme kapacitu tašky, tedy `dp[i][w] = dp[i-1][w - objem šperku] + cena šperku`, pokud se šperk vejde.

### Konečný výsledek:
Nejvyšší hodnota v matici `dp` pro všechny šperky a maximální kapacitu tašky bude v buňce `dp[n][W]`, kde `n` je počet šperků a `W` je kapacita tašky.

## Zdroje
- [Algoritmy.net: Problém batohu](https://www.algoritmy.net/article/5521/Batoh)
- [KSP: Dynamické programování](https://ksp.mff.cuni.cz/kucharky/programatorske-kucharky/09-dynamicke-programovani.pdf)

## Shrnutí
Řešení pomocí dynamického programování je **vždy optimální** pro tento problém. Důvodem je, že dynamické programování zkoumá všechny možné kombinace šperků v rámci dané kapacity tašky a ukládá mezivýsledky, čímž zajišťuje, že žádná kombinace není vynechána.

### Proč je řešení optimální?
1. **Rekurentní vztah**: Každá hodnota v tabulce DP (`dp[i][w]`) představuje nejlepší možné řešení pro prvních `i` šperků a kapacitu `w`. Řešení je založeno na dvou možnostech pro každý šperk – zahrnout jej nebo ne. Dynamické programování zohledňuje obě možnosti a ukládá tu lepší, což zajišťuje, že získáme maximální možnou cenu.
   
2. **Systematické prohledávání**: Algoritmus se rozhoduje pro každý šperk na základě maximální hodnoty, kterou lze získat pro daný objem tašky. Tím pokrývá všechny možné kombinace a žádné potenciálně lepší řešení není vynecháno.

3. **Rekonstrukce řešení**: Po naplnění DP tabulky můžeme přesně zjistit, které šperky byly do optimálního řešení zahrnuty, a to zpětnou analýzou tabulky. Tento proces zajistí, že žádný výhodnější šperk nebude vynechán.

### Srovnání
- **Greedy algoritmus** se snaží vybírat šperky na základě lokální optimalizace (např. poměr ceny k objemu), což může vést k suboptimálním řešením, protože nedokáže vždy zohlednit všechny možné kombinace. Greedy řešení bývá snadno implementovatelné, rychlé a může být v určitých případech blízko optimálnímu, ale není zaručené, že bude vždy nejlepší.
- **Dynamické programování** naproti tomu prohledává všechny možné kombinace šperků systematicky a zaručuje, že nalezené řešení bude globálně optimální.