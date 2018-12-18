using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EnigmaClass.CRUD
{
    public  static  class Criptografia
    {
        public  static string GerarMD5(string texto)
        {
            MD5 md = MD5.Create();
            byte[] text = md.ComputeHash(Encoding.Default.GetBytes(texto));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(text[i].ToString("x2"));
            }
            return sb.ToString();
        }
        
    }
}
