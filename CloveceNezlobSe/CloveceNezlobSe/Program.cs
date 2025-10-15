using System.Diagnostics;
using CloveceNezlobSe;

static Hrac HrajPartii()
{
	HerniPlan herniPlan = new LinearniHerniPlan(30);

	Hra hra = new Hra(herniPlan);

	HerniStrategie herniStrategieTahniPrvniFigurkou = new HerniStrategieTahniPrvniMoznouFigurkou(hra);
	HerniStrategie herniStrategiePreferujVyhazovaniJinakPrvniMoznou = new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra);
	HerniStrategie interaktivnivniHerniStrategie = new HerniStrategieInteraktivni(hra);
	HerniStrategie aiHerniStrategie = new HerniStrategieAi(hra);

	Hrac hrac1 = new Hrac("A:PrvniFigurkou", herniStrategieTahniPrvniFigurkou);
	Hrac hrac2 = new Hrac("B:PreferujVyhazovaniJinakPrvniFigurkou", herniStrategiePreferujVyhazovaniJinakPrvniMoznou);
	Hrac hrac3 = new Hrac("C:AI", aiHerniStrategie);
	//Hrac hracN = new Hrac("Martin", herniStrategiePreferujVyhazovaniJinakPrvniMoznou);


	hra.PridejHrace(hrac1);
	hra.PridejHrace(hrac2);
	hra.PridejHrace(hrac3);
	//hra.PridejHrace(hracN);
	hra.NastavNahodnePoradiHracu();

	hra.Start();

	return hra.Vitezove[0]; // vrátíme vítěze
}


// MĚŘENÍ
var vysledkyHer = new Dictionary<string, int>(); // Key: hráč, Value: počet vítěztví

Console.OutputEncoding = System.Text.Encoding.UTF8;
// vypneme výstup do konzole, abychom urychlili průběh
//var originalConsoleOut = Console.Out;
//Console.SetOut(TextWriter.Null);

const int pocetPartii = 1;
var sw = Stopwatch.StartNew();

for (int i = 0; i < pocetPartii; i++)
{
	Hrac vitez = HrajPartii();
	vysledkyHer[vitez.Jmeno] = vysledkyHer.GetValueOrDefault(vitez.Jmeno, 0) + 1;
}

// obnovení výstupu do konzole
//Console.SetOut(originalConsoleOut);
Console.WriteLine($"{sw.ElapsedMilliseconds:n2} ms");

foreach (var vysledek in vysledkyHer.OrderByDescending(v => v.Value))
{
	Console.WriteLine($"{vysledek.Key}: {vysledek.Value}");
}
