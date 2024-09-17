# Zloděj v klenotnictví

## Zadání
Zloděj se vloupal do klenotnictví a má omezený objem své tašky a velmi omezený čas. V klenotnictví jsou různé šperky, z nichž každý má svůj objem a cenu. Zlodějovým cílem je ukrást co nejcennější kombinaci šperků, aniž by překročil maximální objem své tašky.

Vaším úkolem je pomoci zloději vybrat přiměřeně optimální kombinaci šperků pomocí **greedy algoritmu**, aby získal maximální hodnotu lupu s omezeným objemem tašky.

## Požadavky
1. **Vstup od uživatele**:
   - Uživatel zadá maximální objem tašky (například 100 litrů).
   - Uživatel zadá seznam šperků, kde každý šperk má svůj objem a cenu. Alternativně můžete použít generátor náhodných dat a zeptat se uživatele jen na počet šperků.

2. **Výpočet**:
   - Program se pokusí vybrat takovou kombinaci šperků, která maximalizuje celkovou cenu, aniž by překročila maximální objem tašky.
   - Implementujte základní řešení pomocí **greedy algoritmu**, tj. v každém kroku vyberte lokálně optimální šperk.

3. **Výstup**:
   - Program zobrazí seznam šperků, které zloděj vzal, jejich celkový objem a celkovou cenu.
   - Pokud není možné do tašky vložit žádný šperk, zobrazte zprávu, že taška je malá.

4. **Bonusový úkol**:
   a. Pro srovnání si zkuste implementovat brute-force řešení, které vyzkouší všechny možné kombinace šperků a vybere tu nejlepší. Porovnejte výsledky s greedy algoritmem.
   b. Umožněte uživateli zadat více různých tašek a zkuste ukrást co nejvíce.

## Zamyšlení
- Jaké typy úloh lze řešit pomocí greedy algoritmu? Zkuste si najít další příklady.
- Jaké jsou výhody a nevýhody greedy algoritmů oproti jiným metodám, jako je dynamické programování?
- Jak byste řešili problém, kdy by zloděj měl k dispozici neomezený počet šperků každého druhu?
- Jak byste přizpůsobili řešení, pokud by každý šperk měl i váhu a zloděj měl omezenou nosnost?