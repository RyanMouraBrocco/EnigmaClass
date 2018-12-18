using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class ConteudoDAL
    {
        public ConteudoDAL()
        {
        }
        public void Inserir(Conteudo C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirConteudo";
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = C.Materia.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = C.Nome;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = C.Imagem;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Conteudo C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarConteudo";
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = C.Materia.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = C.Nome;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = C.Imagem;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }

        public Conteudo Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Conteudo where ID_Conteudo = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Conteudo c = new Conteudo();
            while (dr.Read())
            {
                Materia m = new Materia();
                m.ID = Convert.ToInt32(dr.GetValue(1));
                c = new Conteudo
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Materia = m,
                    Nome = dr.GetValue(2).ToString(),
                    Ordem = Convert.ToInt32(dr.GetValue(4)),
                    Usuario = Convert.ToInt32(dr.GetValue(5))
                };
                if (dr.GetValue(3) != null)
                {
                    c.Imagem = dr.GetValue(3) as byte[];
                }
            }
            dr.Close();
            comm.CommandText = "Select ID_ConteudoTexto,Ordem_ConteudoTexto from ConteudoTexto where ID_Conteudo = "+id+" order by Ordem_ConteudoTexto";
            dr = comm.ExecuteReader();
            List<ConteudoTexto> listacont = new List<ConteudoTexto>();
            while (dr.Read())
            {
                ConteudoTextoDAL dalcontext = new ConteudoTextoDAL();
                ConteudoTexto ct = new ConteudoTexto();
                ct = dalcontext.Consultar(Convert.ToInt32(dr.GetValue(0)));
                listacont.Add(ct);
            }
            comm.Connection.Close();
            return c;
        }
    }
}
