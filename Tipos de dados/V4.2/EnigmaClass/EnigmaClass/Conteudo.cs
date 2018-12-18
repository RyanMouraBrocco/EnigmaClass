using System.Collections.Generic;

namespace EnigmaClass
{
    public class Conteudo
    {
        public Conteudo()
        {
        }

        public Conteudo(int iD, Materia materia, string nome, byte[] imagem, int ordem, int usuario)
        {
            ID = iD;
            Materia = materia;
            Nome = nome;
            Imagem = imagem;
            Ordem = ordem;
            Usuario = usuario;
        }

        public Conteudo(int iD, Materia materia, string nome, byte[] imagem, int ordem, List<ConteudoTexto> conteudoTexto, List<Exercicio> exercicio, List<Resumo> resumo, int usuario)
        {
            ID = iD;
            Materia = materia;
            Nome = nome;
            Imagem = imagem;
            Ordem = ordem;
            ConteudoTexto = conteudoTexto;
            Exercicio = exercicio;
            Resumo = resumo;
            Usuario = usuario;
        }

        public int ID { get; set; }
        public Materia Materia{ get; set; }
        public string Nome { get; set; }
        public byte[] Imagem { get; set; }
        public int Ordem { get; set; }
        public List<ConteudoTexto> ConteudoTexto { get; set; }
        public List<Exercicio> Exercicio { get; set; }
        public List<Resumo> Resumo { get; set; }
        public int Usuario { get; set; }
    }
}
