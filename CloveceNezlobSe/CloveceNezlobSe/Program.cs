using CloveceNezlobSe;

HerniPlan herniPlan = new LinearniHerniPlan(15);
Hra hra = new Hra(herniPlan);
HerniStrategieTahniPrvniMoznouFigurkou herniStrategieTahniPrvniFigurkou = new HerniStrategieTahniPrvniMoznouFigurkou(hra);
Hrac hracRobert = new Hrac("Robert", herniStrategieTahniPrvniFigurkou);
Hrac hracKarel = new Hrac("Karel", herniStrategieTahniPrvniFigurkou);
Hrac hracMartin = new Hrac("Martin", herniStrategieTahniPrvniFigurkou);

hra.PridejHrace(hracRobert);
hra.PridejHrace(hracKarel);
hra.PridejHrace(hracMartin);

hra.Start();