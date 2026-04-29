# Producer–Consumer (Async)

## Zadání

Implementuj jednoduchý systém se dvěma rolemi:

* **Producer** generuje čísla `1..N`
* **Consumer** je postupně zpracovává (např. vypisuje na konzoli)

Mezi nimi je sdílený **buffer (fronta)**.

---

## Cíl

* pochopit problém **Producer–Consumer**
* zajistit **thread-safe přístup ke sdíleným datům**
* odstranit **busy waiting**
* seznámit se s **async přístupem ke konzumentovi**

---

## 1) Naivní řešení

Začni implementací bez synchronizace:

* sdílená `Queue<int>`
* producer přidává prvky
* consumer je odebírá v nekonečné smyčce

### Otázky

* Jaké problémy může řešení mít?
* Co se stane při souběžném přístupu?
* Jaké jsou dopady na CPU?

---

## 2) Synchronizace pomocí `lock`

Uprav řešení tak, aby byl přístup ke frontě bezpečný:

* použij `lock`
* zajisti, že enqueue/dequeue je atomické

### Otázky

* Je řešení korektní?
* Je efektivní?
* Co dělá consumer, když je fronta prázdná?

---

## 3) Čekání místo aktivního cyklu

Odstraň busy waiting:

* použij `Monitor.Wait` / `Monitor.Pulse`
* consumer čeká, když je fronta prázdná
* producer probouzí consumera

### Otázky

* Proč je `while` nutné místo `if`?
* Jaký je rozdíl oproti předchozí variantě?

---

## 4) Async consumer

Převeď consumera na **asynchronní variantu**:

* použij `SemaphoreSlim`
* místo `Wait()` použij `WaitAsync()`

### Požadavky

* consumer nesmí blokovat thread při čekání
* producer signalizuje dostupná data

### Otázky

* Jaký je rozdíl mezi `Wait()` a `WaitAsync()`?
* Kdy je async řešení výhodné?

---

## 5) Bonus – moderní řešení

Použij `Channel<T>`:

* producer zapisuje pomocí `Writer`
* consumer čte pomocí `await foreach`

### Otázky

* V čem je toto řešení jednodušší?
* Jaké problémy řeší za tebe?

---

## Rozšíření

* více producentů a konzumentů
* omezená kapacita bufferu
* ukončení consumera po dokončení práce
* měření výkonu jednotlivých variant

---

## Očekávaný výstup

Program vypíše čísla `1..N` v konzoli (pořadí nemusí být garantováno u více vláken).

---

## Poznámky

* Zaměř se na **správnost i efektivitu**
* Sleduj rozdíl mezi:

  * blokováním threadu
  * asynchronním čekáním
* Nepoužívej hotová řešení (`BlockingCollection`, `Channel`) dříve než v bonusové části
