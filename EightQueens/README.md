# Osm dam

**Téma:** Rekurzivní backtracking – hledání kombinace prvků splňujících daná omezení.

Úloha „osm dam“ (anglicky *Eight Queens Puzzle*) je klasický problém z oblasti algoritmizace.  
Cílem je umístit **osm šachových dam na šachovnici 8×8** tak, aby se žádné dvě dámy **navzájem neohrožovaly**.

## Princip problému

Dáma se na šachovnici může pohybovat:
- **vodorovně** (v rámci řádku),
- **svisle** (v rámci sloupce),
- **diagonálně** (oběma směry).

Žádné dvě dámy tedy nesmí být:
- ve stejném řádku,
- ve stejném sloupci,
- ani na stejné diagonále.

## Myšlenka algoritmu

Problém lze řešit metodou **rekurzivního prohledávání s návratem** (*backtracking*).

Postup:
1. Umísti dámu do prvního řádku na libovolný sloupec.
2. Pokus se umístit další dámu do následujícího řádku tak, aby neohrožovala žádnou předchozí.
3. Pokud to není možné, vrať se (backtrack) do předchozího řádku a zkus jiný sloupec.
4. Pokračuj, dokud nebudou všechny dámy umístěny, nebo dokud nevyčerpáš všechny možnosti.

## Zadání

Vaším úkolem je napsat program, který nalezne alespoň jedno řešení problému **osmi dam**.
O jaký grafový algoritmus se jedná? Jaké jsou stavy, přechody a cílové stavy?

## Příklad výstupu

```
. . Q . . . . .
. . . . Q . . .
Q . . . . . . .
. . . . . . Q .
. Q . . . . . .
. . . Q . . . .
. . . . . Q . .
. . . . . . . Q
```

Každý řádek odpovídá jednomu řádku šachovnice, `Q` označuje dámu.


## Tipy k implementaci

- Dámy mohou být reprezentovány polem `int[] sloupce`, kde `sloupce[i]` = číslo sloupce, kam je umístěna dáma v řádku `i`.
- Pro kontrolu diagonál využijte vlastnost:  
  dvě dámy se ohrožují na diagonále, pokud platí  
  `Math.Abs(radek1 - radek2) == Math.Abs(sloupec1 - sloupec2)`.
- Při backtrackingu pomůže, když si vypíšete průběh rekurze pro menší šachovnici (např. 4×4).


## Rozšíření (volitelné)

- Nalezněte **všechna** řešení problému (celkem jich je 92).
- Přidejte parametr `n`, který umožní řešit problém **N dam** (např. 5×5, 6×6, 10×10).
- Spočítejte, kolikrát se test na kolize zavolá při hledání všech řešení.

## Doporučené zdroje
- [Eight Queens Puzzle – Wikipedia](https://en.wikipedia.org/wiki/Eight_queens_puzzle)
- [Backtracking visualization](https://www.cs.usfca.edu/~galles/visualization/Queens.html)
