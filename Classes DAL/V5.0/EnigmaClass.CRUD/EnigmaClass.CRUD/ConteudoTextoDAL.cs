using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class ConteudoTextoDAL
    {
        public ConteudoTextoDAL()
        {
        }
        public void Inserir(ConteudoTexto C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirConteudoTexto";
            comm.Parameters.Add("@Conteudo", SqlDbType.Int).Value = C.Conteudo.ID;
            if (C.Texto != null)
            {
                comm.Parameters.Add("@Texto", SqlDbType.VarChar).Value = C.Texto.ID;
            }
            if (C.Video != null)
            {
                comm.Parameters.Add("@Video", SqlDbType.VarChar).Value = C.Video.ID;
            }
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarChar).Value = C.Imagem.ID;
            }
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(ConteudoTexto C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarConteudoTexto";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = C.ID;
            comm.Parameters.Add("@Conteudo", SqlDbType.Int).Value = C.Conteudo.ID;
            if (C.Texto != null)
            {
                comm.Parameters.Add("@Texto", SqlDbType.VarChar).Value = C.Texto.ID;
            }
            if (C.Video != null)
            {
                comm.Parameters.Add("@Video", SqlDbType.VarChar).Value = C.Video.ID;
            }
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarChar).Value = C.Imagem.ID;
            }
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }

        public ConteudoTexto Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from ConteudoTexto where ID_ConteudoTexto = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            ConteudoTexto c = new ConteudoTexto();
            while (dr.Read())
            {
                Texto t = null;
                if (dr.GetValue(2) != null)
                {
                    t = new Texto();
                    TextoDAL daltex = new TextoDAL();
                    t = daltex.Consultar(Convert.ToInt32(dr.GetValue(2)));
                    
                }
                Conteudo cc = null;
                if (dr.GetValue(1) != null)
                {
                    cc = new Conteudo();
                    ConteudoDAL dalcont = new ConteudoDAL();
                    cc = dalcont.Consultar(Convert.ToInt32(dr.GetValue(1)));
                }
                Imagem i = null;
                if (dr.GetValue(4) != null)
                {
                    i = new Imagem();
                    ImagemDAL dalimg = new ImagemDAL();
                    i = dalimg.Consultar(Convert.ToInt32(dr.GetValue(4)));
                }
                Video v = null;
                if (dr.GetValue(3) != null)
                {
                    v = new Video();
                    VideoDAL dalvid = new VideoDAL();
                    v = dalvid.Consultar(Convert.ToInt32(dr.GetValue(3)));
                }
                c = new ConteudoTexto
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Conteudo = cc,
                    Texto = t,
                    Imagem = i,
                    Video = v,
                    Ordem = Convert.ToInt32(dr.GetValue(5)),
                    Usuario = Convert.ToInt32(dr.GetValue(6))
                };
            }
            comm.Connection.Close();
            return c;
        }
    }
}
