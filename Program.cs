﻿using System;
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

        static void Main(string[] args)
        {
            Kartuvės();

            Console.WriteLine("------------Press any key to continue---------------");
            Console.ReadKey();
        }

        static void Kartuvės()
        {
            Console.WriteLine("Žaidimas \"Kartuvės\"");
            if (TemosPasirinkimas() == "vardai")
            {
                SpejamoZodzioGeneravimas(ZodzioGeneravimas(vardai));
            }
            else if (TemosPasirinkimas() == "miestai")
            {

                SpejamoZodzioGeneravimas(ZodzioGeneravimas(miestai));
            }
            else if (TemosPasirinkimas() == "valstybes")
            {

                SpejamoZodzioGeneravimas(ZodzioGeneravimas(valstybes));
            }
            else
                SpejamoZodzioGeneravimas(ZodzioGeneravimas(sportas));

            Console.WriteLine();
            //papildomai ismesti sugeneruota zodi is 

            //SpejamoZodzioGeneravimas(ZodzioGeneravimas(vardai));

        }

        static void PiesinioIsvedimas(List<char> panaudotosNeteisingosRaides)
        {
            Console.WriteLine(kartuves[panaudotosNeteisingosRaides.Count - 1]);
        }

        static void PridetiRaidePrieNeteisingu(char raide)
        {
            neteisingosRaides.Add(raide);
        }

        static void SpejamoZodzioGeneravimas(string spejamasZodis)
        {
            Console.Write("(");
            for (int i = 0; i < spejamasZodis.Length; i++)
            {
                Console.Write(" _");
            }
            Console.Write(" )");
            Console.WriteLine();
        }


        static string TemosPasirinkimas()
        {
            Dictionary<int, string> tema = new Dictionary<int, string>
            {
                {1, "vardai" },
                {2, "miestai" },
                {3, "valstybes" },
                {4, "sportas" }
            };
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
            Console.WriteLine($" Jūs pasirinkote temą \"{tema[skaiciukas]}\"");
            return tema[skaiciukas];
        }

        
        static string ZodzioGeneravimas(List<string> zodziuListas)
        {
            Random rand = new Random();
            int listoNarioNumeris = rand.Next(0, zodziuListas.Count);
            zodziuListas.RemoveAt(listoNarioNumeris);
            return zodziuListas[listoNarioNumeris];
        }

    }
}
