using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class DesempenhoDAL
    {
        public DesempenhoDAL()
        {
        }
        public void Inserir(Desempenho D)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirDesempenho";
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = D.Usuario.ID;
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = D.Materia.ID;
            comm.Parameters.Add("@Porcentagem", SqlDbType.Decimal).Value = D.Porcentagem;
            comm.Parameters.Add("@HorasEstudadas", SqlDbType.Decimal).Value = D.HorasEstudadas;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Desempenho D)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarDesempenho";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = D.ID;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = D.Usuario.ID;
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = D.Materia.ID;
            comm.Parameters.Add("@Porcentagem", SqlDbType.Decimal).Value = D.Porcentagem;
            comm.Parameters.Add("@HorasEstudadas", SqlDbType.Decimal).Value = D.HorasEstudadas;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }

        public Desempenho Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Desempenho where ID_Desempenho = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Desempenho d = new Desempenho();
            while (dr.Read())
            {
                Usuario u = new Usuario();
                u.ID = Convert.ToInt32(dr.GetValue(1));
                Materia m = new Materia();
                m.ID = Convert.ToInt32(dr.GetValue(2));
                d = new Desempenho
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Usuario = u,
                    Materia = m,
                    Porcentagem = Convert.ToDecimal(dr.GetValue(3)),
                    HorasEstudadas = Convert.ToDecimal(dr.GetValue(4))
                };
            }
            comm.Connection.Close();
            return d;
        }
        public Desempenho Consultar(int materia,int usuario)
        {
            SqlCommand comm = new SqlCommand("Select * from Desempenho where ID_Materia = " + materia+" and ID_Usuario = "+ usuario, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Desempenho d = new Desempenho();
            while (dr.Read())
            {
                Usuario u = new Usuario();
                u.ID = Convert.ToInt32(dr.GetValue(1));
                Materia m = new Materia();
                m.ID = Convert.ToInt32(dr.GetValue(2));
                d = new Desempenho
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Usuario = u,
                    Materia = m,
                    Porcentagem = Convert.ToDecimal(dr.GetValue(3)),
                    HorasEstudadas = Convert.ToDecimal(dr.GetValue(4))
                };
            }
            comm.Connection.Close();
            return d;
        }
    }
}
