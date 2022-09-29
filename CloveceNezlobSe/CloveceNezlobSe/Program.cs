using CloveceNezlobSe;

HerniPlan herniPlan = new LinearniHerniPlan(15);

Hra hra = new Hra(herniPlan);

HerniStrategie herniStrategieTahniPrvniFigurkou = new HerniStrategieTahniPrvniMoznouFigurkou(hra);
HerniStrategie herniStrategiePreferujVyhazovaniJinakPrvniMoznou = new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra);

Hrac hracRobert = new Hrac("Robert", herniStrategieTahniPrvniFigurkou);
Hrac hracKarel = new Hrac("Karel", herniStrategieTahniPrvniFigurkou);
Hrac hracMartin = new Hrac("Martin", herniStrategiePreferujVyhazovaniJinakPrvniMoznou);

hra.PridejHrace(hracRobert);
hra.PridejHrace(hracKarel);
hra.PridejHrace(hracMartin);

hra.Start();