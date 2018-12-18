using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class MateriaDAL
    {
        public MateriaDAL()
        {
        }
        public void Inserir(Materia M)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirMateria";
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value =M.Nome;
            comm.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = M.Descricao;
            if (M.Imagem!=null)
            {

                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = M.Imagem;
            }
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = M.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Materia M)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarMateria";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = M.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = M.Nome;
            comm.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = M.Descricao;
            if (M.Imagem != null)
            {

                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = M.Imagem;
            }
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = M.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }

        public Materia Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Materia where ID_Materia = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Materia m = new Materia();
            while (dr.Read())
            {
                m = new Materia
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Nome = dr.GetValue(1).ToString(),
                    Descricao = dr.GetValue(2).ToString(),
                    Usuario = Convert.ToInt32(dr.GetValue(4))
                };
                if (dr.GetValue(3) != null)
                {
                    m.Imagem = dr.GetValue(3) as byte[];
                }
            }
            dr.Close();
            comm.CommandText = "Select * from Conteudo Where ID_Materia = "+id;
            List<Conteudo> lista = new List<Conteudo>();
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                Conteudo c = new Conteudo
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Materia = m,
                    Nome = dr.GetValue(2).ToString(),
                    Ordem = Convert.ToInt32(dr.GetValue(4)),
                    Usuario= Convert.ToInt32(dr.GetValue(5))
                };
                if (dr.GetValue(3) != null)
                {
                    c.Imagem = dr.GetValue(3) as byte[];
                }
                lista.Add(c);
            }
            m.Conteudo = lista;
            comm.Connection.Close();
            return m;
        }

    }
}
