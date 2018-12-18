using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EnigmaClass.CRUD
{
    public class ExercicioDAL
    {
        public ExercicioDAL()
        {
        }
        public void Criar(Exercicio E)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirExercicio";
            comm.Parameters.Add("@Conteudo", SqlDbType.Int).Value = E.Conteudo.ID;
            comm.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = E.Descricao;
            comm.Parameters.Add("@Tipo", SqlDbType.Char).Value = E.Tipo;
            comm.Parameters.Add("@AleatorioQuestao", SqlDbType.Bit).Value = E.AleatorioQuestao;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = E.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public void Alterar(Exercicio E)
        {
            SqlCommand comm = new SqlCommand("", Banco.Abrir());
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "InserirExercicio";
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = E.ID;
            comm.Parameters.Add("@Conteudo", SqlDbType.Int).Value = E.Conteudo.ID;
            comm.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = E.Descricao;
            comm.Parameters.Add("@Tipo", SqlDbType.Char).Value = E.Tipo;
            comm.Parameters.Add("@AleatorioQuestao", SqlDbType.Bit).Value = E.AleatorioQuestao;
            comm.Parameters.Add("@Usuario", SqlDbType.Int).Value = E.Usuario;
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        public Exercicio Consultar(int id)
        {
            SqlCommand comm = new SqlCommand("Select * from Exercicio Where ID_Exercicio = " + id, Banco.Abrir());
            SqlDataReader dr = comm.ExecuteReader();
            Exercicio e = new Exercicio();
            while (dr.Read())
            {
                e.ID = Convert.ToInt32(dr.GetValue(0));
                Conteudo cont = new Conteudo();
                cont.ID = Convert.ToInt32(dr.GetValue(1));
                e.Conteudo = cont;
                e.Descricao = dr.GetValue(2).ToString();
                e.Tipo = dr.GetValue(3).ToString();
                e.AleatorioQuestao = Convert.ToBoolean(dr.GetValue(4));
                e.Usuario = Convert.ToInt32(dr.GetValue(5));
            }
            dr.Close();
            comm.CommandText = "Select ID_Questao, Ordem_Questao from Questao Where ID_Exercicio = " + id + " order by Ordem_Questao";
            dr = comm.ExecuteReader();
            List<Questao> lista = new List<Questao>();
            while (dr.Read())
            {
                QuestaoDAL dalq = new QuestaoDAL();
                Questao q = new Questao();
                q = dalq.Consultar(Convert.ToInt32(dr.GetValue(0)));
                lista.Add(q);
            }
            e.Questao = lista;
            comm.Connection.Close();
            return e;
        }
        public Nota Corrigir(Exercicio Realizado,Exercicio Gabarito)
        {
            decimal TotalQuestoes = Gabarito.Questao.Count;
            decimal acerto = 0;
            if (Gabarito.Tipo =="C")
            {
                foreach (var itemGabaritoQ in Gabarito.Questao)
                {
                    foreach (var itemRealizadoQ in Realizado.Questao)
                    {
                        if (itemGabaritoQ.Ordem == itemRealizadoQ.Ordem)
                        {
                            foreach (var itemGabaritoA in itemGabaritoQ.Alternativa)
                            {
                                foreach (var itemRealizadoA in itemRealizadoQ.Alternativa)
                                {
                                    if (itemRealizadoA.Conteudo == itemGabaritoA.Conteudo)
                                    {
                                        acerto += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var itemGabaritoQ in Gabarito.Questao)
                {
                    foreach (var itemRealizadoQ in Realizado.Questao)
                    {
                        if (itemGabaritoQ.Ordem == itemRealizadoQ.Ordem)
                        {
                            foreach (var itemGabaritoA in itemGabaritoQ.Alternativa)
                            {
                                foreach (var itemRealizadoA in itemRealizadoQ.Alternativa)
                                {
                                    if (itemRealizadoA.Ordem == itemGabaritoA.Ordem)
                                    {
                                        if (itemRealizadoA.Tipo == "R")
                                        {
                                            if (itemGabaritoA.Tipo=="C")
                                            {
                                                if (itemRealizadoA.Conteudo == itemGabaritoA.Conteudo)
                                                {
                                                    acerto += 1;
                                                }
                                            }
                                        }
                                       
                                    }
                                }
                            }
                        }
                    }
                }
            }
            decimal pontoquestao = 10 / TotalQuestoes;
            decimal nota = pontoquestao * acerto;
            Nota n = new Nota
            {
                _Nota = nota,
                Exercicio = Realizado
            };
            Usuario u = new Usuario();
            UsuarioDAL dal = new UsuarioDAL();
            u = dal.Consultar(Realizado.Usuario);
            n.Usuario = u;
            return n;
        }
    }
}
