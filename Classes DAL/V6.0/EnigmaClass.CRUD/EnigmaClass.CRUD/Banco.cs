﻿using System;
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
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-B4MGRSR\SQLEXPRESS;Initial Catalog=EnigmaDB;Integrated Security=True");
            cn.Open();
            return cn;
        }
    }
}
