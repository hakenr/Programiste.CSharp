using System.Diagnostics;
using CloveceNezlobSe;

static Hrac HrajPartii()
{
	HerniPlan herniPlan = new LinearniHerniPlan(15);

	Hra hra = new Hra(herniPlan);

	HerniStrategie herniStrategieTahniPrvniFigurkou = new HerniStrategieTahniPrvniMoznouFigurkou(hra);
	HerniStrategie herniStrategiePreferujVyhazovaniJinakPrvniMoznou = new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra);

	Hrac hrac1 = new Hrac("Karel:PrvniFigurkou", herniStrategieTahniPrvniFigurkou);
	Hrac hrac2 = new Hrac("Robert:PreferujVyhazovaniJinakPrvniFigurkou", herniStrategiePreferujVyhazovaniJinakPrvniMoznou);
	//Hrac hracN = new Hrac("Martin", herniStrategiePreferujVyhazovaniJinakPrvniMoznou);

	hra.PridejHrace(hrac1);
	hra.PridejHrace(hrac2);
	//hra.PridejHrace(hracN);
	hra.NastavNahodnePoradiHracu();

	hra.Start();

	return hra.Vitezove[0]; // vrátíme vítěze
}


// MĚŘENÍ
var vysledkyHer = new Dictionary<string, int>(); // Key: hráč, Value: počet vítěztví

// vypneme výstup do konzole, abychom urychlili průběh
var originalConsoleOut = Console.Out;
Console.SetOut(TextWriter.Null);
var sw = Stopwatch.StartNew();

for (int i = 0; i < 50000; i++)
{
	Hrac vitez = HrajPartii();
	vysledkyHer[vitez.Jmeno] = vysledkyHer.GetValueOrDefault(vitez.Jmeno, 0) + 1;
}

// obnovení výstupu do konzole
Console.SetOut(originalConsoleOut);
Console.WriteLine($"{sw.ElapsedMilliseconds:n2} ms");

foreach (var vysledek in vysledkyHer.OrderByDescending(v => v.Value))
{
	Console.WriteLine($"{vysledek.Key}: {vysledek.Value}");
}
