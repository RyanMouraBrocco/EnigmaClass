using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class PerguntaDAL
    {
        public PerguntaDAL()
        {
        }
        public void Inserir(Pergunta P)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirPergunta";
            comm.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = P.Titulo;
            comm.Parameters.Add("@Texto", SqlDbType.Text).Value = P.Texto;
            comm.Parameters.Add("@Visibilidade", SqlDbType.Bit).Value = P.Visibilidade;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = P.Usuario;
            comm.ExecuteNonQuery();
            comm.CommandText = "InserirImagemPergunta";
            int ordem = 1;
            foreach (var item in P.Imagem)
            {
                comm.Parameters.Add("@Imagem",SqlDbType.Int).Value = item.ID;
                comm.Parameters.Add("@Pergunta", SqlDbType.Int).Value = P.ID;
                comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = ordem;
                comm.Parameters.Add("@Usaurio", SqlDbType.Int).Value = P.Usuario;
                ordem += 1;
                comm.ExecuteNonQuery();
            }
            comm.Connection.Close();
        }
        public void Alterar(Pergunta P)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarPergunta";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = P.ID;
            comm.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = P.Titulo;
            comm.Parameters.Add("@Texto", SqlDbType.Text).Value = P.Texto;
            comm.Parameters.Add("@Visibilidade", SqlDbType.Bit).Value = P.Visibilidade;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = P.Usuario;
            comm.ExecuteNonQuery();
            comm.CommandType = CommandType.Text;
            comm.CommandText = "Delete ImagemPergunta Where ID_Pergunta = "+P.ID;
            comm.ExecuteNonQuery();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "InserirImagemPergunta";
            int ordem = 1;
            foreach (var item in P.Imagem)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.Int).Value = item.ID;
                comm.Parameters.Add("@Pergunta", SqlDbType.Int).Value = P.ID;
                comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = ordem;
                comm.Parameters.Add("@Usaurio", SqlDbType.Int).Value = P.Usuario;
                ordem += 1;
                comm.ExecuteNonQuery();
            }
            comm.Connection.Close();
        }
        public Pergunta Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Pergunta p = new Pergunta();
            while (dr.Read())
            {
                p = new Pergunta
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Titulo = dr.GetValue(1).ToString(),
                    Texto = dr.GetValue(2).ToString(),
                    Visibilidade = Convert.ToBoolean(dr.GetValue(3)),
                    Usuario = Convert.ToInt32(dr.GetValue(4))
                };
            }
            dr.Close();
            comm.CommandText = @"Select i.ID_Imagem,ip.Ordem_ImagemPergunta 
                                from Imagem i inner join ImagemPergunta ip 
                                on i.ID_Imagem = ip.ID_Imagem 
                                Where ip.ID_Pergunta = "+id +" order by ip.Ordem_ImagemPergunta";
            dr = comm.ExecuteReader();
            List<Imagem> listaimg = new List<Imagem>();
            while (dr.Read())
            {
                ImagemDAL dal = new ImagemDAL();
                Imagem im = new Imagem();
                im = dal.Consultar(Convert.ToInt32(dr.GetValue(0)));
                listaimg.Add(im);
            }
            p.Imagem = listaimg;
            dr.Close();
            comm.CommandText = "Select ID_Resposta from Resposta Where ID_Pergunta = "+id;
            dr = comm.ExecuteReader();
            List<Resposta> listresposta = new List<Resposta>();
            while (dr.Read())
            {
                Resposta r = new Resposta();
                RespostaDAL dal = new RespostaDAL();
                r = dal.Consultar(Convert.ToInt32(dr.GetValue(0)));
                listresposta.Add(r);
            }
            p.Resposta = listresposta;
            comm.Connection.Close();
            return p;
        }
    }
}
