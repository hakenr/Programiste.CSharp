# k-NN:  k-nejbližších sousedů (k-NN)

## Cíl:
Seznámit se s algoritmem "k-NN" (k-nejbližších sousedů) a implementovat jej v jazyce C# pro klasifikaci bodů ve dvoudimenzionálním prostoru.

## Pokyny:
1. Vytvořte třídu `KNN`, která bude obsahovat metody pro trénování a predikci modelu k-NN.
2. Implementujte metodu `Train`, která přijímá trénovací data ve formě seznamu bodů a jejich příslušných tříd (např. jako `List<Tuple<double, double, string>>`, kde první dvě hodnoty představují souřadnice bodu a třetí hodnota je název třídy).
3. Implementujte metodu `Predict`, která přijímá testovací bod (např. `Tuple<double, double>`) a počet nejbližších sousedů k jako parametry. Metoda vrátí predikovanou třídu testovacího bodu.
4. V metodě `Predict` vypočítejte vzdálenosti mezi testovacím bodem a všemi trénovacími body (Euklidovská vzdálenost je nejčastější volbou).
5. Seřaďte trénovací body podle vzdálenosti od testovacího bodu a vyberte `k` nejbližších sousedů.
6. Určete nejčastější třídu mezi `k` nejbližšími sousedy a vrátte ji jako predikci pro testovací bod.
7. Otestujte svůj k-NN model na nějakém datasetu. Například můžete vytvořit umělý dataset s dvěma třídami bodů, které jsou rozmístěny ve dvou klastrech. Rozdělte dataset na trénovací a testovací část a vyhodnoťte úspěšnost vašeho modelu.

Příklad použití vaší implementace k-NN v kódu:
```csharp
// Příprava trénovacích dat
List<Tuple<double, double, string>> trainingData = new List<Tuple<double, double, string>>
{
    Tuple.Create(1.0, 2.0, "ClassA"),
    Tuple.Create(3.0, 4.0, "ClassB"),
    // ...další body...
};

// Příprava testovacího bodu
Tuple<double, double> testPoint = Tuple.Create(2.5, 3.5);

// Inicializace a trénování k-NN modelu
KNN knn = new KNN();
knn.Train(trainingData);

// Predikce třídy pro testovací bod
int k = 3;
string predictedClass = knn.Predict(testPoint, k);

// Výpis výsledku
Console.WriteLine($"Predikovaná třída pro bod ({testPoint.Item1}, {testPoint.Item2}) je: {predictedClass}");
```

## Inspirace
Vzorové řešení úlohy může vypadat takto:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class KNN
{
    private List<Tuple<double, double, string>> _trainingData;

    public void Train(List<Tuple<double, double, string>> trainingData)
    {
        _trainingData = trainingData;
    }

    public string Predict(Tuple<double, double> testPoint, int k)
    {
        var sortedNeighbors = _trainingData.Select(point => new
        {
            Distance = EuclideanDistance(testPoint, Tuple.Create(point.Item1, point.Item2)),
            Class = point.Item3
        })
        .OrderBy(point => point.Distance)
        .Take(k)
        .ToList();

        string predictedClass = sortedNeighbors.GroupBy(x => x.Class)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;

        return predictedClass;
    }

    private double EuclideanDistance(Tuple<double, double> point1, Tuple<double, double> point2)
    {
        double distance = Math.Sqrt(Math.Pow(point1.Item1 - point2.Item1, 2) + Math.Pow(point1.Item2 - point2.Item2, 2));
        return distance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Tuple<double, double, string>> trainingData = new List<Tuple<double, double, string>>
        {
            Tuple.Create(1.0, 2.0, "ClassA"),
            Tuple.Create(3.0, 4.0, "ClassB"),
            // ...další body...
        };

        Tuple<double, double> testPoint = Tuple.Create(2.5, 3.5);

        KNN knn = new KNN();
        knn.Train(trainingData);

        int k = 3;
        string predictedClass = knn.Predict(testPoint, k);

        Console.WriteLine($"Predikovaná třída pro bod ({testPoint.Item1}, {testPoint.Item2}) je: {predictedClass}");
    }
}
```

# Závěr
Algoritmus k-nejbližších sousedů (k-NN) je univerzální a jednoduchý algoritmus, který se dá použít v mnoha praktických příkladech. Zde jsou některé z nich:
1. Rozpoznávání ručně psaných číslic: Souřadnice mohou představovat pixely z obrázku ručně psané číslice (předzpracované do vektoru), zatímco třída klasifikace odpovídá číselné hodnotě číslice (0-9).
2. Klasifikace novinových článků: Souřadnice mohou být reprezentace článků, například vektor TF-IDF nebo jiné metody výpočtu slovních vah. Třída klasifikace pak odpovídá kategoriím článků, jako např. politika, sport, kultura atd.
3. Diagnóza nemocí: Souřadnice mohou být hodnoty různých biomedicínských měření, jako jsou krevní testy, krevní tlak, tělesná hmotnost atd. Třída klasifikace by pak odpovídala diagnóze nemoci nebo stavu pacienta.
4. Doporučování filmů: Souřadnice mohou být vlastnosti filmů, jako je žánr, délka, hodnocení, obsazení atd. Třída klasifikace by pak mohla odpovídat preferencím uživatele (např. "líbí se" nebo "nelíbí se").
5. Detekce spamu: Souřadnice mohou být slovní vektor zprávy, např. frekvence klíčových slov, délka zprávy nebo další textové rysy. Třída klasifikace by pak odpovídala označení zprávy jako "spam" nebo "ne-spam".

V praxi je důležité správně předzpracovat data a zvolit vhodnou metriku vzdálenosti, která bude fungovat dobře pro konkrétní případ použití. K-NN algoritmus může být citlivý na škálování dat a výběr správného parametru k, což může ovlivnit jeho výkon a přesnost.

# Další příklady

## Iris
Jednoduchý příklad použití k-NN algoritmu, který vyžaduje minimální předzpracování dat, je klasifikace květů irisů na základě jejich délky a šířky okvětních lístků. Tento dataset je známý jako Iris dataset a je populární v oblasti strojového učení pro testování algoritmů. Dataset obsahuje 150 vzorků, každý s čtyřmi atributy (délka a šířka okvětních lístků a délka a šířka kališních lístků) a třemi třídami (setosa, versicolor, virginica).

V tomto případě by souřadnice byly hodnoty délky a šířky okvětních lístků, které jsou běžně dostupné v reálném světě a nevyžadují značné předzpracování. Třída klasifikace by pak byl druh irisu (setosa, versicolor nebo virginica).

Příklad použití k-NN algoritmu pro klasifikaci květů irisů v C#:

```csharp
List<Tuple<double, double, string>> trainingData = new List<Tuple<double, double, string>>
{
    Tuple.Create(5.1, 3.5, "setosa"),
    Tuple.Create(4.9, 3.0, "setosa"),
    Tuple.Create(7.0, 3.2, "versicolor"),
    Tuple.Create(6.4, 3.2, "versicolor"),
    Tuple.Create(6.3, 3.3, "virginica"),
    Tuple.Create(5.8, 2.7, "virginica"),
    // ...další vzorky...
};

