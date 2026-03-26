# Turnaj výtahových strategií - Návod pro studenty

## Co je to turnaj?

Turnaj automaticky:
1. Najde všechny strategie v projektu (t?ídy implementující `IElevatorStrategy`)
2. Spustí každou strategii na 5 r?zných scéná?ích (seedech)
3. Spo?ítá pr?m?rné statistiky pro každou strategii
4. Se?adí strategie podle **pr?m?rné celkové doby** (?ím nižší, tím lepší)
5. Vypíše tabulku s výsledky

## Jak p?idat svou strategii

### Krok 1: Vytvo?te nový soubor
Ve složce `Strategies/` vytvo?te nový soubor, nap?íklad `MojeStrategieStrategy.cs`

### Krok 2: Implementujte rozhraní
```csharp
namespace ElevatorSimulation.Strategies;

/// <summary>
/// Popis vaší strategie zde.
/// </summary>
public class MojeStrategieStrategy : IElevatorStrategy
{
    public MoveResult DecideNextMove(ElevatorSystem elevator)
    {
        // TODO: Implementujte sv?j algoritmus
        
        return MoveResult.NoAction;
    }
}
```

### Krok 3: Implementujte logiku rozhodování

Máte p?ístup k t?mto informacím:

```csharp
// Aktuální patro výtahu (int)
elevator.CurrentElevatorFloor

// Cestující ?ekající na vyzvednutí (List<RiderRequest>)
elevator.PendingRequests
// Pro každý požadavek: request.From, request.To, request.CreatedAt

// Cestující uvnit? výtahu (List<RiderRequest>)
elevator.ActiveRiders
// Pro každý požadavek: request.To (cílové patro)

// Konfigurace budovy
elevator.Building.MinFloor  // obvykle 0
elevator.Building.MaxFloor  // obvykle 9

// Aktuální ?as simulace
elevator.CurrentTime
```

Musíte vrátit jednu z t?chto akcí:
- `MoveResult.MoveUp` – je? o patro nahoru
- `MoveResult.MoveDown` – je? o patro dol?
- `MoveResult.OpenDoors` – otev?i dve?e (vyzvedni/vylo? cestující)
- `MoveResult.NoAction` – ne?innost (?ekej)

### Krok 4: Spus?te turnaj
```bash
dotnet run
```

Vaše strategie se automaticky objeví v turnaji!

## P?íklad: Jednoduchá strategie "Nearest First"

```csharp
namespace ElevatorSimulation.Strategies;

public class NearestFirstStrategy : IElevatorStrategy
{
    public MoveResult DecideNextMove(ElevatorSystem elevator)
    {
        // Nejd?ív vyložíme cestující, pokud jsou na cílovém pat?e
        if (elevator.ActiveRiders.Count > 0)
        {
            // Najdeme nejbližší cíl
            var closestDestination = elevator.ActiveRiders
                .Select(r => r.To)
                .OrderBy(floor => Math.Abs(floor - elevator.CurrentElevatorFloor))
                .First();

            // Jedeme k n?mu
            if (closestDestination > elevator.CurrentElevatorFloor)
                return MoveResult.MoveUp;
            if (closestDestination < elevator.CurrentElevatorFloor)
                return MoveResult.MoveDown;
            return MoveResult.OpenDoors;
        }

        // Pokud nemáme cestující, jedeme k nejbližšímu ?ekajícímu
        if (elevator.PendingRequests.Count > 0)
        {
            var nearestRequest = elevator.PendingRequests
                .OrderBy(r => Math.Abs(r.From - elevator.CurrentElevatorFloor))
                .First();

            if (nearestRequest.From > elevator.CurrentElevatorFloor)
                return MoveResult.MoveUp;
            if (nearestRequest.From < elevator.CurrentElevatorFloor)
                return MoveResult.MoveDown;
            return MoveResult.OpenDoors;
        }

        // Nic ned?lat
        return MoveResult.NoAction;
    }
}
```

## Tipy pro dobrou strategii

1. **Prioritizujte aktivní cestující** – Lidé ve výtahu by m?li být doru?eni rychle
2. **Minimalizujte zbyte?né pohyby** – Nepojížd?jte sem a tam bez d?vodu
3. **Skupinujte požadavky** – Pokud jedete nahoru, vyzvedn?te všechny cestující po cest?
4. **Zvažte sm?r pohybu** – Udržujte konzistentní sm?r dokud to dává smysl
5. **Testujte r?zné p?ístupy** – N?kdy jednodušší strategie je lepší!

## Hodnocení

Hlavní metrika: **Average Total Time** (pr?m?rná celková doba)
- M??í pr?m?rnou dobu od vytvo?ení požadavku do doru?ení cestujícího
- Nižší hodnota = lepší strategie

Další metriky:
- **Average Wait Time** – pr?m?rná doba ?ekání na vyzvednutí
- **Average Travel Time** – pr?m?rná doba cesty ve výtahu
- **Completed** – po?et dokon?ených požadavk? (m?lo by být stejné pro všechny)

## Testování

### Turnajový režim (výchozí)
Spustí všechny strategie na více scéná?ích:
```bash
dotnet run
```

### Test jedné strategie
V `Program.cs` nastavte:
```csharp
public const bool TournamentMode = false;
```

Pak v `Main()` odkomentujte ?ádek s vaší strategií:
```csharp
RunSimulation("MOJE STRATEGIE", new MojeStrategieStrategy(), building, seed: RandomSeed);
```

## ?asto kladené otázky

**Q: Jak zjistím, jestli jsem na správném pat?e pro vyzvednutí/vyložení?**  
A: Zavoláním `MoveResult.OpenDoors` se automaticky vyzvednou/vyloží všichni relevantní cestující na aktuálním pat?e.

**Q: Co se stane, když se pokusím jet mimo budovu?**  
A: Použijte `elevator.Building.IsValidFloor(floor)` pro kontrolu.

**Q: M?žu použít vlastní prom?nné/stav?**  
A: Ano! Strategie je t?ída, m?žete mít vlastní fieldy pro sledování stavu.

**Q: Jak vím, jestli moje strategie je dobrá?**  
A: Porovnejte ji s ostatními v turnaji. Ideáln? by m?la být v TOP 3!

**Q: Kolik strategií m?žu p?idat?**  
A: Neomezen?! Všechny se automaticky objeví v turnaji.

## D?ležité poznámky

- Každý krok (move/openDoors/noAction) trvá 1 ?asovou jednotku
- Požadavky p?icházejí náhodn? b?hem prvních 20 krok?
- Simulace kon?í, když jsou všichni cestující doru?eni
- Turnaj b?ží každou strategii 5x s r?znými seedy pro spravedlivé porovnání
- Název vaší strategie v turnaji bude název t?ídy bez p?ípony "Strategy" (nap?. `MojeStrategieStrategy` ? `MOJESTRATEGIE`)

## Úsp?ch!

Hodn? št?stí s vývojem vaší strategie! ??

Pokud máte otázky, koukn?te do existujících strategií (`FifoStrategy.cs`, `NearestFirstStrategy.cs`) jako p?íklady.
