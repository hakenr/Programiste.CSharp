# Parallel Password Hasher — Zadání (CPU-bound, Tasks/Parallel)

Vyzkoušíme si paralelizace práce na výpočtech hashe pro velkou sadu hesel (řetězců).


### 1) Baseline: sekvenční verze (hotová ve starteru)
- Pro každé heslo spočtěte PBKDF2 (pevný salt, např. `"SemPrgX-salt-01"`, 32B výstup).
- Výsledek uložte jako Base64 do `Dictionary<string, string>`.
- Změřte čas (`Stopwatch`).

### 2) Paralelní verze — Threads (TODO)
- Rozdělte práci na části (např. podle počtu jader).
- Pozor, že `Dictionary` není thread-safe pro zápis → použijte `lock` nebo `ConcurrentDictionary`.
- Seznamte se s `Thread`, `Start()`, `Join()`.

### 3) Paralelní verze — Tasks + WhenAll (TODO)
- Stejné dělení na části jako u Threads.
- Každou část počítejte přes **`Task.Run`** (CPU-bound → ThreadPool).
- Sesbírejte výsledky přes **`await Task.WhenAll(...)`** nebo `Task.WhenEach(...)`

### 4) Paralelní verze — Parallel.ForEach (TODO)
- Použijte `Parallel.ForEach(passwords, p => { ... })`.
- Prozkoumejte options - např. `MaxDegreeOfParallelism` .

### 5) Kontrola shody výsledků
- Ověřte, že slovníky z Threads/Tasks/Parallel dávají **stejné** výsledky jako sekvenční.
- Stačí porovnat, že pro každý klíč je totožná Base64 hodnota.

## Co měřit a zkoumat
- Dle výkonu PC, zvolte hodnoty `Iterations` nebo `PasswordCount`.
- „1 task na každé heslo“ nedělejte — je to zbytečný overhead. Dělte na větší chunky.
- Prozkoumejte vliv velikosti chunků (počet hesel na jeden Task/Thread).
- Všímejte si, kdy už přidávání paralelismu nepomáhá.
- Pozorujte warm-up ThreadPoolu, zkuste úlohy opakovat vícekrát.