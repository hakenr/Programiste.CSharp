# QuickSort

## Cíl úlohy
Cílem této úlohy je porozumět principu algoritmu **QuickSort**, naučit se ho implementovat v jazyce **C#** a pochopit jeho silné i slabé stránky. QuickSort patří mezi efektivní řadící algoritmy, které se používají i v praxi.

## Co je QuickSort a jak funguje?

QuickSort je efektivní řadící algoritmus, který využívá strategii **rozděl a panuj** (*divide and conquer*). Základní schéma algoritmu:

1. **Vybereme prvek z pole jako pivot** (např. první, poslední, náhodný, nebo "medián ze tří" - prozkoumejte si, proč na strategii záleží).
2. **Rozdělíme** pole na dvě části – levá bude obsahovat prvky **menší než pivot**, pravá pak prvky **větší než pivot** (nebo rovné, podle zvolené strategie).
3. **Rekurzivně** aplikujeme QuickSort na obě části.
4. Pole se tímto způsobem **postupně rozpadá** na menší části, které se následně **spojí do výsledného seřazeného pole**.

## Co znamená "rozděl a panuj"?

"Rozděl a panuj" (*divide and conquer*) je obecná strategie řešení problémů, která zahrnuje tři kroky:

1. **Rozdělení** složitého problému na menší dílčí podproblémy.
2. **Vyřešení** každého podproblému (často rekurzivně).
3. **Spojení** dílčích řešení do celkového výsledku.


## Zadání

1. Implementuj **rekurzivní verzi** algoritmu QuickSort v jazyce C#.
2. Otestuj funkčnost algoritmu na různých vstupech:
   - náhodně generovaná pole,
   - již seřazená pole,
   - inverzně seřazená pole,
   - pole s duplicitními hodnotami.
3. Přemýšlej nad tím, kdy algoritmus pracuje efektivně (**O(n log n)**) a kdy může selhávat na složitost **O(n²)**. Jakou roli v tom hraje výběr pivota?


## Volitelná výzva

- Implementuj **nerekurzivní verzi** QuickSortu.
- Vyzkoušej různé strategie výběru pivota a jejich vliv na výkon algoritmu.
- Porovnej různé varianty pomocí knihovny **BenchmarkDotNet**.
- Porovnej svůj algoritmus s vestavěnou metodou `Array.Sort`.