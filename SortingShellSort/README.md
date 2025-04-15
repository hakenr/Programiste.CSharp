# Shell Sort

## Cíl úlohy
Cílem této úlohy je porozumět principu algoritmu [**Shell Sort**](https://cs.wikipedia.org/wiki/Shellovo_%C5%99azen%C3%AD), pochopit jeho výhody i omezení a procvičit si kodérské dovednosti. Shell Sort je efektivnější než běžné jednoduché algoritmy jako Bubble Sort nebo Insertion Sort, přestože nepatří mezi nejrychlejší známé algoritmy.

## Co je Shell Sort a jak funguje?

Shell Sort je vylepšení algoritmu **Insertion Sort**, které funguje na principu porovnávání vzdálených prvků. Klíčová myšlenka spočívá v tom, že pole se nejprve „částečně seřadí“ pomocí určitého **kroku (gap)**, který se postupně snižuje.

Základní schéma algoritmu:

1. Zvolíme počáteční velikost **kroku (gap)** – například polovinu délky pole.
2. Procházíme pole a porovnáváme prvky, které jsou od sebe vzdáleny o aktuální `gap`. Pokud jsou v nesprávném pořadí, prohodíme je.
3. Zmenšíme `gap` (např. na polovinu) a proces opakujeme.
4. Pokračujeme, dokud `gap` není 1 – tehdy už jde v podstatě o běžný Insertion Sort, ale díky předchozím krokům je pole často již téměř seřazené.

Shell Sort **není stabilní algoritmus**, ale jeho časová složitost může být lepší než O(n²), zejména při vhodném výběru posloupnosti kroků.

## Zadání

Implementuj **Shell Sort**.

## Volitelná výzva

1. Vyzkoušej různé **posloupnosti kroků** (`gap sequence`), například:
   - půlení (`n / 2, n / 4, ..., 1`),
   - Hibbardova posloupnost (`1, 3, 7, 15, ...`),
   - Ciura posloupnost (`1, 4, 10, 23, 57, 132, 301, 701, 1750, ...` – používej prefix odpovídající velikosti vstupu).
1. Otestuj algoritmus na různých typech vstupů:
   - náhodně generovaná pole,
   - již seřazená pole,
   - inverzně seřazená pole,
   - pole s duplicitami.
4. Zamysli se nad výkonem algoritmu:
   - jaký vliv má volba posloupnosti kroků?
   - v jakých případech funguje Shell Sort výrazně rychleji než Insertion Sort?