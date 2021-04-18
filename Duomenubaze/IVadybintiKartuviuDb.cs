using System.Collections.Generic;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Duomenubaze
{
    public interface IVadybintiKartuviuDb
    {
        List<Statistika> GautiVisaStatistika();
        List<Zodis> GautiVisusZodzius();
        void PapildytiStatistika(string zaidejas, int sansai, bool arAtspejo, int zodzioId);
    }
}