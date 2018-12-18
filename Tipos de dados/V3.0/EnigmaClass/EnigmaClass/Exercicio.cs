using System.Collections.Generic;

namespace EnigmaClass
{
    public class Exercicio
    {
        public int ID { get; set; }
        public Conteudo Conteudo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public bool AleatorioQuestao { get; set; }
        public List<Questao> Questao { get; set; }
        public int Usuario { get; set; }
    }
}
