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

## 🏆 Turnaj strategií

Aplikace nyní podporuje **turnajový režim**, který automaticky:
- Objeví všechny strategie v projektu
- Spustí každou strategii na více různých scénářích (seedech)
- Agreguje statistiky a vytvoří žebříček výkonnosti
- Seřadí strategie podle průměrné celkové doby (lower is better)

**Jak přidat novou strategii:**
1. Vytvořte novou třídu ve složce `Strategies/`
2. Implementujte `IElevatorStrategy`
3. Strategie se automaticky objeví v turnaji!

Detailní návod: viz [TURNAJ_NAVOD.md](TURNAJ_NAVOD.md)

**Konfigurace turnaje** v `Program.cs`:
- `TournamentMode = true/false` – zapne/vypne turnajový režim
- `TournamentSeeds = { 42017, 12345, 99999, 54321, 77777 }` – pole seedů pro různé scénáře (výchozí: 5 seedů)

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

### Turnajový systém
- **`StrategyTournament`** – správa turnaje strategií:
  - Automatické objevování všech strategií v projektu
  - Spuštění každé strategie na více seedech
  - Agregace statistik přes všechny běhy
  - Seřazení výsledků podle průměrné celkové doby

- **`StrategyResult`** – výsledek jedné strategie v turnaji:
  - Název strategie
  - Agregované statistiky (průměrné časy, počet dokončených požadavků)

## Příklad výstupu

### Režim turnaje
```
=== ELEVATOR SIMULATION ===

🏁 STARTING STRATEGY TOURNAMENT
   Testing with 5 different scenarios (seeds)

📋 Found 2 strategies:
   - FIFO
   - NEARESTFIRST

Running FIFO...
  Seed 1/5 (seed=42017): Completed=9, Avg Total=47,22, Avg Wait=42,56
  Seed 2/5 (seed=12345): Completed=8, Avg Total=35,12, Avg Wait=29,50
  Seed 3/5 (seed=99999): Completed=7, Avg Total=30,00, Avg Wait=26,57
  Seed 4/5 (seed=54321): Completed=3, Avg Total=29,33, Avg Wait=22,67
  Seed 5/5 (seed=77777): Completed=6, Avg Total=31,83, Avg Wait=26,83
  → Overall: Avg Total Time = 36,00

Running NEARESTFIRST...
  Seed 1/5 (seed=42017): Completed=9, Avg Total=34,78, Avg Wait=30,11
  Seed 2/5 (seed=12345): Completed=8, Avg Total=34,12, Avg Wait=28,50
  Seed 3/5 (seed=99999): Completed=7, Avg Total=30,00, Avg Wait=26,57
  Seed 4/5 (seed=54321): Completed=3, Avg Total=27,67, Avg Wait=21,00
  Seed 5/5 (seed=77777): Completed=6, Avg Total=23,33, Avg Wait=19,50
  → Overall: Avg Total Time = 30,00

====================================================================================================
TOURNAMENT RESULTS - RANKED BY AVERAGE TOTAL TIME
====================================================================================================

Rank   Strategy             Avg Total    Avg Wait     Avg Travel   Completed 
----------------------------------------------------------------------------------------------------
1      NEARESTFIRST         30,00        26,00        4,00         33        
2      FIFO                 36,00        31,00        5,00         33        
====================================================================================================

🏆 WINNER: NEARESTFIRST with average total time of 30,00 steps
```

### Režim jedné strategie
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

### Základní parametry
- `TimeForRequests = 20` – doba generování nových požadavků
- `MaxFloor = 9` – nejvyšší patro budovy (0-9)
- `RequestDensityPercent = 0.30` – pravděpodobnost vytvoření požadavku v každém kroku (30%)
- `RandomSeed = 42017` – seed pro reprodukovatelnost (používá se v single mode)

### Turnajový režim
- `TournamentMode = true` – zapne turnajový režim (false = test jedné strategie)
- `TournamentSeeds = { 42017, 12345, 99999, 54321, 77777 }` – pole seedů pro různé scénáře

## Jak přidat novou strategii

1. Vytvořte novou třídu ve složce `Strategies/`
2. Implementujte rozhraní `IElevatorStrategy`
3. Implementujte metodu `DecideNextMove(ElevatorSystem elevator)`
4. Strategie bude automaticky objevena a zařazena do turnaje

Příklad:
```csharp
namespace ElevatorSimulation.Strategies;

public class MojeStrategieStrategy : IElevatorStrategy
{
    public MoveResult DecideNextMove(ElevatorSystem elevator)
    {
        // Váš algoritmus zde
        // Máte přístup k:
        // - elevator.CurrentElevatorFloor
        // - elevator.PendingRequests
        // - elevator.ActiveRiders
        // - elevator.Building
        
        return MoveResult.NoAction; // nebo MoveUp, MoveDown, OpenDoors
    }
}
```

Strategie se automaticky objeví v turnaji pod názvem "MOJESTRATEGIE" (bez přípony "Strategy").

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
