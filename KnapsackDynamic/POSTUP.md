Pojďme si podrobně projít navržené řešení pomocí dynamického programování (DP) krok za krokem, a také jak se mění obsah tabulky DP.

### 1. **Příprava tabulky DP**

Tabulka DP má rozměry `(n + 1) x (W + 1)`:
- `n` je počet šperků.
- `W` je maximální objem tašky.
- Každá buňka `dp[i][w]` obsahuje maximální cenu, které lze dosáhnout při použití prvních `i` šperků a tašky s objemem `w`.

#### Příklad:

Máme 3 šperky:
- Šperk 1: Objem = 3 litry, Cena = 60
- Šperk 2: Objem = 4 litry, Cena = 100
- Šperk 3: Objem = 2 litry, Cena = 120

Kapacita tašky = 5 litrů.

### 2. **Inicializace tabulky**

Začneme s prázdnou tabulkou, kde řádek `i=0` a sloupec `w=0` jsou inicializovány na 0. To znamená, že pokud nemáme žádné šperky nebo je kapacita tašky 0, zloděj nemůže nic ukrást, takže maximální cena je 0.

| i/w | 0 | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|---|
| 0   | 0 | 0 | 0 | 0 | 0 | 0 |
| 1   | 0 | 0 | 0 | 0 | 0 | 0 |
| 2   | 0 | 0 | 0 | 0 | 0 | 0 |
| 3   | 0 | 0 | 0 | 0 | 0 | 0 |

### 3. **Vyplňování tabulky DP**

Tabulku budeme vyplňovat iterativně. Pro každý šperk se rozhodneme, zda jej zahrneme do tašky, nebo ne, a zohledníme maximální možnou cenu pro každý objem tašky.

#### **Pro šperk 1 (Objem = 3, Cena = 60):**

- Pro kapacitu 0 až 2 litry nemůže být šperk 1 zahrnut, protože jeho objem je větší než kapacita:
  - `dp[1][0] = dp[0][0] = 0`
  - `dp[1][1] = dp[0][1] = 0`
  - `dp[1][2] = dp[0][2] = 0`

- Pro kapacitu 3 až 5 litrů lze šperk 1 přidat:
  - `dp[1][3] = max(dp[0][3], dp[0][3-3] + 60) = max(0, 0 + 60) = 60`
  - `dp[1][4] = max(dp[0][4], dp[0][4-3] + 60) = max(0, 0 + 60) = 60`
  - `dp[1][5] = max(dp[0][5], dp[0][5-3] + 60) = max(0, 0 + 60) = 60`

Tabulka po zpracování šperku 1:

| i/w | 0 | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|---|
| 0   | 0 | 0 | 0 | 0 | 0 | 0 |
| 1   | 0 | 0 | 0 | 60 | 60 | 60 |
| 2   | 0 | 0 | 0 | 0  | 0  | 0  |
| 3   | 0 | 0 | 0 | 0  | 0  | 0  |

#### **Pro šperk 2 (Objem = 4, Cena = 100):**

- Pro kapacitu 0 až 3 litry nelze šperk 2 přidat, takže hodnoty zůstávají stejné:
  - `dp[2][0] = dp[1][0] = 0`
  - `dp[2][1] = dp[1][1] = 0`
  - `dp[2][2] = dp[1][2] = 0`
  - `dp[2][3] = dp[1][3] = 60`

- Pro kapacitu 4 a 5 litrů:
  - `dp[2][4] = max(dp[1][4], dp[1][4-4] + 100) = max(60, 0 + 100) = 100`
  - `dp[2][5] = max(dp[1][5], dp[1][5-4] + 100) = max(60, 0 + 100) = 100`

Tabulka po zpracování šperku 2:

| i/w | 0 | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|---|
| 0   | 0 | 0 | 0 | 0  | 0  | 0  |
| 1   | 0 | 0 | 0 | 60 | 60 | 60 |
| 2   | 0 | 0 | 0 | 60 | 100 | 100 |
| 3   | 0 | 0 | 0 | 0  | 0  | 0  |

#### **Pro šperk 3 (Objem = 2, Cena = 120):**

- Pro kapacitu 0 až 1 litru nelze šperk 3 přidat:
  - `dp[3][0] = dp[2][0] = 0`
  - `dp[3][1] = dp[2][1] = 0`

- Pro kapacitu 2 až 5 litrů lze šperk 3 přidat a rozhodneme se podle toho, zda ho vzít:
  - `dp[3][2] = max(dp[2][2], dp[2][2-2] + 120) = max(0, 0 + 120) = 120`
  - `dp[3][3] = max(dp[2][3], dp[2][3-2] + 120) = max(60, 0 + 120) = 120`
  - `dp[3][4] = max(dp[2][4], dp[2][4-2] + 120) = max(100, 0 + 120) = 120`
  - `dp[3][5] = max(dp[2][5], dp[2][5-2] + 120) = max(100, 60 + 120) = 180`

Tabulka po zpracování šperku 3:

| i/w | 0 | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|---|
| 0   | 0 | 0 | 0 | 0  | 0  | 0  |
| 1   | 0 | 0 | 0 | 60 | 60 | 60 |
| 2   | 0 | 0 | 0 | 60 | 100 | 100 |
| 3   | 0 | 0 | 120 | 120 | 120 | 180 |

### 4. **Výsledek a rekonstrukce řešení**

- Maximální hodnota se nachází v buňce `dp[3][5]`, což je 180. To znamená, že nejvyšší možná cena, kterou lze získat při kapacitě 5 litrů, je 180.

- Nyní můžeme zpětně rekonstruovat, které šperky byly vybrány:
  - `dp[3][5] != dp[2][5]`, což znamená, že šperk 3 byl vybrán.
  - Po odečtení objemu šperku 3 (2 litry) pokračujeme v kapacitě 3: `dp[2][3] = dp[1][3]`, což znamená, že šperk 2 nebyl vybrán.
  - Pokračujeme na předchozí šperk `dp[1][3] != dp[2][3]`, což znamená, že šperk 1 byl vybrán.
  - Po odečtení objemu šperku 1 (3 litry) nezbývá žádná kapacita, takže jsme na konci.
  
  Výsledek tedy je:
  - Vybrané šperky: šperk 3 (120) a šperk 1 (60)
  - Celková cena: 180

### Shrnutí:

Dynamické programování efektivně nalezne optimální řešení tím, že pro každý šperk zohlední všechny možné objemy tašky a rozhodne se, zda šperk zahrnout. Tabulka DP se plní systematicky, a výsledek je vždy optimální.