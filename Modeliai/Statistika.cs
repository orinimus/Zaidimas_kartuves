using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zaidimas_kartuves.Modeliai
{
    public class Statistika
    {
        [Key]
        public int StatistikaId { get; set; }
        public string ZaidejoVardas { get; set; }
        public int ZodisId { get; set; }
        public int KiekKartuSpeta { get; set; }
        public bool ArAtspejo { get; set; }
        public DateTime KadaZaide { get; set; }
    }
}
