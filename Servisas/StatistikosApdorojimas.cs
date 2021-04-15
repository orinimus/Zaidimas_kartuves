using System;
using System.Collections.Generic;
using System.Text;
using Zaidimas_kartuves.Duomenubaze;

namespace Zaidimas_kartuves.Servisas
{
    public class StatistikosIsvedimas
    {
        private readonly IVadybintiKartuviuDb _vadybintiKartuviuDb;

        public StatistikosIsvedimas(IVadybintiKartuviuDb vadybintiKartuviuDb)
        {
            _vadybintiKartuviuDb = vadybintiKartuviuDb;
        }




    }
}
