using System;
using System.Collections.Generic;
using System.Text;

namespace Zaidimas_kartuves
{
    class Program
    {
        static readonly Dictionary<int, string> kartuves = new Dictionary<int, string>
            {
                {0, " ------|\n|\n|\n|\n|\n|\n|\n|\n________" },
                {1, " ------|\n|      o\n|\n|\n|\n|\n|\n|\n________" },
                {2, " ------|\n|      o\n|      |\n|\n|\n|\n|\n|\n________" },
                {3, " ------|\n|      o\n|      |\n|      O\n|\n|\n|\n|\n________" },
                {4, " ------|\n|      o\n|     \\|\n|      O\n|\n|\n|\n|\n________" },
                {5, " ------|\n|      o\n|     \\|/\n|      O\n|\n|\n|\n|\n________" },
                {6, " ------|\n|      o\n|     \\|/\n|      O\n|     /\n|\n|\n|\n________" },
                {7, " ------|\n|      o\n|     \\|/\n|      O\n|     / \\\n|\n|\n|\n________" }
            };
        static readonly List<string> vardai = new List<string> { "Liutauras", "Dominykas", "Augustas", "Klemensas", "Aloyzas", "Aleksandras", "Marijonas", "Vytautas", "Anzelmas", "Gediminas" };
        static readonly List<string> miestai = new List<string> { "Radviliškis", "Anykščiai", "Mažeikiai", "Virbalis", "Kybartai", "Ignalina", "Kalvarija", "Marijampolė", "Tauragė", "Jurbarkas" };
        static readonly List<string> valstybes = new List<string> { "Argentina", "Brazilija", "Meksika", "Jamaika", "Alžyras", "Albanija", "Tailandas", "Kazakstanas", "Armėnija", "Garikija" };
        static readonly List<string> sportas = new List<string> { "vandensvydis", "biatlonas", "šaškės", "penkiakovė", "maratonas", "futbolas", "beisbolas", "kriketas", "akmensvydis", "šachmatai" };
        static readonly List<char> neteisingosRaides = new List<char>();
        static readonly List<string> bandytosRaides = new List<string>();

        static void Main(string[] args)
        {
            Kartuves();

            Console.WriteLine("------------Press any key to continue---------------");
            Console.ReadKey();
        }

        static void Kartuves()
        {
            List<string> spetiZodziai = new List<string>();
            Console.WriteLine("Žaidimas \"Kartuvės\"");
            string spejamasZodis = string.Empty;
            string tema = TemosPasirinkimas();
            int sansai = 0;

            if (tema == "vardai")
            {
                spejamasZodis = ZodzioGeneravimas(vardai); //sugeneruojam zodi is vardu saraso
                spetiZodziai.Add(spejamasZodis); //issaugom sugeneruota zodi speliotu zodziu sarase
                Console.WriteLine(spejamasZodis);
                Console.WriteLine(kartuves[0]); //isvedam pradini kartuviu vaizda
                do
                {
                    SpejamoZodzioVizualizacija(spejamasZodis); // zodzio "uzkodavimas" arba kitaip pradine vizualizacija                    
                    Console.WriteLine($"Jūs jau bandėte spėti šias raides {String.Join(" ", bandytosRaides)}"); //parašome kokias raides zaidejas jau bande
                    bandytosRaides.Add(raidesArbaZodzioSpejimas(spejamasZodis).ToUpper()); //iskvieciam spejimo metoda (ten pasitikrinam ar spejamas visas zodis) ir jeigu spejama raide, ja prisidedam prie bandytu saraso
                    Console.WriteLine($"Test paskutine raide sarase {bandytosRaides[bandytosRaides.Count - 1]}");
                    sansai = sansai + ZaidziamRaide(bandytosRaides[bandytosRaides.Count - 1], spejamasZodis); //iskvieciam raides suzaidimo metoda, kuriame patirkinam ar raide yra jei yra perpiesiam vizualizacija, jei nera padidinam sansu keiki
                    Console.WriteLine(kartuves[sansai]);
                } while (sansai < 7);
                if (sansai == 7)
                {
                    Console.WriteLine("Deje, Jūs i6naudojote visus bandymus ir pralaimėjote šį žaidimą!");
                    Console.WriteLine(kartuves[7]);
                    ArZaisiteDarKarta();
                }
                
            }
            else if (tema == "miestai")
            {
                spejamasZodis = ZodzioGeneravimas(miestai);
                spetiZodziai.Add(spejamasZodis);
                SpejamoZodzioVizualizacija(spejamasZodis);
                Console.WriteLine(kartuves[0]);

            }
            else if (tema == "valstybes")
            {
                spejamasZodis = ZodzioGeneravimas(valstybes);
                spetiZodziai.Add(spejamasZodis);
                SpejamoZodzioVizualizacija(spejamasZodis);
                Console.WriteLine(kartuves[0]);

            }
            else
            {
                spejamasZodis = ZodzioGeneravimas(sportas);
                spetiZodziai.Add(spejamasZodis);
                SpejamoZodzioVizualizacija(spejamasZodis);
                Console.WriteLine(kartuves[0]);

            }

            Console.WriteLine();
            
        }

