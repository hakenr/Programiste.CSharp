namespace CloveceNezlobSe
{
	public abstract class HerniPlan
	{
		public abstract int MaximalniPocetHracu { get; }

		public abstract void DejFigurkuNaStartovniPolicko(Figurka figurka);
		
		public abstract Policko? ZjistiCilovePolicko(Figurka figurka, int hod);

		public abstract void PosunFigurku(Figurka figurka, int pocetPolicek);

		public abstract bool MuzuTahnout(Figurka figurka, int hod);

		public abstract void Vykresli();
	}
}