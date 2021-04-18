using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Duomenubaze
{
    public class VadybintiKartuviuDb : IVadybintiKartuviuDb
    {
        private readonly KartuvesContext _context;

        public VadybintiKartuviuDb(KartuvesContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        public List<Zodis> GautiVisusZodzius()
        {
            return _context.Zodziai.ToList(); 
        }

        public List<Statistika> GautiVisaStatistika()
        {
            return _context.Statistikos.ToList();
        }

        public void PapildytiStatistika(string zaidejas, int sansai, bool arAtspejo, int zodzioId)
        {
            _context.Statistikos.Add(new Statistika { ZaidejoVardas = zaidejas, ZodisId = zodzioId, KiekKartuSpeta = sansai, ArAtspejo = arAtspejo, KadaZaide = DateTime.Now });
            _context.SaveChanges();

        }

    }
}
