using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	public class HerniStrategiePreferujVyhazovaniJinakPrvniMoznou : HerniStrategieTahniPrvniMoznouFigurkou
	{
		public HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(Hra hra) : base(hra)
		{
		}

		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if (!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku
						return figurka;
					}
				}
			}

			return base.DejFigurkuKterouHrat(hrac, hod);
		}
	}
}
