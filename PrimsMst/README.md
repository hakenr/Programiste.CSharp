### **Návrh minimální silniční sítě mezi městy světa**


#### Scénář:
Představte si, že jste pověřeni návrhem minimální silniční sítě, která propojí zadaná města na světě. Každé město je reprezentováno svou zeměpisnou šířkou (LAT) a délkou (LON). Náklady na vybudování silnice mezi dvěma městy odpovídají jejich vzdálenosti vzdušnou čarou (geografická vzdálenost). Úkolem programu je navrhnout nejlevnější síť propojení, která zajistí, že všechna města budou propojena.

#### Popis Primova algoritmu:
![Prim](https://upload.wikimedia.org/wikipedia/commons/9/9b/PrimAlgDemo.gif)

[Primův algoritmus](https://en.wikipedia.org/wiki/Prim%27s_algorithm) se používá k nalezení minimální kostry (Minimum Spanning Tree, MST) v grafu (obrázek linkován z Wikipedie). Výsledná kostra:
1. Propojuje všechny vrcholy (města) grafu.
2. Minimalizuje celkové náklady (součet vzdáleností mezi propojenými městy).

**Postup Primova algoritmu:**
1. Začněte s libovolným vrcholem (městem).
2. Postupně přidávejte nejlevnější hranu (s nejmenší vzdáleností), která spojuje již navštívené město s nenavštíveným.
3. Opakujte, dokud nejsou propojena všechna města.

#### Specifikace úkolu:

1. **Vstupní data**:  
   [CSV soubor](worldcities.csv) ([zdroj](https://simplemaps.com/data/world-cities)) obsahuje seznam měst a jejich souřadnice (LAT a LON). Například:
   ```
   Name,Lat,Lon
   New York,40.7128,-74.0060
   Los Angeles,34.0522,-118.2437
   Chicago,41.8781,-87.6298
   ...
   ```

2. **Požadavek na výstup**:
   - Seznam silnic (hran), které tvoří minimální silniční síť.
   - Celková délka (váha) této sítě.

3. **Zjednodušený předpoklad**:
   - Všechna města lze spojit přímou vzdušnou čarou.

4. **Výpočet vzdálenosti**:
   Pro výpočet vzdálenosti mezi dvěma městy použijte [Haversinovu formuli](https://en.wikipedia.org/wiki/Haversine_formula):
   \[
   d = 2r \cdot \arcsin\left(\sqrt{\sin^2\left(\frac{\phi_2 - \phi_1}{2}\right) + \cos(\phi_1) \cdot \cos(\phi_2) \cdot \sin^2\left(\frac{\lambda_2 - \lambda_1}{2}\right)}\right)
   \]
   Kde:
   - \( \phi_1, \phi_2 \): zeměpisná šířka obou bodů v radiánech,
   - \( \lambda_1, \lambda_2 \): zeměpisná délka obou bodů v radiánech,
   - \( r \): poloměr Země (6371 km).


### Challenge
Ze vstupního souboru použijte jen tolik záznamů, kolik váš program zvládne zpracovat do 1 minuty. Kolik to bude? Jakou asymtotickou časovou složitost má váš algoritmus?