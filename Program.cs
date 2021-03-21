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
        static readonly List<string> bandytosRaides = new List<string>();
        static readonly List<string> spetiZodziai = new List<string>();

        static void Main(string[] args)
        {
            Kartuves();
        }

        static void Kartuves() //viskas OK
        {
            Console.WriteLine("Žaidimas \"Kartuvės\"");
            Console.WriteLine($"sužaisti žodžiai: {string.Join(", ",spetiZodziai)}");
            string tema = TemosPasirinkimas();
            if (tema == "vardai")
            {
                ZaistiZaidima(vardai);               
            }
            else if (tema == "miestai")
            {
                ZaistiZaidima(miestai);               
            }
            else if (tema == "valstybes")
            {
                ZaistiZaidima(valstybes);                
            }
            else
            {
                ZaistiZaidima(sportas);                
            }

            Console.WriteLine();
            
        }

        static void ZaistiZaidima(List<string> likeZodziaiSarase)
        {
            string spejamasZodis = string.Empty;
            int sansai = 0;
            spejamasZodis = ZodzioGeneravimas(likeZodziaiSarase); //sugeneruojam zodi is vardu saraso
            spetiZodziai.Add(spejamasZodis); //issaugom sugeneruota zodi speliotu zodziu sarase              
            Console.WriteLine(kartuves[0]); //isvedam pradini kartuviu vaizda
            do
            {
                SpejamoZodzioVizualizacija(spejamasZodis); // zodzio "uzkodavimas" arba kitaip pradine vizualizacija                    
                Console.WriteLine($"Jūs jau bandėte spėti šias raides: {String.Join(" ", bandytosRaides)}"); //parašome kokias raides zaidejas jau bande
                bandytosRaides.Add(raidesArbaZodzioSpejimas(spejamasZodis).ToUpper()); //iskvieciam spejimo metoda (ten pasitikrinam ar spejamas visas zodis) ir jeigu spejama raide, ja prisidedam prie bandytu saraso
                sansai = sansai + ZaidziamRaide(bandytosRaides[bandytosRaides.Count - 1], spejamasZodis); //iskvieciam raides suzaidimo metoda, kuriame patirkinam ar raide yra jei yra perpiesiam vizualizacija, jei nera padidinam sansu kieki
                Console.WriteLine(kartuves[sansai]);
                Console.WriteLine("--------------------------------------------------------");
            } while (sansai < 7);
            if (sansai == 7)
            {
                Console.WriteLine("Deje, Jūs išnaudojote visus bandymus ir pralaimėjote šį žaidimą!");
                ArZaisiteDarKarta();
            }

        }  

        static int ZaidziamRaide(string spejimas, string spejamasZodis) //veikia OK
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

        static string raidesArbaZodzioSpejimas(string spejamasZodis) //reikia pakoreguoti kad neleistu ivesti ilgesnio/trumpesnio zodzio ir jeigu atspetos visos raides reikia uzskaityti laimejima
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
                    else if (spejimas.ToUpper() == spejamasZodis.ToUpper())
                    {
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine(" Sveikiname, Jūs atspėjote žodį ir laimėjote šį žaidimą!");
                        Console.WriteLine("--------------------------------------------------------");
                        ArZaisiteDarKarta();
                    }
                    else
                    {
                        Console.WriteLine("Deje, Jūsų spėjimas neteisingas, Jūs pralaimėjote šį žaidimą!");
                        Console.WriteLine(kartuves[7]);
                        ArZaisiteDarKarta();
                    }  
                }
                else x = 1;
            }
            return spejimas; 

        }

        static void ArZaisiteDarKarta() //veikia OK
        {
            Console.WriteLine("Ar bandysite dar karta?");
            int x = 0;
            while (x != 1)
            {
                string arZaisimeToliau = Console.ReadLine();
                if (arZaisimeToliau != null && (arZaisimeToliau == "t" || arZaisimeToliau == "T"))
                {
                    x = 1;
                    bandytosRaides.Clear();
                    Kartuves();
                }
                else if (arZaisimeToliau != null && (arZaisimeToliau == "n" || arZaisimeToliau == "N"))
                {
                    System.Environment.Exit(1);                    
                }
                else
                {
                    Console.Write("Neteisinga įvestis! Prašome įvesti t/T arba n/N");
                }
            }
        }

        static bool ArRaides(string spejimas) //veikia OK
        {
            foreach (char raide in spejimas)
            {
                if (!Char.IsLetter(raide))
                    return false;
            }
            return true;
        }

        static void SpejamoZodzioVizualizacija(string spejamasZodis) //veikia OK
        {
            Console.WriteLine($"Spėkite žodį iš {spejamasZodis.Length} raidžių");
            Console.Write("(");
            int visuRaidziuAtspejimoPatikrinimas = 0; 
            foreach (var raide in spejamasZodis)
            {
                if (bandytosRaides.Contains(raide.ToString().ToUpper()))
                {
                    Console.Write($" {raide}");
                    visuRaidziuAtspejimoPatikrinimas += 1;
                }
                else Console.Write(" _");
            }
            Console.Write(" )");
            if (spejamasZodis.Length == visuRaidziuAtspejimoPatikrinimas)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Sveikiname, Jūs atspėjote visas raides ir laimėjote šį žaidimą!");
                Console.WriteLine("---------------------------------------------------------------");
                ArZaisiteDarKarta();
            }

            Console.WriteLine();
        }

        static string TemosPasirinkimas() //veikia OK
        {
            String[] temosString = {"vardai", "miestai", "valstybes", "sportas" };
            Console.WriteLine("Prašome pasirinkti temą: 1. Vardai, 2. Miestai, 3. Valstybės, 4. Sportas");
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


        static string ZodzioGeneravimas(List<string> zodziuListas) //veikia OK
        {
            Random rand = new Random();
            int listoNarioNumeris = rand.Next(0, zodziuListas.Count);
            var zodis = zodziuListas[listoNarioNumeris];
            zodziuListas.RemoveAt(listoNarioNumeris);
            return zodis;
        }

    }
}
