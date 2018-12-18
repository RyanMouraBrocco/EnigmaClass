﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class NotaDAL
    {
        public NotaDAL()
        {
        }

        public void Inserir(Nota N)
        {
            SqlCommand comm = new SqlCommand("",Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "InserirNota";
            comm.Parameters.Add("@Usuario",SqlDbType.Int).Value =N.Usuario;
            comm.Parameters.Add("@Exercicio",SqlDbType.Int).Value =N.Exercicio;
            comm.Parameters.Add("@Nota",SqlDbType.Decimal).Value =N._Nota;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Nota N)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "AlterarNota";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = N.ID;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = N.Usuario;
            comm.Parameters.Add("@Exercicio", SqlDbType.Int).Value = N.Exercicio;
            comm.Parameters.Add("@Nota", SqlDbType.Decimal).Value = N._Nota;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Nota Consultar(int usuario, int exercicio)
        {
            SqlCommand comm = new SqlCommand("Select * from Nota where ID_Usuario = "+usuario+" and ID_Exercicio = "+exercicio,Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Nota n = new Nota();
            while (dr.Read())
            {
                Exercicio e = new Exercicio
                {
                    ID= exercicio
                };
                Usuario u = new Usuario
                {
                    ID = usuario
                };
                n = new Nota
                {
                    Usuario = u,
                    Exercicio = e,
                    _Nota = Convert.ToDecimal(dr.GetValue(2)),
                    ID = Convert.ToInt32(dr.GetValue(0))
                };

            }
            comm.Connection.Close();
            return n;
        }
    }
}
