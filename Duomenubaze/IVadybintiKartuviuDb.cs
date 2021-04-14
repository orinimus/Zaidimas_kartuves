using System.Collections.Generic;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Duomenubaze
{
    public interface IVadybintiKartuviuDb
    {
        List<Zodis> GautiVisusZodzius();
    }
}