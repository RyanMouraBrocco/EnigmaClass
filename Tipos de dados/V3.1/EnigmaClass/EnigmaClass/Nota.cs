using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaClass
{
    public class Nota
    {
        public int ID { get; set; }
        public Usuario Usuario { get; set; }
        public Exercicio Exercicio { get; set; }
        public int Tentativa { get; set; }
        public decimal _Nota { get; set; }

    }
}