Tuple<double, double> testPoint = Tuple.Create(5.9, 3.0);

KNN knn = new KNN();
knn.Train(trainingData);

int k = 3;
string predictedClass = knn.Predict(testPoint, k);

Console.WriteLine($"Predikovaná třída pro bod ({testPoint.Item1}, {testPoint.Item2}) je: {predictedClass}");
```

V tomto příkladě je použití k-NN algoritmu jednoduché a přímočaré, protože vstupní data jsou již v jednoduchém formátu, který je snadno srozumitelný a nevyžaduje složité předzpracování.

## Víno
Další jednoduchý příklad použití k-NN algoritmu je klasifikace vína na základě jejich chemických vlastností. Představme si, že máme dataset obsahující informace o různých typech vín, jako je obsah alkoholu, kyselost, hustota, cukr atd. Naším cílem je klasifikovat vína do tříd jako červené, bílé nebo růžové.

V tomto případě by souřadnice byly chemické vlastnosti vína, které jsou běžně dostupné v reálném světě a nevyžadují značné předzpracování. Třída klasifikace by pak byl druh vína (červené, bílé nebo růžové).

Příklad použití k-NN algoritmu pro klasifikaci vín v C#:
```csharp
List<Tuple<double, double, string>> trainingData = new List<Tuple<double, double, string>>
{
    Tuple.Create(13.2, 3.4, "red"),
    Tuple.Create(12.8, 3.1, "red"),
    Tuple.Create(9.8, 3.3, "white"),
    Tuple.Create(10.2, 3.2, "white"),
    Tuple.Create(11.6, 3.2, "rose"),
    Tuple.Create(11.0, 3.0, "rose"),
    // ...další vzorky...
};

// Testovací bod: obsah alkoholu a kyselost
Tuple<double, double> testPoint = Tuple.Create(10.5, 3.1);

KNN knn = new KNN();
knn.Train(trainingData);

int k = 3;
string predictedClass = knn.Predict(testPoint, k);

Console.WriteLine($"Predikovaná třída pro bod ({testPoint.Item1}, {testPoint.Item2}) je: {predictedClass}");
```

## Studijní výsledky
Jednoduchý příklad použití k-NN algoritmu, který by byl tematicky ze studentského prostředí, je klasifikace studentů do skupin podle jejich studijních návyků a dosažených výsledků. Představme si, že máme dataset obsahující informace o studentech, jako je počet hodin studia týdně, účast na přednáškách a průměrné známky.

V tomto případě by souřadnice byly studijní návyky a dosažené výsledky studentů, které jsou běžně dostupné v reálném světě a nevyžadují značné předzpracování. Třída klasifikace by pak byla skupina studentů (například "úspěšní", "průměrní" nebo "potřebují zlepšit").

Příklad použití k-NN algoritmu pro klasifikaci studentů v C#:

```csharp
List<Tuple<double, double, string>> trainingData = new List<Tuple<double, double, string>>
{
    // Formát: (počet hodin studia týdně, účast na přednáškách, skupina)
    Tuple.Create(25.0, 90.0, "successful"),
    Tuple.Create(20.0, 85.0, "successful"),
    Tuple.Create(15.0, 75.0, "average"),
    Tuple.Create(10.0, 70.0, "average"),
    Tuple.Create(5.0, 50.0, "needs_improvement"),
    Tuple.Create(3.0, 40.0, "needs_improvement"),
    // ...další vzorky...
};

// Testovací bod: počet hodin studia týdně a účast na přednáškách
Tuple<double, double> testPoint = Tuple.Create(12.0, 60.0);

KNN knn = new KNN();
knn.Train(trainingData);

int k = 3;
string predictedClass = knn.Predict(testPoint, k);

Console.WriteLine($"Predikovaná třída pro bod ({testPoint.Item1}, {testPoint.Item2}) je: {predictedClass}");
```
V tomto případě je použití k-NN algoritmu jednoduché a přímočaré, protože vstupní data jsou v jednoduchém formátu a nevyžadují složité předzpracování.