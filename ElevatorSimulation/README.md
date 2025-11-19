# Simulátor výtahu

**Téma:** Objektově orientované programování, simulace, fronty a algoritmy plánování.

Úloha „Simulátor výtahu“ modeluje provoz výtahu v budově s několika patry.  
Cílem je **navrhnout a implementovat systém**, který obsluhuje požadavky cestujících podle zvolené strategie.

## Princip problému

Výtah je fyzický systém, který:
- má **aktuální patro** (`CurrentElevatorFloor`) a **směr pohybu** (`CurrentElevatorDirection`: nahoru / dolů / nečinný),
- spravuje **čekající požadavky** (`PendingRequests`) – cestující čekající na vyzvednutí,
- spravuje **aktivní cestující** (`ActiveRiders`) – cestující uvnitř výtahu směřující k cílovým patrům.

Každý krok simulace (`TickOneTimeUnit`) představuje jednu časovou jednotku:
- výtah se může **pohnout o jedno patro** (MoveUp/MoveDown),
- výtah může **otevřít dveře** (OpenDoors) – vyložit a naložit cestující,
- výtah může **čekat nečinně** (NoAction),
- během simulace průběžně přicházejí **nové náhodné požadavky**.

## Myšlenka algoritmu

Chování výtahu závisí na zvolené **strategii obsluhy** (`IElevatorStrategy`), která rozhoduje o dalším kroku.

Implementované strategie:
1. **FIFO (First-In-First-Out)** – obsluhuje požadavky v pořadí příchodu:
   - Nejprve vyloží první aktivní cestující, pokud je na cílovém patře
   - Poté vyzvedne čekající cestující z prvního požadavku, pokud je na správném patře
   - Jinak se pohybuje směrem k cíli prvního požadavku/cestujícího

Další strategie lze implementovat rozšířením rozhraní `IElevatorStrategy`.

## Implementované třídy

### Hlavní komponenty
- **`ElevatorSystem`** – řídící třída výtahu s vlastnostmi:
  - `CurrentElevatorFloor` – aktuální patro
  - `CurrentElevatorDirection` – směr pohybu
  - `PendingRequests` – čekající požadavky
  - `ActiveRiders` – cestující ve výtahu
  - `Statistics` – sledování metrik
  
- **`RiderRequest`** – požadavek cestujícího:
  - `From` – výchozí patro
  - `To` – cílové patro
  - `CreatedAt` – čas vytvoření požadavku
  - `PickedUpAt` – čas vyzvednutí
  - `CompletedAt` – čas dokončení

- **`Building`** – konfigurace budovy (min/max patro, validace)

- **`Statistics`** – sledování výkonnosti strategie:
  - Průměrná doba čekání na vyzvednutí
  - Průměrná doba cesty
  - Průměrná celková doba
  - Celkový kumulativní čas (hlavní metrika pro porovnání)

## Příklad výstupu

```
[01] 📞 Request #1: floor 2 → 7
[01] ⬆️  Move up to floor 1
[02] ⬆️  Move up to floor 2
[03] 🚪 Pick up passenger at floor 2 (→ 7)
[04] ⬆️  Move up to floor 3
[05] ⬆️  Move up to floor 4
[06] ⬆️  Move up to floor 5
[07] ⬆️  Move up to floor 6
[08] ⬆️  Move up to floor 7
[09] 🚪 Drop off passenger at floor 7

[20] ✅ Simulation completed

==================================================
SIMULATION STATISTICS
==================================================
Completed requests:      6
Average wait time:       4.33 steps
Average travel time:     5.17 steps
Average total time:      9.50 steps
Total cumulative time:   57 steps
==================================================
```

## Konfigurace simulace

V souboru `Program.cs`:
- `TimeForRequests = 20` – doba generování nových požadavků
- `MaxFloor = 9` – nejvyšší patro budovy (0-9)
- `RequestDensityPercent = 0.30` – pravděpodobnost vytvoření požadavku v každém kroku (30%)
- `RandomSeed = 42017` – seed pro reprodukovatelnost

## Rozšíření projektu

Možnosti dalšího vývoje:
- Implementujte nové strategie (např. směrnou, optimalizační, SCAN)
- Přidejte podporu **více výtahů** koordinovaných dispečerem
- Rozšiřte vizualizaci (grafické zobrazení budovy a výtahů)
- Přidejte omezení kapacity výtahu
- Implementujte prioritní požadavky (např. požární poplach)


## Doporučené zdroje
- [Elevator algorithm – Wikipedia](https://en.wikipedia.org/wiki/Elevator_algorithm)
- [Simulation basics – Wikipedia](https://en.wikipedia.org/wiki/Discrete-event_simulation)
