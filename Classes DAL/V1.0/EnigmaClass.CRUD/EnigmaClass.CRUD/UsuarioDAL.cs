using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaClass;
using System.Data.SqlClient;

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
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirUsuario";
        }
    }
}
