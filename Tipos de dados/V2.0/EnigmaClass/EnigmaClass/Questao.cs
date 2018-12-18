using System.Collections.Generic;

namespace EnigmaClass
{
    public class Questao
    {
        public int ID { get; set; }
        public Exercicio Exercicio { get; set; }
        public int Ordem { get; set; }
        public bool AleatorioAlternativa { get; set; }
        public string Pergunta { get; set; }
        public List<Alternativa> Alternativa { get; set; }
        public int Usuario { get; set; }
    }
}
