using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zaidimas_kartuves.Modeliai
{
    public class Zodis
    {
        [Key]
        public int ZodisId { get; set; }
        public EZodziuTemos Tema { get; set; }
        public string Zodelis { get; set; }
        public int KiekKartuSpeta { get; set; }
        public int KiekKartuAtspeta { get; set; }
    }
}
