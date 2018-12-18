using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class ImagemDAL
    {
        public void Inserir(Imagem I)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirImagem";
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = I.Nome;
            comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = I._Imagem;
            comm.Parameters.Add("@Extensao", SqlDbType.Char).Value = I.Extensao;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = I.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Imagem I)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarImagem";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = I.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = I.Nome;
            comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = I._Imagem;
            comm.Parameters.Add("@Extensao", SqlDbType.Char).Value = I.Extensao;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = I.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Deletar(int id)
        {
            SqlCommand comm = new SqlCommand("Delete Imagem where ID_Imagem = " + id, Banco.Abrir());
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Imagem Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Imagem Where ID_Imagem = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Imagem I = new Imagem();
            while (dr.Read())
            {
                I.ID = Convert.ToInt32(dr.GetValue(0));
                I.Nome = dr.GetValue(1).ToString();
                I._Imagem= dr.GetValue(2) as byte[];
                I.Extensao = dr.GetValue(3).ToString();
                I.Usuario = Convert.ToInt32(dr.GetValue(4));
            }
            comm.Connection.Close();
            return I;
        }
    }
}