        static void ZaistiZaidima()
        {
            
        }

        static int ZaidziamRaide(string spejimas, string spejamasZodis)
        {
            if (spejamasZodis.ToUpper().Contains(spejimas.ToUpper()))
            {
                SpejamoZodzioVizualizacija(spejamasZodis);
                return 0;
            }
            else
            {
                Console.WriteLine($"Raidės {spejimas}, šiame žodyje nėra");
                SpejamoZodzioVizualizacija(spejamasZodis);
                return 1;
            }
        }

        static string raidesArbaZodzioSpejimas(string spejamasZodis)
        {
            string spejimas = string.Empty;
            Console.WriteLine("spėkite raidę arba visą žodį");

            int x = 0;
            while (x != 1)
            {
                spejimas = Console.ReadLine();
                if (!ArRaides(spejimas))
                {
                    Console.WriteLine("Neteisinga įvesti, prašome įvesti raidę arba visą žodį");
                }
                else if (spejimas.Length > 1)
                {
                    if (spejimas.Length != spejamasZodis.Length)
                    {
                        Console.WriteLine($"Jūs bandote spėti žodį, tam reikia parašyti {spejamasZodis.Length} raides, o jūs parašėte {spejimas.Length} ");
                    }
                    else if (spejimas == spejamasZodis)
                    {
                        Console.WriteLine("Sveikiname, Jūs atspėjote žodį ir laimėjote šį žaidimą!");
                        ArZaisiteDarKarta();
                    }
                    else
                    {
                        Console.WriteLine("Deje, Jūsų spėjimas neteisingas, Jūs pralaimėjote šį žaidimą!");
                        Console.WriteLine(kartuves[7]);
                        ArZaisiteDarKarta();
                    }
                }
                x = 1;
            }
            return spejimas; 

        }

        static void ArZaisiteDarKarta()
        {
            Console.WriteLine("Ar bandysite dar karta?");
            int x = 0;
            while (x != 1)
            {
                string arZaisimeToliau = Console.ReadLine();
                if (arZaisimeToliau != null && (arZaisimeToliau == "t" || arZaisimeToliau == "T"))
                {
                    x = 1;
                    ZaistiZaidima();
                }
                else if (arZaisimeToliau != null && (arZaisimeToliau == "n" || arZaisimeToliau == "N"))
                {
                    break;
                }
                else
                {
                    Console.Write("Neteisinga įvestis! Prašome įvesti t/T arba n/N");
                }
            }
        }

        static bool ArRaides(string spejimas)
        {
            foreach (char raide in spejimas)
            {
                if (!Char.IsLetter(raide))
                    return false;
            }
            return true;
        }


        static void PiesinioIsvedimas(List<char> panaudotosNeteisingosRaides)
        {
            Console.WriteLine(kartuves[panaudotosNeteisingosRaides.Count - 1]);
        }

        static void PridetiRaidePrieNeteisingu(char raide)
        {
            neteisingosRaides.Add(raide);
        }

        static void SpejamoZodzioVizualizacija(string spejamasZodis)
        {
            Console.WriteLine($"Spėkite žodį iš {spejamasZodis.Length} raidžių");
            Console.Write("(");
            foreach (var raide in spejamasZodis)
            {
                if (bandytosRaides.Contains(raide.ToString().ToUpper()))
                {
                    Console.Write($" {raide}");
                }
                else Console.Write(" _");
            }
            Console.Write(" )");

            Console.WriteLine();
        }

        static string TemosPasirinkimas()
        {
            String[] temosString = {"vardai", "miestai", "valstybes", "sportas" };
            Console.WriteLine("Prašome pasirinkti temą: 1. Vardai, 2. miestai, 3. valstybės, 4. sportas");
            int x = 0;
            int skaiciukas = 0;
            while (x != 1)
            {
                char temosPasirinkimas = Console.ReadKey().KeyChar;
                if (int.TryParse(temosPasirinkimas.ToString(), out skaiciukas))
                {
                    if (skaiciukas < 1 && skaiciukas > 4)
                    {
                        Console.WriteLine($"Pasirinkimas {skaiciukas}, neatitinka minėtų temų");
                    }
                    else x = 1;
                }
                else Console.WriteLine($"neteisinga įvestis, prašome paspausti 1, 2, 3 arba 4");
            }
            Console.WriteLine($" Jūs pasirinkote temą \"{temosString[skaiciukas-1]}\"");
            return temosString[skaiciukas - 1];
        }

        
        static string ZodzioGeneravimas(List<string> zodziuListas)
        {
            Random rand = new Random();
            int listoNarioNumeris = rand.Next(0, zodziuListas.Count + 1);
            zodziuListas.RemoveAt(listoNarioNumeris);
            return zodziuListas[listoNarioNumeris];
        }

    }
}
