# Vyhledávání vzoru pomocí KMP algoritmu

## Úvod
Knuth-Morris-Pratt (KMP) algoritmus je efektivní způsob vyhledávání podřetězce (vzoru) v textu (streamu). Oproti jednoduchému algoritmu, který při každé neshodě začíná hledání znovu od následujícího znaku, KMP využívá speciální předzpracování vzoru. Toto předzpracování vytváří tzv. prefixovou tabulku (LPS - Longest Prefix Suffix), která pomáhá algoritmu přeskočit nepotřebné kontroly znaků.

**Časová složitost:**
- Naivní algoritmus: **O(n·m)**, kde *n* je délka textu a *m* délka vzoru.
- KMP algoritmus: **O(n + m)**, tedy výrazně efektivnější.

### Princip LPS tabulky

Prefixová tabulka zaznamenává, jak dlouhá je nejdelší předpona vzoru, která je současně jeho příponou, avšak ne celý vzor. Díky tomu se při neshodě posuneme ve vzoru o maximální možný počet znaků dopředu.

Například pro vzor:
```
Vzor: "ABABCABAB"
Index: 012345678
```

Tabulka LPS vypadá následovně:

| Index vzoru | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |
|-------------|---|---|---|---|---|---|---|---|---|
| Znak vzoru  | A | B | A | B | C | A | B | A | B |
| LPS         | 0 | 0 | 1 | 2 | 0 | 1 | 2 | 3 | 4 |

Vysvětlení některých hodnot:
- Pro index 3 („ABAB“): nejdelší shoda prefix-suffix je „AB“ (délka 2).
- Pro index 8 („ABABCABAB“): nejdelší shoda prefix-suffix je „ABAB“ (délka 4).

## Zadání úlohy

Tvým úkolem je implementovat algoritmus KMP v jazyce C#, který:

1. Vytvoří prefixovou tabulku (LPS) pro zadaný vzor.
2. Použije tuto tabulku k efektivnímu vyhledání všech výskytů vzoru v textu.

## Požadavky

Napiš dvě funkce:

- `int[] ComputeLPS(string pattern)` – vrátí prefixovou tabulku pro daný vzor.
- `List<int> KMPSearch(string text, string pattern)` – vrátí seznam všech indexů v textu, kde se vzor nachází.

## Ukázkový vstup a výstup

### Příklad 1:

**Vstup:**
```csharp
string text = "ABABDABACDABABCABAB";
string pattern = "ABABCABAB";
```

**Výstup:**
```csharp
[10]
```

### Příklad 2:

**Vstup:**
```csharp
string text = "AABAACAADAABAABA";
string pattern = "AABA";
```

**Výstup:**
```csharp
[0, 9, 12]
```
