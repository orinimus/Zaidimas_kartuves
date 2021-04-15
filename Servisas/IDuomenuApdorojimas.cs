using System.Collections.Generic;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Servisas
{
    public interface IDuomenuApdorojimas
    {
        List<Zodis> LikeZodziaiSarase(string tema);
        List<string> TemuIsvedimas();
        List<Statistika> ZaidejoStatistika(string zaidejas);
    }
}