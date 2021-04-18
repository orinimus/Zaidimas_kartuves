using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zaidimas_kartuves.Duomenubaze;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Servisas
{
    public class DuomenuApdorojimas : IDuomenuApdorojimas
    {
        private readonly IVadybintiKartuviuDb _vadybintiKartuviuDb;

        public DuomenuApdorojimas(IVadybintiKartuviuDb vadybintiKartuviuDb)
        {
            _vadybintiKartuviuDb = vadybintiKartuviuDb;
        }

        public List<string> TemuIsvedimas()
        {
            var visiZodziai = _vadybintiKartuviuDb.GautiVisusZodzius();
            var temuListas = new List<string>();
            foreach (var zodis in visiZodziai)
            {
                if (!temuListas.Contains(zodis.Tema))
                {
                    temuListas.Add(zodis.Tema);
                }
            }
            return temuListas;
        }

        public List<Zodis> VisiZodziai()
        {
            var visiZodziai = _vadybintiKartuviuDb.GautiVisusZodzius();
            return visiZodziai;
        }

        
        public List<Zodis> LikeTemosZodziai(string tema, List<Zodis> suzaistiTemosZodziai) //geriau pavadinti LikeTemosZodziai
        {
            var visiZodziai = _vadybintiKartuviuDb.GautiVisusZodzius();
            var temosZodziai = visiZodziai.Where(t => t.Tema == tema).ToList();
            foreach (var temosZodis in temosZodziai)
            {
                foreach (var suzaistasTemosZodis in suzaistiTemosZodziai)
                {
                    if (temosZodis.Tema == suzaistasTemosZodis.Tema)
                    {
                        temosZodziai.Remove(temosZodis);
                        break;
                    }
                }
            }
            return temosZodziai;
        }        

        public List<Statistika> ZaidejoStatistika(string zaidejas)
        {
            var visaStatistika = _vadybintiKartuviuDb.GautiVisaStatistika();
            var zaidejoStatistika = visaStatistika.Where(s => s.ZaidejoVardas == zaidejas).ToList();
            return zaidejoStatistika;
        }
    }
}
