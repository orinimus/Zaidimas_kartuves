using System;
using System.Collections.Generic;
using System.Text;

namespace Zaidimas_kartuves
{
    static class Program
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

        static void Kartuves() 
        {
            Console.WriteLine("Žaidimas \"Kartuvės\"");
            if (spetiZodziai.Count != 0)
            {
                Console.WriteLine($"sužaisti žodžiai: {string.Join(", ", spetiZodziai)}");                
            }
            else
            {
                Console.WriteLine("Kolkas žodžių dar nebuvo sužaista");
            }                
            string tema = TemosPasirinkimas();
            if (tema == "vardai")
            {
                ZaistiZaidima(vardai, tema);                          
            }
            else if (tema == "miestai")
            {
                ZaistiZaidima(miestai, tema);               
            }
            else if (tema == "valstybes")
            {
                ZaistiZaidima(valstybes, tema);                
            }
            else if (tema == "sportas")
            {
                ZaistiZaidima(sportas, tema);                
            }
            Console.WriteLine();
            
        }

        static void ZaistiZaidima(List<string> likeZodziaiSarase, string tema)
        {
            string spejamasZodis = string.Empty;
            int sansai = 0;
            if (likeZodziaiSarase.Count > 0)
            {
                spejamasZodis = ZodzioGeneravimas(likeZodziaiSarase); //sugeneruojam zodi is vardu saraso
                spetiZodziai.Add(spejamasZodis); //issaugom sugeneruota zodi speliotu zodziu sarase              
                Console.WriteLine(kartuves[0]); //isvedam pradini kartuviu vaizda
                do
                {
                    SpejamoZodzioVizualizacija(spejamasZodis); // zodzio "uzkodavimas" arba kitaip pradine vizualizacija
                    if (bandytosRaides.Count != 0)
                    {
                        Console.WriteLine($"Jūs jau bandėte spėti šias raides: {String.Join(" ", bandytosRaides)}"); //parašome kokias raides zaidejas jau bande
                    }
                    else
                    {
                        Console.WriteLine("Kolkas nebandėte nei vienos raidės");
                    }                    
                    Console.WriteLine("--------------------------------------------------------");
                    bandytosRaides.Add(RaidesArbaZodzioSpejimas(spejamasZodis).ToUpper()); //iskvieciam spejimo metoda (ten pasitikrinam ar spejamas visas zodis) ir jeigu spejama raide, ja prisidedam prie bandytu saraso
                    sansai += ZaidziamRaide(bandytosRaides[bandytosRaides.Count - 1], spejamasZodis); //iskvieciam raides suzaidimo metoda, kuriame patirkinam ar raide yra jei yra perpiesiam vizualizacija, jei nera padidinam sansu kieki
                    Console.WriteLine(kartuves[sansai]);
                } while (sansai < 7);
                if (sansai == 7)
                {
                    Console.WriteLine("Deje, Jūs išnaudojote visus bandymus ir pralaimėjote šį žaidimą!");
                    ArZaisiteDarKarta();
                }
            }
            else            
            {
                Console.WriteLine($"Jūs sužaidėte visus žodžius iš temos{tema}, prašome pasirinkti kitą temą");
                Kartuves();
            }            

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

        static string RaidesArbaZodzioSpejimas(string spejamasZodis)
        {
            string spejimas = string.Empty;
            Console.WriteLine("spėkite raidę arba visą žodį");

            int x = 0;
            while (x != 1)
            {
                spejimas = Console.ReadLine();
                if (!bandytosRaides.Contains(spejimas.ToUpper()))
                {
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
                else
                {
                    Console.WriteLine($"Jūs jau badėte spėti {spejimas}");
                }               
            }
            return spejimas; 
        }

        static void ArZaisiteDarKarta() 
        {
            Console.WriteLine("Ar bandysite dar karta?");
            int x = 0; 
            while (x != 1)
            {
                string arZaisimToliau = Console.ReadKey().KeyChar.ToString();
                if (arZaisimToliau.Equals("t", StringComparison.OrdinalIgnoreCase))
                {
                    x = 1;
                    bandytosRaides.Clear();
                    Kartuves();
                }
                else if (arZaisimToliau.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine(" - neteisinga įvestis! Prašome įvesti t/T arba n/N");
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

        static void SpejamoZodzioVizualizacija(string spejamasZodis) 
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
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Sveikiname, Jūs atspėjote visas raides ir laimėjote šį žaidimą!");
                Console.WriteLine("---------------------------------------------------------------");
                ArZaisiteDarKarta();
            }

            Console.WriteLine();
        }

        static string TemosPasirinkimas() 
        {
            String[] temosString = {"vardai", "miestai", "valstybes", "sportas" };
            
            
            Console.WriteLine("Prašome pasirinkti temą: 1. Vardas, 2. Miestas, 3. Valstybė, 4. Sportas, 5. Gyvūnas");
            int x = 0;
            int skaiciukas = 0;
            while (x != 1)
            {
                char temosPasirinkimas = Console.ReadKey().KeyChar;
                if (int.TryParse(temosPasirinkimas.ToString(), out skaiciukas))
                {
                    if (skaiciukas < 1 || skaiciukas > 5)
                    {
                        Console.WriteLine($"Pasirinkimas {skaiciukas}, neatitinka minėtų temų");
                    }
                    else x = 1;
                }
                else Console.WriteLine($" - neteisinga įvestis, prašome paspausti 1, 2, 3 arba 4");
            }
            Console.WriteLine($" Jūs pasirinkote temą \"{temosString[skaiciukas-1]}\"");
            return temosString[skaiciukas - 1];
        }

        static string ZodzioGeneravimas(List<string> zodziuListas) 
        {
            Random rand = new Random();
            int listoNarioNumeris = rand.Next(0, zodziuListas.Count);
            var zodis = zodziuListas[listoNarioNumeris];
            zodziuListas.RemoveAt(listoNarioNumeris);
            return zodis;
        }

    }
}
