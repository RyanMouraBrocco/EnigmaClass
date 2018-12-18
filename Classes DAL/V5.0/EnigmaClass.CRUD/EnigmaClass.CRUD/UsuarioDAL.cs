using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaClass;
using System.Data.SqlClient;
using System.Data;
namespace EnigmaClass.CRUD
{
    public class UsuarioDAL
    {
        public UsuarioDAL()
        {
        }

        public void Inserir(Usuario U)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "InserirUsuario";
            comm.Parameters.Add("@Nome",SqlDbType.VarChar).Value = U.Nome;
            comm.Parameters.Add("@Email", SqlDbType.VarChar).Value = U.Email;
            comm.Parameters.Add("@Senha", SqlDbType.VarChar).Value = Criptografia.GerarMD5(U.Senha);
            comm.Parameters.Add("@TipoConta", SqlDbType.Char).Value = U.TipoConta;
            if (U.Foto != null)
            {
                comm.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = U.Foto;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Usuario U)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "AlterarUsuario";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = U.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = U.Nome;
            comm.Parameters.Add("@Email", SqlDbType.VarChar).Value = U.Email;
            comm.Parameters.Add("@Senha", SqlDbType.VarChar).Value = Criptografia.GerarMD5(U.Senha);
            comm.Parameters.Add("@TipoConta", SqlDbType.Char).Value = U.TipoConta;
            if (U.Foto != null)
            {
                comm.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = U.Foto;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public bool Logar(string login , string senha)
        {
            SqlCommand comm = new SqlCommand("Select Email_Usuario, Senha_Usuario from Usuario where Email_Usuario = '"+login+"'",Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            string _login = "";
            string _senha = "";
            while (dr.Read())
            {
                _login = dr.GetValue(0).ToString();
                _senha = dr.GetValue(1).ToString();
            }
            if (_login == "")
            {
                return false;
            }
            else
            {
                if (_login == login && _senha == senha)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        public Usuario Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Usuario where ID_Usuario = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Usuario u = new Usuario();
            while (dr.Read())
            {
                u = new Usuario
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Nome = dr.GetValue(1).ToString(),
                    Email = dr.GetValue(2).ToString(),
                    Senha = null,
                    TipoConta = dr.GetValue(4).ToString()
                 
                };
                if (dr.GetValue(5) != null)
                {
                    u.Foto = dr.GetValue(5) as byte[];
                }
            }
            return u;
        }
        public Usuario Consultar(string login)
        {
            SqlCommand comm = new SqlCommand("Select * from Usuario where Email_Usuario = '" + login +"'", Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Usuario u = new Usuario();
            while (dr.Read())
            {
                u = new Usuario
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Nome = dr.GetValue(1).ToString(),
                    Email = dr.GetValue(2).ToString(),
                    Senha = null,
                    TipoConta = dr.GetValue(4).ToString()

                };
                if (dr.GetValue(5) != null)
                {
                    u.Foto = dr.GetValue(5) as byte[];
                }
            }
            return u;
        }
    }
}
