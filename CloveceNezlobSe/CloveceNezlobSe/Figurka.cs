using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	public class Figurka
	{
		public string OznaceniFigurky { get; private set; }
		public Hrac Hrac { get; private set; }
		public Policko? Policko { get; private set; }

		public Figurka(Hrac hrac, string oznaceniFigurky)
		{
			this.Hrac = hrac;
			this.OznaceniFigurky = oznaceniFigurky;
		}

		public void NastavPolicko(Policko? policko)
		{
			this.Policko = policko;
		}

		public bool JeVDomecku()
		{
			return (this.Policko != null) && (this.Policko.JeDomecek);
		}
	}
}
