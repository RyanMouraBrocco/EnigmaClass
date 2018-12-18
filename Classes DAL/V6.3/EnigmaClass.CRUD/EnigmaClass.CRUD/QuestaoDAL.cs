using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class QuestaoDAL
    {
        public QuestaoDAL()
        {
        }
        /// <summary>
        /// Cria uma questao relacionada ao um exercicio
        /// Necessitando apenas do ID do Exercicio
        /// </summary>
        /// <param name="Q"> parametro do tipo Questao | sem id </param>
        public void Criar(Questao Q)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirQuestao";
            comm.Parameters.Add("@Exercicio", SqlDbType.Int).Value = Q.Exercicio.ID;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = Q.Ordem;
            comm.Parameters.Add("@AleatorioAlternativa", SqlDbType.Bit).Value = Q.AleatorioAlternativa;
            comm.Parameters.Add("@Pergunta", SqlDbType.Text).Value = Q.Pergunta;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = Q.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        /// <summary>
        /// Altera uma questao relacionada ao um exercicio
        /// Necessitando apenas do ID do Exercicio
        /// </summary>
        /// <param name="Q">parametro do tipo Questao | com id</param>
        public void Alterar(Questao Q)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarQuestao";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = Q.ID;
            comm.Parameters.Add("@Exercicio", SqlDbType.Int).Value = Q.Exercicio.ID;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = Q.Ordem;
            comm.Parameters.Add("@AleatorioAlternativa", SqlDbType.Bit).Value = Q.AleatorioAlternativa;
            comm.Parameters.Add("@Pergunta", SqlDbType.Text).Value = Q.Pergunta;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = Q.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        /// <summary>
        /// Retorna um objeto do tipo Questao completo 
        /// Contendo list de ALternativa (Completo)
        /// </summary>
        /// <param name="id"> parametro do tipo inteiro representando o ID da Questao </param>
        /// <returns></returns>
        public Questao Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Questao Where ID_Questao = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Questao q = new Questao();
            while (dr.Read())
            {
                q.ID = Convert.ToInt32(dr.GetValue(0));
                Exercicio e = new Exercicio();
                e.ID= Convert.ToInt32(dr.GetValue(1));
                q.Exercicio = e;
                q.Ordem = Convert.ToInt32(dr.GetValue(2));
                q.AleatorioAlternativa = Convert.ToBoolean(dr.GetValue(3));
                q.Pergunta = dr.GetValue(4).ToString();
                q.Usuario = Convert.ToInt32(dr.GetValue(5));
            }
            dr.Close();
            comm.CommandText = "Select ID_Alternativa, Ordem_Alternativa from Alternativa Where ID_Questao = " + id+" order by Ordem_Alternativa";
            dr = comm.ExecuteReader();
            List<Alternativa> lista = new List<Alternativa>();
            while (dr.Read())
            {
                AlternativaDAL dalalter = new AlternativaDAL();
                Alternativa a = new Alternativa();
                a = dalalter.Consultar(Convert.ToInt32(dr.GetValue(0)));
                lista.Add(a);
            }
            q.Alternativa = lista;
            comm.Connection.Close();
            return q;
        }
    }
}
