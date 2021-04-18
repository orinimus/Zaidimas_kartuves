using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zaidimas_kartuves.Duomenubaze;
using Zaidimas_kartuves.Modeliai;
using Zaidimas_kartuves.Servisas;

namespace Zaidimas_kartuves
{
    static class Program
    {
        static readonly Dictionary<int, string> kartuves = new Dictionary<int, string> //paveiksliuko generavimui
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

        static readonly List<string> bandytosRaides = new List<string>();
        static readonly List<string> sarasasZaidejuiSaugoti = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Žaidimas \"Kartuvės\"");
            sarasasZaidejuiSaugoti.Add(IvestiVarda()); //gaunam varda is vartotojo ir saugom iki naujo inicializavimo
            Kartuves();
        }

        static void Kartuves() 
        {
            using (var context = new KartuvesContext())
            {
                IVadybintiKartuviuDb vadybintiKartuviuDb = new VadybintiKartuviuDb(context);
                IDuomenuApdorojimas duomenuApdorojimas = new DuomenuApdorojimas(vadybintiKartuviuDb);
                //string zaidejas = IvestiVarda(); //gaunam varda is vartotojo
                ZaidejoStatistika(sarasasZaidejuiSaugoti[0]); //pasiklausiam ar nori statistikos, jei nori atspausdinam
                var zaidejoStatistika = duomenuApdorojimas.ZaidejoStatistika(sarasasZaidejuiSaugoti[0]); //pagal zaideja istraukiam visa statistika ka zaide zaidejas
                var suzaistiZodziai = SuzaistuZodziuGrazinimas(zaidejoStatistika);
                var kiekKartuSpeliota = zaidejoStatistika.Count; 
                if (kiekKartuSpeliota != 0)
                {
                    Console.WriteLine();
                    Console.Write($"sužaisti žodžiai : ");
                    foreach (var zodis in suzaistiZodziai)
                    {
                        Console.Write(zodis.Zodelis);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Kolkas žodžių dar nebuvo sužaista");
                }

                List<Zodis> likeTemosZodziai = new List<Zodis>();
                string tema = string.Empty;

                int x = 0;
                while (x < 1)
                {
                    tema = TemosPasirinkimas();                    
                    var suzaistiTemosZodziai = SuzaistuTemosZodziuGrazinimas(tema, suzaistiZodziai);
                    likeTemosZodziai = duomenuApdorojimas.LikeTemosZodziai(tema, suzaistiTemosZodziai); //Nepadarem listo sumazinimo
                    if (likeTemosZodziai.Count == 0)
                    {
                        Console.WriteLine($"Jūs esate sužaidęs visus žodžius iš temos {tema}, prašome pasirinkti kitą temą");
                    }
                    else
                    {
                        x = 1;
                    }
                }

                if (tema == "VARDAS")
                {
                    ZaistiZaidima(likeTemosZodziai, sarasasZaidejuiSaugoti[0]); 
                }
                else if (tema == "MIESTAS")
                {
                    ZaistiZaidima(likeTemosZodziai, sarasasZaidejuiSaugoti[0]);
                }
                else if (tema == "VALSTYBĖ")
                {
                    ZaistiZaidima(likeTemosZodziai, sarasasZaidejuiSaugoti[0]);
                }
                else if (tema == "SPORTAS")
                {
                    ZaistiZaidima(likeTemosZodziai, sarasasZaidejuiSaugoti[0]);
                }
                else if (tema == "GYVŪNAS")
                {
                    ZaistiZaidima(likeTemosZodziai, sarasasZaidejuiSaugoti[0]);
                }
                Console.WriteLine();
            }
        }

        static List<Zodis> SuzaistuZodziuGrazinimas(List<Statistika> zaidejoStatistika)
        {
            List<Zodis> suzaistiZodziai = new List<Zodis>();
            foreach (var stat in zaidejoStatistika)
            {
                suzaistiZodziai.Add(new Zodis { ZodisId = stat.ZodisId }); //suformuojam zaistu zodziu listą 
            }

            return suzaistiZodziai;

        }

        static List<Zodis> SuzaistuTemosZodziuGrazinimas(string tema, List<Zodis> suzaistiZodziai)
        {
            List<Zodis> suzaistiTemosZodziai = new List<Zodis>();
            foreach (var zodis in suzaistiZodziai)
            {
                if (zodis.Tema == tema)
                {
                    suzaistiTemosZodziai.Add(new Zodis { Tema = zodis.Tema }); //suformuojam suzaistu tos temos zodziu lista
                }
            }
            return suzaistiTemosZodziai;
        }

        private static void ZaidejoStatistika(string zaidejas)
        {
            Console.WriteLine("Ar pageidaujate pamatyti savo statistiką t/n?");
            while (true)
            {

                char s = (char)Console.ReadKey().Key;
                if ( s == (char)ConsoleKey.N)
                {
                    return;
                }
                else if ( s == (char)ConsoleKey.T)
                {
                    using (var context = new KartuvesContext())
                    {
                        IVadybintiKartuviuDb vadybintiKartuviuDb = new VadybintiKartuviuDb(context);
                        IDuomenuApdorojimas duomenuApdorojimas = new DuomenuApdorojimas(vadybintiKartuviuDb);

                        Console.WriteLine();
                        Console.WriteLine($"Žaidėjo {zaidejas} statistika: ");
                        var zaidejoStatistika = duomenuApdorojimas.ZaidejoStatistika(zaidejas);
                        var kiekKartuSpeta = zaidejoStatistika.Count;
                        var kiekKartuAtspeta = zaidejoStatistika.Count(a => a.ArAtspejo is true);
                        if (kiekKartuSpeta != 0)
                        {
                            Console.WriteLine($"spėta {kiekKartuSpeta} iš jų {kiekKartuAtspeta} kartai atspėti, atspėtų santykis {AtspetuSantykis(kiekKartuSpeta, kiekKartuAtspeta)}%");
                        }
                        else
                        {
                            Console.WriteLine("Dar nebuvo spėtas nei vienas žodis");
                        }
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("prašome paspausti t arba T jei sutinkate matyti savo statistiką ir n abra N jei nesutinkate");
                }
            }
        } //OK

        static string AtspetuSantykis(int kiekKartuSpeta, int kiekKartuAtspeta)
        {
            double santykisProc = ((double)kiekKartuAtspeta / (double)kiekKartuSpeta)*100;
            string santykis = santykisProc.ToString("#.##");
            return santykis;
        }

        static void ZaistiZaidima(List<Zodis> likeTemosZodiai, string zaidejas)
        {
            using (var context = new KartuvesContext())
            {
                IVadybintiKartuviuDb vadybintiKartuviuDb = new VadybintiKartuviuDb(context);
                string spejamasZodis = string.Empty;
                int sansai = 0;
                int zodzioId = 0;
                spejamasZodis = ZodzioGeneravimas(likeTemosZodiai, out zodzioId); //sugeneruojam zodi is Zodziu duombazes OK
                Console.WriteLine(kartuves[0]); //isvedam pradini kartuviu vaizda
                Console.WriteLine("------------------------------------------------------------");
                do
                {
                    int laimejimas = 0;
                    SpejamoZodzioVizualizacija(spejamasZodis, zaidejas, vadybintiKartuviuDb, sansai, zodzioId); // zodzio "uzkodavimas" arba kitaip pradine vizualizacija
                    if (bandytosRaides.Count != 0)
                    {
                        Console.WriteLine($"Jūs jau bandėte spėti šias raides: {String.Join(" ", bandytosRaides)}"); //parašome kokias raides zaidejas jau bande
                    }
                    else
                    {
                        Console.WriteLine("Kolkas nebandėte nei vienos raidės");
                    }
                    bandytosRaides.Add(RaidesArbaZodzioSpejimas(spejamasZodis, zaidejas, vadybintiKartuviuDb, sansai, zodzioId).ToUpper()); //iskvieciam spejimo metoda (ten pasitikrinam ar spejamas visas zodis) ir jeigu spejama raide, ja prisidedam prie bandytu saraso
                    if (laimejimas == 1)
                    {
                        Laimejimas(zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
                    }
                    sansai += ZaidziamRaide(bandytosRaides[bandytosRaides.Count - 1], spejamasZodis, zaidejas, vadybintiKartuviuDb, sansai, zodzioId); //iskvieciam raides suzaidimo metoda, kuriame patirkinam ar raide yra jei yra perpiesiam vizualizacija, jei nera padidinam sansu kieki
                        Console.WriteLine(kartuves[sansai]);
                        Console.WriteLine("------------------------------------------------------------");
                } while (sansai < 7);
                    if (sansai == 7)
                {
                    Pralaimejimas(zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
                }
            }
        }

        private static void Pralaimejimas(string zaidejas, IVadybintiKartuviuDb vadybintiKartuviuDb, int sansai, int zodzioId)
        {
            Console.WriteLine();
            Console.WriteLine(kartuves[7]);
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Deje, Jūs pralaimėjote šį žaidimą!");
            Console.WriteLine("----------------------------------");
            var arAtspejo = false;
            vadybintiKartuviuDb.PapildytiStatistika(zaidejas, sansai, arAtspejo, zodzioId);
            ArZaisiteDarKarta();
        }

        private static void Laimejimas(string zaidejas, IVadybintiKartuviuDb vadybintiKartuviuDb, int sansai, int zodzioId)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(" Sveikiname, Jūs atspėjote žodį ir laimėjote šį žaidimą!");
            Console.WriteLine("--------------------------------------------------------");
            var arAtspejo = true;
            vadybintiKartuviuDb.PapildytiStatistika(zaidejas, sansai, arAtspejo, zodzioId);
            ArZaisiteDarKarta();
        }

        static int ZaidziamRaide(string spejimas, string spejamasZodis, string zaidejas, IVadybintiKartuviuDb vadybintiKartuviuDb, int sansai, int zodzioId) 
        {
            if (spejamasZodis.ToUpper().Contains(spejimas.ToUpper()))
            {
                SpejamoZodzioVizualizacija(spejamasZodis, zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
                return 0;
            }
            else
            {
                Console.WriteLine($"Raidės {spejimas}, šiame žodyje nėra");
                SpejamoZodzioVizualizacija(spejamasZodis, zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
                return 1;
            }
        }

        static string RaidesArbaZodzioSpejimas(string spejamasZodis, string zaidejas, IVadybintiKartuviuDb vadybintiKartuviuDb, int sansai, int zodzioId)
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
                            Laimejimas(zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
                        }
                        else
                        {
                            Pralaimejimas(zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
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

        static void ArZaisiteDarKarta() // TODO papraščiau
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

        static void SpejamoZodzioVizualizacija(string spejamasZodis, string zaidejas, IVadybintiKartuviuDb vadybintiKartuviuDb, int sansai, int zodzioId) 
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
                Laimejimas(zaidejas, vadybintiKartuviuDb, sansai, zodzioId);
            }
            Console.WriteLine();
        } //OK

        static string IvestiVarda()
        {
            Console.WriteLine("Įveskite žaidėjo vardą: ");
            string zaidejas = Console.ReadLine();
            return zaidejas;            
        } //OK

        static string TemosPasirinkimas() 
        {
            using (var context = new KartuvesContext())
            {
                IVadybintiKartuviuDb vadybintiKartuviuDb = new VadybintiKartuviuDb(context);
                IDuomenuApdorojimas duomenuApdorojimas = new DuomenuApdorojimas(vadybintiKartuviuDb);
                int i = 1;
                var temuSarasas = duomenuApdorojimas.TemuIsvedimas(); //parnesa iš zodziu duombazes unikaliu temu sarasa
                Console.Write("Prašome pasirinkti temą: ");

                foreach (var tema in temuSarasas) 
                {
                    Console.Write($"{i}. {tema} ");
                    i += 1;
                }
                Console.WriteLine();
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
                    else Console.WriteLine($" - neteisinga įvestis, prašome paspausti 1, 2, 3, 4 arba 5");
                }
                Console.WriteLine($" Jūs pasirinkote temą \"{temuSarasas[skaiciukas - 1]}\"");
                return temuSarasas[skaiciukas - 1];
            }           
        } //OK

        static string ZodzioGeneravimas(List<Zodis> zodziuListas, out int zodzioID) 
        {
            Random rand = new Random();
            int listoNarioNumeris = rand.Next(0, zodziuListas.Count);
            var zodis = zodziuListas[listoNarioNumeris];
            zodzioID = zodis.ZodisId;
            return zodis.Zodelis;
        } //OK

    }
}
