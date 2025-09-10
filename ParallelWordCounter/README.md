# ParallelWordCounter

Úkolem je z adresáře s `.txt` soubory spočítat četnosti slov a vytisknout **Top N** nejčastějších. 
Vypracujte **dvě varianty**: (1) **sekvenční** (referenční) a (2) **paralelní** s vlákny a zámky. 

## Cíle
- práce s vláknem (`Thread`), `Join`, `lock`
- pochopení **race condition** a korektního zápisu do sdílené struktury
- rozdělení práce mezi vlákna

## Příprava dat
Do složky `data/` umístěte několik větších `.txt` souborů (knihy, logy…). Čím větší data, tím lépe uvidíte rozdíl výkonu. Základní sample-soubory jsou připraveny.

Různě velké textové soubory lze stáhnout například zde: https://www.corpusdata.org/iweb_samples.asp

## Implementace
Využijte připravenou šablonu projektu a implementujte minimálně dvě verze:

### 1) Baseline (sekvenční řešení)
- Projděte soubory jeden po druhém.
- Rozpadněte řádky na slova (tokenizace).
- Počítejte četnosti do `Dictionary<string, int>`.
- Vytiskněte **Top N** (např. 20).
- Změřte čas (`Stopwatch`).

### 2) Paralelní řešení (Thread + lock)
- Rozdělte práci na vlákna, např. 1 vlákno na každý soubor (nebo rozumný počet vláken, viz dále).
- Všechna vlákna zapisují do jedné sdílené paměti `Dictionary<string, int>`.
- Při inkrementu používejte `lock` ke zamezení *race condition* (vyzkoušejte si i verzi bez zámku).
- Změřte čas a porovnejte se sekvenční verzí.

## Dobrovolná vylepšení
- **Map-Reduce** Místo přímého zápisu do sdílené mapy počítejte v lokální mapě (bez zámku) a na konci ji slučte do globální (krátká kritická sekce).
- **Background reporting** ve zvláštním *background* vlákně (periodický stav zpracování, např. každých 500ms počet zpracovaných souborů/slov/rychlost).
- **ConcurrentDictionary** + `AddOrUpdate` (porovnejte výkon se zámky).
- **Producer–Consumer** (fronta práce a pevný pool vláken).
- **Ignorovaná slova** / minimální délka slova, normalizace diakritiky atp.
- **I/O buffering** a volba velikosti bloků.
- **Performance-tuning** Libovolnými vlastními nápady se pokuste celou úlohu optimalizovat a spočítat počet slov co nejrychleji (velikost „šarží“ (batching), počet vláken vs. počet jader).
