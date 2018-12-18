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
        /// <summary>
        /// Retorna um objeto SqlConnection  com sua conexão ao banco de dados.
        /// </summary>
        /// <returns> Retorna um Sqlconnection Com a conexão já Aberta </returns>
        public static  SqlConnection Abrir()
        {
            SqlConnection cn = new SqlConnection(@"Server=5c37e073-a92f-4ca6-a853-a8810009884b.sqlserver.sequelizer.com;Database=db5c37e073a92f4ca6a853a8810009884b;User ID=dfjaozkaqpvneyhg;Password=tKAuy4HAHwTLguvfHqNbj8gfMxk3m5q68uXyzrzkL5ymmU3soS54rpguDuQbSNpM;");
            cn.Open();
            return cn;
        }
    }
}
