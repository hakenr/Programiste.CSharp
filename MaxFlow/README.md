### **Maximální průtok v síti (Max-Flow) – Ford-Fulkerson (Edmonds-Karp varianta)**

#### **Motivace**
Představte si, že jste inženýři vodohospodářské společnosti a máte naplánovat maximální možný průtok vody mezi **hlavním vodním zdrojem** (uzel `S`) a **hlavním odběrným místem** (uzel `T`). Vodovodní systém lze modelovat jako **směrovaný graf**, kde:
- **Vrcholy** představují vodárny nebo přivaděče.
- **Hrany** představují trubky mezi nimi s určitou **kapacitou** (kolik vody může protéct najednou).



## **Co je to maximální tok?**
- Maximální množství "hmoty" (např. vody, dat, elektrické energie), které můžeme **poslat z uzlu `S` do uzlu `T`**, aniž bychom překročili kapacity hran.
- Každá hrana má **kapacitu** – maximální množství průtoku, které může nést.
- Naším cílem je **najít největší možný tok** ze zdroje do cíle.



## **Ford-Fulkersonův algoritmus**
Algoritmus Ford-Fulkerson řeší problém maximálního toku iterativním **hledáním augmentačních cest** a **upravováním reziduálního grafu**.

### **Hlavní myšlenka:**
1. **Vytvoříme kopii grafu (reziduální graf)** – ten uchovává nejen původní kapacity, ale i aktuální zbytkové kapacity.
2. **Opakovaně hledáme augmentační cestu** (cestu, kterou lze ještě poslat další tok).
3. **Najdeme minimální kapacitu na této cestě** (to je maximální množství toku, které lze přidat).
4. **Upravíme reziduální graf**:
   - Snížíme kapacitu hran po směru toku.
   - Přidáme zpětné hrany s kapacitou odpovídající již poslanému toku.
5. **Krok 2-4 opakujeme, dokud neexistuje žádná další augmentační cesta**.

**Poznámka:**  
Původní Ford-Fulkerson používá **DFS pro hledání augmentační cesty**, což může vést k horší složitosti `O(E * max_flow)`.  
**Edmonds-Karp varianta** využívá **BFS (šířkové prohledávání)** a běží v `O(V * E²)`.



## **Složky algoritmu**
### **Reziduální graf**
- Místo toho, abychom **mazali hrany**, vytvoříme si nový **reziduální graf**, kde:
  - **Původní hrana se sníženou kapacitou** (tok nám snižuje volné místo).
  - **Zpětná hrana s novou kapacitou** (tok se může vrátit, pokud potřebujeme přerozdělit průtok).
  
Příklad:
- Původní hrana **A → B, kapacita 10**.
- Pokud posíláme **tok 4**, změníme graf na:
  - **A → B, kapacita 6**.
  - **B → A, kapacita 4** (zpětná hrana pro vracení toku).

### **Augmentační cesta**
- Cesta od `S` do `T`, kde každá hrana má **nenulovou zbývající kapacitu**.
- Najdeme ji pomocí **BFS**, abychom vždy našli nejkratší cestu (v počtu hran).

### **Pravidla aktualizace toku**
1. Najdeme **nejmenší kapacitu** na augmentační cestě – to je **průtok, který můžeme přidat**.
2. **Snížíme kapacitu přímých hran** na této cestě.
3. **Zvýšíme kapacitu zpětných hran**.


1. 
## **Příklad**
### **Vstupní graf:**
```
        10
   (S) -----> (A)
    |         |
    |5        | 15
    v         v
   (B) -----> (T)
        10
```
Hrany jsou ve formátu `(zdroj -> cíl, kapacita)`:
```
S → A (10)
S → B (5)
A → T (15)
B → T (10)
```

### **Průběh algoritmu:**
1. **Najdeme augmentační cestu** `S → A → T` s minimální kapacitou `10` → pošleme `10` jednotek toku.
2. **Aktualizujeme graf**:
   - `S → A` kapacita se sníží na **0**.
   - `A → T` kapacita se sníží na **5**.
   - Přidáme zpětné hrany `A → S` s kapacitou `10` a `T → A` s kapacitou `10`.

Nový reziduální graf:
```
        0
   (S) -----> (A)
    |         |
    |5        | 5
    v         v
   (B) -----> (T)
        10
```

3. **Další augmentační cesta** `S → B → T` s minimální kapacitou `5`.
4. **Pošleme tok 5** a **reziduální graf se upraví**.

Po dokončení:
- **Celkový tok = 10 + 5 = 15**.
- **Maximální tok z `S` do `T` je 15**.

## Testovací data

### Vstupní formát
```
N M
from1 to1 capacity1
from2 to2 capacity2
...
fromM toM capacityM
S T
```
Kde:
- `N` = počet vrcholů
- `M` = počet hran
- `from to capacity` = orientované hrany s kapacitami
- `S` = zdrojový uzel
- `T` = cílový uzel


### Vstup 1
```
4 10
0 3
0 1 20
1 0 20
0 2 10
2 0 10
1 2 5
2 1 5
1 3 10
3 1 10
2 3 20
3 2 20
```
Očekávaný výstup: 25

### Vstup 2
```
4 12
0 3
0 1 20
1 0 20
0 2 10
2 0 10
1 2 5
2 1 5
2 1 3
1 2 3
1 3 10
3 1 10
2 3 20
3 2 20
```
Očekávaný výstup: 28

### Vstup 3
```
6 8
0 5
0 1 11
0 2 12
2 1 1
1 3 12
2 4 11
3 5 19
4 3 7
4 5 4
```
Očekávaný výstup: 23


## Další ukázky a vysvětlení
* https://inginious.org/course/competitive-programming/graphs-maxflow
* https://www.w3schools.com/dsa/dsa_theory_graphs_maxflow.php
* https://www.w3schools.com/dsa/dsa_algo_graphs_edmondskarp.php
* https://visualgo.net/en/maxflow?slide=1
* https://isabek.github.io/
* https://algorithms.discrete.ma.tum.de/graph-algorithms/flow-ford-fulkerson/index_en.html