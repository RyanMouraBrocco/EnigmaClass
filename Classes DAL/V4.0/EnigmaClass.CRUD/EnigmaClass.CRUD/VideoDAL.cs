using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class VideoDAL
    {
        public VideoDAL()
        {
        }
        public void Inserir(Video V)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirVideo";
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = V.Nome;
            comm.Parameters.Add("@Link", SqlDbType.VarChar).Value = V.Link;
            comm.Parameters.Add("@Duracao", SqlDbType.Decimal).Value = V.Duracao;
            comm.Parameters.Add("@InicioConteudo", SqlDbType.Decimal).Value = V.Inicio;
            comm.Parameters.Add("@FimConteudo", SqlDbType.Decimal).Value = V.Fim;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = V.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Video V)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarVideo";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = V.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = V.Nome;
            comm.Parameters.Add("@Link", SqlDbType.VarChar).Value = V.Link;
            comm.Parameters.Add("@Duracao", SqlDbType.Decimal).Value = V.Duracao;
            comm.Parameters.Add("@InicioConteudo", SqlDbType.Decimal).Value = V.Inicio;
            comm.Parameters.Add("@FimConteudo", SqlDbType.Decimal).Value = V.Fim;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = V.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Deletar(int id)
        {
            SqlCommand comm = new SqlCommand("Delete Video where ID_Video = " + id, Banco.Abrir());
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Video Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Video Where ID_Video = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Video v = new Video();
            while (dr.Read())
            {
                v.ID = Convert.ToInt32(dr.GetValue(0));
                v.Nome = dr.GetValue(1).ToString();
                v.Link = dr.GetValue(2).ToString();
                v.Duracao = Convert.ToDecimal(dr.GetValue(3));
                v.Inicio = Convert.ToDecimal(dr.GetValue(4));
                v.Fim = Convert.ToDecimal(dr.GetValue(5));
                v.Usuario = Convert.ToInt32(dr.GetValue(6));
            }
            comm.Connection.Close();
            return v;
        }
    }
}
