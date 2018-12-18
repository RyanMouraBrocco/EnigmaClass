using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class ConteudoDAL
    {
        public ConteudoDAL()
        {
        }
        /// <summary>
        /// Insere um Conteudo na tabela conteudo no Banco de dados 
        /// Com Imagem Podendo ser nula
        /// </summary>
        /// <param name="C"> Parametro do tipo Conteudo| sem id </param>
        public void Inserir(Conteudo C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirConteudo";
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = C.Materia.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = C.Nome;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = C.Imagem;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        /// <summary>
        /// Altera um Conteudo na tabela conteudo no Banco de dados 
        /// Com Imagem Podendo ser nula
        /// </summary>
        /// <param name="C"> Parametro do tipo Conteudo| com id </param>
        public void Alterar(Conteudo C)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AlterarConteudo";
            comm.Parameters.Add("@Materia", SqlDbType.Int).Value = C.Materia.ID;
            comm.Parameters.Add("@Nome", SqlDbType.VarChar).Value = C.Nome;
            comm.Parameters.Add("@Ordem", SqlDbType.Int).Value = C.Ordem;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = C.Usuario;
            if (C.Imagem != null)
            {
                comm.Parameters.Add("@Imagem", SqlDbType.VarBinary).Value = C.Imagem;
            }
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        /// <summary>
        /// retorna um objeto do tipo Conteudo 
        /// Contendo so o ID Da Materia 
        /// E um List de  ConteudoTexto (completo) já organizado em ordem crescente
        /// </summary>
        /// <param name="id"> parametro inteiro do id do conteudo</param>
        /// <returns></returns>
        public Conteudo Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Conteudo where ID_Conteudo = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Conteudo c = new Conteudo();
            while (dr.Read())
            {
                Materia m = new Materia();
                m.ID = Convert.ToInt32(dr.GetValue(1));
                c = new Conteudo
                {
                    ID = Convert.ToInt32(dr.GetValue(0)),
                    Materia = m,
                    Nome = dr.GetValue(2).ToString(),
                    Ordem = Convert.ToInt32(dr.GetValue(4)),
                    Usuario = Convert.ToInt32(dr.GetValue(5))
                };
                if (dr.GetValue(3) != null)
                {
                    c.Imagem = dr.GetValue(3) as byte[];
                }
            }
            dr.Close();
            comm.CommandText = "Select ID_ConteudoTexto,Ordem_ConteudoTexto from ConteudoTexto where ID_Conteudo = "+id+" order by Ordem_ConteudoTexto";
            dr = comm.ExecuteReader();
            List<ConteudoTexto> listacont = new List<ConteudoTexto>();
            while (dr.Read())
            {
                ConteudoTextoDAL dalcontext = new ConteudoTextoDAL();
                ConteudoTexto ct = new ConteudoTexto();
                ct = dalcontext.Consultar(Convert.ToInt32(dr.GetValue(0)));
                listacont.Add(ct);
            }
            c.ConteudoTexto = listacont;
            comm.Connection.Close();
            dr.Close();
            comm.CommandText = "Select ID_Resumo from Resumo where ID_Conteudo = " + id ;
            dr = comm.ExecuteReader();
            List<Resumo> listaresumo  = new List<Resumo>();
            while (dr.Read())
            {
                ResumoDAL dalresu = new ResumoDAL();
                Resumo r = new Resumo();
                r = dalresu.Consultar(Convert.ToInt32(dr.GetValue(0)));
                listaresumo.Add(r);
            }
            c.Resumo = listaresumo;
            comm.Connection.Close();
            return c;
        }
    }
}
