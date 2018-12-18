using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaClass
{
    public class Resumo
    {
        public int ID { get; set; }
        public Conteudo Conteudo { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Arquivo { get; set; }
        public string Extensao { get; set; }
        public int Usuario { get; set; }
    }
}
