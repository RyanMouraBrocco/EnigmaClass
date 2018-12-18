using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EnigmaClass.CRUD
{
    public  static class Banco
    {
        public static  SqlConnection Abrir()
        {
            SqlConnection cn = new SqlConnection();
            cn.Open();
            return cn;
        }
    }
}
