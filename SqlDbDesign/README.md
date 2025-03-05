# Návrh relační databáze pro městskou knihovnu

#### **Cíl úlohy:**

Navrhněte a **realizujte** schéma relační databáze pro evidenci knih a výpůjček v městské knihovně. Kromě vytvoření **ER diagramu** vytvoří také **fyzické tabulky a vztahy** mezi nimi (např. v SQL Server Management Studio pomocí *database diagrams*).



## **Business požadavky**

Databázový systém knihovny musí pokrývat následující oblasti:

### **1) Evidence knih**

- Každá kniha má **jedinečné ID**, název, rok vydání, ISBN a počet fyzických kusů dostupných v knihovně.
- Každá kniha patří do **jednoho nebo více oborů** (např. historie, informatika, beletrie).
- Každá kniha může mít **jednoho nebo více autorů**.
- Autor má evidované jméno, příjmení, případně rok narození.
- Knihy mohou být součástí **série**, která má svůj název a pořadí knih v sérii.
- Kniha má **vydavatele** (název, sídlo, rok založení).

### **2) Evidence uživatelů**

- Každý uživatel knihovny má **jedinečné ID**, jméno, příjmení, e-mail, datum narození a datum registrace.
- Uživatel může mít záznam o aktivních výpůjčkách a historii dřívějších výpůjček.

### **3) Výpůjčky a rezervace**

- Každou knihu si může půjčit pouze **registrovaný uživatel**.
- Každá výpůjčka obsahuje:
  - **Datum výpůjčky**
  - **Předpokládané datum vrácení** (max. 30 dnů od výpůjčky)
  - **Datum skutečného vrácení**
  - **Informaci o prodloužení výpůjčky** (max. jednou, pokud není na knihu rezervace).
- Pokud je kniha momentálně vypůjčená, uživatel si ji může **rezervovat**.
- Rezervace platí **3 dny** od okamžiku, kdy se kniha vrátí do knihovny.
- Pokud uživatel **nevrátí knihu včas**, může systém evidovat penalizaci (např. pokutu).

### **4) Strukturované vyhledávání v knihách**

- Knihy lze vyhledávat podle **autora, oboru, vydavatele** a dalších atributů.
- Musí být možné zjistit, kolik kusů dané knihy je **aktuálně dostupných** k půjčení.



## **Zadání**

1. **Vytvořte ER diagram** relační databáze v libovolném nástroji (např. dbdiagram.io, Lucidchart, Draw.io).

2. Na základě ER diagramu implementujte fyzické schéma databáze v libovolném SQL serveru (ideálně Microsoft SQL Server pomocí SQL Server Management Studio – SSMS - Database Diagrams - viz níže).

    - Vytvořte tabulky a nastavte mezi nimi relace (primární a cizí klíče).
- Použijte vhodné **datové typy** (např. INT, NVARCHAR, DATE, BIT).
   - Zajistěte **integritní omezení** (např. NOT NULL, UNIQUE, CHECK, ...).
   
   **V SSMS můžete použít nástroj Database Diagram** a splnit obě úlohy najednou. Existují samozřejmě i alternativní nástroje pro MySQL, PostgreSQL a další SQL DBMS, popř. nástroje třetích stran pro MSSQL.

4. **Ověřte správnost návrhu** – vytvořte ukázková data a ověřte, že lze evidovat výpůjčky i rezervace.
