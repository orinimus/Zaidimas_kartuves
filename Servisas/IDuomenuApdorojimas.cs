using System.Collections.Generic;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Servisas
{
    public interface IDuomenuApdorojimas
    {
        List<Zodis> LikeTemosZodziai(string tema, List<Zodis> suzaistiTemosZodziai);
        List<string> TemuIsvedimas();
        List<Zodis> VisiZodziai();
        List<Statistika> ZaidejoStatistika(string zaidejas);
    }
}