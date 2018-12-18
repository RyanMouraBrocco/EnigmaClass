using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class AvaliacaoDAL
    {
        public AvaliacaoDAL()
        {
        }

        public void Inserir(Avaliacao A)
        {
            SqlCommand comm = new SqlCommand("",Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            if (A.Pergunta == null)
            {
                comm.CommandText = "InserirAvaliacaoResposta";
                comm.Parameters.Add("@Resposta", SqlDbType.Int).Value = A.Resposta.ID;

            }
            else
            {
                comm.CommandText = "InserirAvaliacaoPergunta";
                comm.Parameters.Add("@Pergunta", SqlDbType.Int).Value =A.Pergunta.ID;
            }
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = A.Usuario.ID;
            if (A.Denuncia == false)
            {
                comm.Parameters.Add("@Avaliacao", SqlDbType.Bit).Value = A._Avaliacao;
            }
            comm.Parameters.Add("@Denuncia", SqlDbType.Bit).Value = A.Denuncia;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Avaliacao A)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            if (A.Pergunta == null)
            {
                comm.CommandText = "AlterarAvaliacaoResposta";
                comm.Parameters.Add("@Resposta", SqlDbType.Int).Value = A.Resposta.ID;

            }
            else
            {
                comm.CommandText = "AlterarAvaliacaoPergunta";
                comm.Parameters.Add("@Pergunta", SqlDbType.Int).Value = A.Pergunta.ID;
            }
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = A.ID;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = A.Usuario.ID;
            if (A.Denuncia == false)
            {
                comm.Parameters.Add("@Avaliacao", SqlDbType.Bit).Value = A._Avaliacao;
            }
            comm.Parameters.Add("@Denuncia", SqlDbType.Bit).Value = A.Denuncia;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Avaliacao Consultar(int usuario, int pergunta, int resposta)
        {
            SqlCommand comm = new SqlCommand("",Banco.Abrir());
            if (pergunta > 0)
            {
                comm.CommandText = "Select * from AvaliacaoPergunta where ID_Usuario = "+usuario +" and ID_Pergunta = "+pergunta;
            }
            else
            {
                comm.CommandText = "Select * from AvaliacaoResposta where ID_Usuario = " + usuario + " and ID_Resposta = " + resposta;
            }
            SqlDataReader dr = comm.ExecuteReader();
            Avaliacao a = new Avaliacao();
            while (dr.Read())
            {
                a.ID = Convert.ToInt32(dr.GetValue(0));
                Usuario u = new Usuario();
                u.ID = Convert.ToInt32(dr.GetValue(1));
                a.Usuario = u;
                if (pergunta>0)
                {
                    Pergunta p = new Pergunta();
                    p.ID = Convert.ToInt32(dr.GetValue(2));
                    a.Pergunta = p;
                    a.Resposta = null;
                }
                else
                {
                    Resposta r = new Resposta();
                    r.ID = Convert.ToInt32(dr.GetValue(2));
                    a.Pergunta = null;
                    a.Resposta = r;
                }
                if (Convert.ToBoolean(dr.GetValue(4))==false)
                {
                    a._Avaliacao = Convert.ToBoolean(dr.GetValue(3));
                    a.Denuncia = false;
                }
                else
                {
                    a.Denuncia = true;
                }
            }
            comm.Connection.Close();
            return a;
        }

        public int AvaliacaoPossitiva(int pergunta, int resposta)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            if (pergunta > 0)
            {
                comm.CommandText = "Select sum(*) from AvaliacaoPergunta where ID_Pergunta = " + pergunta + " and Avaliacao_AvaliacaoPergunta = 1";
            }
            else
            {
                comm.CommandText = "Select sum(*) from AvaliacaoResposta where  ID_Resposta = " + resposta +" and Avaliacao_AvaliacaoResposta = 1";
            }
            int qtd = Convert.ToInt32(comm.ExecuteScalar());
            comm.Connection.Close();
            return qtd;
        }
        public int AvaliacaoNegativa(int pergunta, int resposta)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            if (pergunta > 0)
            {
                comm.CommandText = "Select sum(*) from AvaliacaoPergunta where ID_Pergunta = " + pergunta + " and Avaliacao_AvaliacaoPergunta = 0 and Denuncia_AvaliacaoPergunta = 0";
            }
            else
            {
                comm.CommandText = "Select sum(*) from AvaliacaoResposta where  ID_Resposta = " + resposta + " and Avaliacao_AvaliacaoResposta = 0 and Denuncia_AvaliacaoResposta = 0";
            }
            int qtd = Convert.ToInt32(comm.ExecuteScalar());
            comm.Connection.Close();
            return qtd;
        }
        public int Denuncias(int pergunta, int resposta)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            if (pergunta > 0)
            {
                comm.CommandText = "Select sum(*) from AvaliacaoPergunta where ID_Pergunta = " + pergunta + " and Denuncia_AvaliacaoPergunta = 1";
            }
            else
            {
                comm.CommandText = "Select sum(*) from AvaliacaoResposta where  ID_Resposta = " + resposta + " and Denuncia_AvaliacaoResposta = 1";
            }
            int qtd = Convert.ToInt32(comm.ExecuteScalar());
            comm.Connection.Close();
            return qtd;
        }
    }
}
