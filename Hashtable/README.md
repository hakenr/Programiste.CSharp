# Hashovací tabulka (Hashtable)

## Cíl úlohy
Cílem této úlohy je porozumět principu **hashovací tabulky (Hashtable)**, naučit se ji implementovat v jazyce **C#**, pochopit její využití a význam správné implementace hashovací funkce a řešení kolizí.

## Co je hashovací tabulka a jak funguje?

**Hashovací tabulka** je efektivní datová struktura, která umožňuje rychlé vyhledávání, vkládání a odstraňování položek (v průměrném případě v konstantním čase O(1)).

Základní schéma fungování hashovací tabulky:

1. **Hashovací funkce** převede klíč položky na číselnou hodnotu (hash).
2. Číselná hodnota (hash) určí index v interním poli, kde bude položka uložena.
3. Pokud dojde ke kolizi (dva různé klíče mají stejný hash), je třeba kolizi řešit, nejčastěji pomocí:
   - **Metody řetězení (chaining)**: každý index pole obsahuje seznam položek.
   - **Metody otevřené adresace (open addressing)**: nalezení jiné volné pozice podle určité strategie (lineární či kvadratické sondování).

## Co je kolize a jak se řeší?

Kolize vzniká, když dva různé klíče mají stejný výsledek hashovací funkce. Typická řešení kolizí jsou:

- **Chaining (řetězení)**: Každý prvek v poli hashovací tabulky obsahuje seznam hodnot. V případě kolize jednoduše přidáme novou položku na konec seznamu.
- **Open Addressing**: Pokud je místo obsazené, hledáme další volné místo podle daného pravidla (lineární, kvadratické, dvojité hashování).

## Zadání

1. Implementuj hashovací tabulku v jazyce C#:
   - Doporučujeme použít metodu řešení kolizí **chaining** (seznam pro každý index).
   - Implementuj základní operace: **Insert**, **Search** a **Delete**.

2. Využij existující metodu `GetHashCode()` v C# pro generování hashovacího kódu z klíče typu `string`. Pro získání indexu pole využij operaci modulo (`%`), například:

```csharp
int index = key.GetHashCode() % array.Length;
```

3. Otestuj funkčnost hashovací tabulky na různých sadách dat:
   - Vlož a následně vyhledej různé hodnoty.
   - Vyzkoušej chování tabulky při větším počtu kolizí (například pomocí malého pole interní tabulky).

4. Zhodnoť efektivitu své implementace:
   - Jak ovlivňuje počet kolizí dobu hledání?
   - Jak ovlivňuje velikost pole interní tabulky výkon?

## Volitelná výzva

- Vyzkoušej vestavěnou implementaci `Dictionary<TKey, TValue>` v .NET a porovnej její výkon se svou hashovací tabulkou pomocí knihovny **BenchmarkDotNet**.
- Implementuj metodu pro automatické zvětšení velikosti hashovací tabulky, pokud dojde k překročení určitého poměru obsazenosti (např. 0.7). Při zvětšení tabulky přehashuj všechny existující položky.
