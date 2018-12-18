using System.Collections.Generic;

namespace EnigmaClass
{
    public class Conteudo
    {
        public int ID { get; set; }
        public Materia Materia{ get; set; }
        public string Nome { get; set; }
        public byte[] Imagem { get; set; }
        public List<ConteudoTexto> ConteudoTexto { get; set; }
        public List<Exercicio> Exercicio { get; set; }
        public int Usuario { get; set; }
    }
}
