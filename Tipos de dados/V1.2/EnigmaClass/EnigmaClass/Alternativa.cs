namespace EnigmaClass
{
    public class Alternativa
    {
        public int ID { get; set; }
        public Questao Questao { get; set; }
        public string Tipo { get; set; }
        public string Conteudo { get; set; }
        public int Usuario { get; set; }
    }
}
