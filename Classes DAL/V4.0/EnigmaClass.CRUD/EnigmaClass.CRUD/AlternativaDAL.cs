using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class AlternativaDAL
    {
        public AlternativaDAL()
        {
        }
        public void Criar(Alternativa A)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirAlternativa";
            comm.Parameters.Add("@Questao", SqlDbType.Int).Value = A.Questao.ID;
            comm.Parameters.Add("@Tipo", SqlDbType.Char).Value = A.Tipo;
            comm.Parameters.Add("@Conteudo", SqlDbType.VarChar).Value = A.Conteudo;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = A.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = A.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Alternativa A)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarAlternativa";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = A.ID;
            comm.Parameters.Add("@Questao", SqlDbType.Int).Value = A.Questao.ID;
            comm.Parameters.Add("@Tipo", SqlDbType.Char).Value = A.Tipo;
            comm.Parameters.Add("@Conteudo", SqlDbType.VarChar).Value = A.Conteudo;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = A.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = A.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Alternativa Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Alternativa Where ID_Alternativa = "+id,Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Alternativa a = new Alternativa();
            while (dr.Read())
            {
                a.ID = Convert.ToInt32(dr.GetValue(0));
                Questao q = new Questao();
                q.ID = Convert.ToInt32(dr.GetValue(1));
                a.Questao = q;
                a.Tipo = dr.GetValue(2).ToString();
                a.Conteudo = dr.GetValue(3).ToString();
                a.Ordem = Convert.ToInt32(dr.GetValue(4));
                a.Usuario = Convert.ToInt32(dr.GetValue(5));
            }
            comm.Connection.Close();
            return a;
        }
    }
}
