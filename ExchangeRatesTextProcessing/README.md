# Kurzovní lístek - zpracování textových dat

Česká národní banka publikuje oficiální kurzy měn pro každý den na svých webových stránkách: https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/

Pro snažší strojové zpracování publikuje vždy i strukturovanou textovou podobu: https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt

Data vypadají zhruba takto (počet řádků zkrácen):
```
08.12.2021 #236
země|měna|množství|kód|kurz
Austrálie|dolar|1|AUD|16,077
Brazílie|real|1|BRL|4,021
Bulharsko|lev|1|BGN|13,025
Čína|žen-min-pi|1|CNY|3,552
Dánsko|koruna|1|DKK|3,426
EMU|euro|1|EUR|25,475
```

Napište miniaplikaci, která stáhne aktuální kurzovní lístek z webu ČNB a vypíše jej uživateli v této podobě:

TODO

## Challenge
Webu ČNB lze předat požadované datum, z kterého kurzovní lístek chceme: https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt?date=07.12.2021
Vylepšete aplikaci tak, že do výstupu přidá informaci, jak se kurz změnil oproti hodnotě z předchozího dne (třeba v procentech).

## Bude se vám hodit
TODO
* získání obsahu webové stránky do string
