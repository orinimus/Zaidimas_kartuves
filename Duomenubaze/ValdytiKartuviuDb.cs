using System;
using System.Collections.Generic;
using System.Text;

namespace Zaidimas_kartuves.Duomenubaze
{
    public class VadybintiKartuviuDb
    {
        private readonly KartuvesContext _context;

        public VadybintiKartuviuDb(KartuvesContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

    }
}
