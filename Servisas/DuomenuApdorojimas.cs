using System;
using System.Collections.Generic;
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
    }
}
