# Nejkratší cesta mezi městy

**Téma:** Dijkstra algoritmus pro nalezení nejkratší cesty mezi dvěma městy.

Dijkstrův algoritmus je grafový algoritmus, který slouží k nalezení nejkratší cesty v grafu. Jeho základní myšlenka spočívá v tom, že iterativně rozšiřuje seznam dosažitelných vrcholů grafu, přičemž si udržuje nejkratší nalezenou vzdálenost do každého vrcholu.

Algoritmus pracuje takto:

1. **Inicializace**:
   - Pro každý vrchol grafu se nastaví vzdálenost na nekonečno, kromě počátečního vrcholu, kde je vzdálenost nastavena na 0.
   - Počáteční vrchol je označen jako navštívený, ostatní vrcholy jako nenavštívené.

2. **Prohledávání**:
   - Vybere se vrchol s nejnižší vzdáleností mezi nenavštívenými vrcholy (zpočátku je to počáteční vrchol).
   - Pro každý sousední vrchol se vypočítá vzdálenost přes právě vybraný vrchol. Pokud je tato vzdálenost kratší než aktuálně uložená vzdálenost k sousedu, aktualizuje se.
   - Po prozkoumání všech sousedů se vrchol označí jako "navštívený" a nepoužívá se znovu.

3. **Konec**:
   - Proces se opakuje, dokud nejsou všechny vrcholy navštíveny, nebo dokud není nalezena nejkratší cesta do cílového vrcholu.

Dijkstrův algoritmus funguje dobře u grafů, kde jsou všechny hrany kladné (tj. vzdálenosti jsou kladné). Pro grafy s negativními hranami není tento algoritmus použitelný, protože by mohl vynechat lepší cestu nalezenou později.

Využívá se například při plánování tras v mapových aplikacích nebo při optimalizaci síťových cest.

**Zadání:**
Máte k dispozici tabulku silničních vzdáleností mezi několika českými městy (viz přiložený CSV soubor). Vaším úkolem je implementovat program, který dokáže najít nejkratší cestu mezi dvěma libovolnými městy pomocí **Dijkstra algoritmu**.

**Kroky:**

1. **Nastudování Dijkstra algoritmu:** Nejprve si prostudujte, jak funguje Dijkstra algoritmus. Ten je určen k nalezení nejkratší cesty v grafu, kde jsou hrany ohodnoceny kladnými váhami (v tomto případě vzdálenosti mezi městy).
	* [Dijkstrův algoritmus – Wikipedie](https://cs.wikipedia.org/wiki/Dijkstr%C5%AFv_algoritmus)
	* [Dijkstra Visualzation](https://www.cs.usfca.edu/~galles/visualization/Dijkstra.html)

2. **Načtení dat:** 
   - Načtěte data z poskytnutého CSV souboru, který obsahuje města a vzdálenosti mezi nimi.
   - Vytvořte z dat graf, kde města budou vrcholy a silniční vzdálenosti mezi nimi budou hranami.

3. **Implementace algoritmu:** 
   - Vytvořte program, který bude hledat nejkratší cestu mezi dvěma zadanými městy pomocí Dijkstra algoritmu.
   - Algoritmus by měl zobrazit celkovou délku nejkratší cesty a seznam měst, kterými cesta vede.

4. **Uživatelské rozhraní:**
   - Program by měl přijmout jako vstup výchozí město a cílové město.
   - Výstupem by měla být nejkratší cesta mezi těmito městy a celková vzdálenost.

**Tipy pro implementaci:**
- Můžete použít datové struktury jako slovníky (dictionary) nebo seznamy (list) pro uložení grafu.
- Zvažte využití prioritní fronty `PriorityQueue`.
- Algoritmus Dijkstra funguje na principu postupného prohledávání nejbližších sousedů, proto je důležité efektivně spravovat seznam neprozkoumaných uzlů a jejich vzdáleností.

**Rozšíření (volitelné):**
- Přiložený soubor `silnicni_vzdalenosti_mesta_full.csv` obsahuje kombinace měst, které nejsou vždy spojeny přímou cestou, oba směry. Najděte tyto záznamy a vyřaďte je (ponechte jen kratší spojnici sousedních měst).
- Implementujte možnost nalezení nejkratší cesty pro více měst (např. optimalizovaná trasa pro doručení).

**Data:**
V CSV souboru najdete následující strukturu:
- **Sloupce:** Město 1, Město 2, Vzdálenost (v km).

Příklad úseku dat:
```
Město1,Město2,Vzdálenost
Plzeň,Brno,296
Olomouc,Brno,77
Liberec,Brno,309
České Budějovice,Brno,217
Hradec Králové,Brno,167
Ústí nad Labem,Brno,298
Pardubice,Brno,146
```