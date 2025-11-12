# Simulátor výtahu

**Téma:** Objektově orientované programování, simulace, fronty a algoritmy plánování.

Úloha „Simulátor výtahu“ modeluje provoz výtahu v budově s několika patry.  
Cílem je **navrhnout a implementovat systém**, který obsluhuje požadavky uživatelů na přivolání a jízdu podle zvolené strategie.

## Princip problému

Výtah je fyzický systém, který:
- má **aktuální patro** a **směr pohybu** (nahoru / dolů / stojí),
- má **frontu požadavků** – patra, kam má jet,
- obsluhuje požadavky vzniklé buď **vně** (z pater), nebo **uvnitř výtahu** (zadané cílové patro).

Každý krok simulace představuje určitý časový okamžik:
- výtah se může **pohnout o jedno patro** nebo **obsloužit požadavek**,
- během simulace mohou přicházet **nové požadavky**.

## Myšlenka algoritmu

Chování výtahu závisí na zvolené **strategii obsluhy** požadavků.

Příklady strategií:
1. **Naivní** – vždy jeď k prvnímu požadavku ve frontě.
2. **Směrná (directional)** – nejprve dokonči všechny požadavky v aktuálním směru, pak změň směr.
3. **Optimalizační** – vybírej požadavek, který minimalizuje budoucí čekání (např. nejbližší aktuálnímu patru).

## Zadání

Vaším úkolem je:
1. **Navrhnout třídy** pro model výtahu a požadavků.  
2. **Implementovat simulaci** pohybu výtahu krok po kroku.
3. **Porovnat různé strategie** řízení výtahu podle průměrné doby čekání cestujících.

## Příklad výstupu

```
[00] Request from 2 to 7
[01] Elevator moving up (floor 0 → 1)
[02] Elevator moving up (floor 1 → 2)
[03] Picking up passenger at 2 (→ 7)
[04] Elevator moving up (floor 2 → 3)
[08] Arrived at 7, passenger exits
```

## Tipy k implementaci

- Použijte **třídu** `Elevator` s vlastnostmi `CurrentFloor`, `Direction`, `Requests`.
- Každý krok simulace realizujte metodou `Step()`.
- Vytvořte **generátor požadavků**, který náhodně vytváří nové požadavky.
- Sledujte **metriku** – průměrná doba čekání cestujících.

## Volitelná rozšíření

- Implementujte **více výtahů**.
- Přidejte **vizualizaci**.
- Porovnejte různé strategie a vyhodnoťte efektivitu.


## Doporučené zdroje
- [Elevator algorithm – Wikipedia](https://en.wikipedia.org/wiki/Elevator_algorithm)
- [Simulation basics – Wikipedia](https://en.wikipedia.org/wiki/Discrete-event_simulation)
