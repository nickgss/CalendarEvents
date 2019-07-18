using CalendarEvent.Utils;
using Db;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEvent.Model.Db
{
    class EventoDao : Dao
    {
        // Listagem de Eventos
        public List<Evento> ListarEventos(string padrao)
        {
            using (DbConnection conn = CreateConnection())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT Id, Nome, DataInicio, DataFinal, Descricao FROM Evento ");

                // Caso haja um padrão na barra de pesquisa ele faz a busca pelo nome digitado
                if (!string.IsNullOrWhiteSpace(padrao))
                {
                    sql.Append("WHERE Nome LIKE @Padrao ");
                }

                // Caso nao haja um nome ele traz todos os registros e os ordena pelo nome
                sql.Append("ORDER BY Nome");

                using (DbCommand cmd = CreateCommand(conn, sql.ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(padrao))
                    {
                        AddParameter(cmd, "@Padrao", "%" + padrao + "%");
                    }

                    List<Evento> eventos = new List<Evento>();

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Evento e = new Evento();
                            ExtrairDados(dr, e);
                            eventos.Add(e);
                        }

                        return eventos;
                    }
                }
            }
        }

        // Obtém um evento com base no seu ID. 
        public void LerEvento(int id, Evento evento)
        {
            using (DbConnection conn = CreateConnection())
            {
                string sql = "SELECT Id, Nome, DataInicio, DataFinal, Descricao FROM Evento WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    AddParameter(cmd, "@Id", id);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            return;
                        }

                        ExtrairDados(dr, evento);
                    }
                }
            }
        }

        // Insere um novo Evento no banco de dados.
        public void Inserir(Evento evento)
        {
            using (DbConnection conn = CreateConnection())
            {
                // Usa uma transação para fazer as duas inserções de forma atômica. coloquei o transaction por necessidades futuras de realizar um sistema
                // onde ele iria atribuir os eventos para mult-usuarios, onde cada evento teria seu usuario.
                DbTransaction transaction = conn.BeginTransaction();

                string sql = "INSERT INTO Evento (Id, Nome, DataInicio, DataFinal, Descricao) VALUES (@Id, @Nome, @DataInicio, @DataFinal, @Descricao)";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;

                    evento.Id = LerMaiorId() + 1;

                    AddParameter(cmd, "@Id", evento.Id);
                    AddParameter(cmd, "@Nome", evento.Nome);
                    AddParameter(cmd, "@DataInicio", evento.DataInicio);
                    AddParameter(cmd, "@DataFinal", evento.DataFinal);
                    AddParameter(cmd, "@Descricao", evento.Descricao);

                    cmd.ExecuteNonQuery();
                }


                transaction.Commit();
            }
        }

        // Altera os dados de um evento
        public void Alterar(Evento evento)
        {
            using (DbConnection conn = CreateConnection())
            {
                // Usa uma transação para fazer as duas alterações de forma atômica.
                DbTransaction transaction = conn.BeginTransaction();

                string sql = "UPDATE Evento SET Nome = @Nome, DataInicio = @DataInicio, DataFinal = @DataFinal, Descricao = @Descricao WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;

                    AddParameter(cmd, "@Nome", evento.Nome);
                    AddParameter(cmd, "@DataInicio", evento.DataInicio);
                    AddParameter(cmd, "@DataFinal", evento.DataFinal);
                    AddParameter(cmd, "@Descricao", evento.Descricao);
                    AddParameter(cmd, "@Id", evento.Id);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        // Adiciona um parâmetro À query.
        void AddParameter(DbCommand cmd, string name, object value)
        {
            if (value == null)
            {
                // Se o valor for nulo, atribui DbNull.
                value = DBNull.Value;
            }

            CreateParameter(cmd, name, value, null);
        }

        // Exclui um evento
        public void Excluir(int id)
        {
            using (DbConnection conn = CreateConnection())
            {

                DbTransaction transaction = conn.BeginTransaction();

                string sql = "DELETE FROM Evento WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;
                    AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        // Obtém o maior ID cadastrado de um evento, porem não há necessidade de controlar o id deixei apenas para controle da aplicação
        // isso pode ser resolvido facil pelo SGDB apenas colocar o atributo "IDENTITY(1,1)" no campo do id que ele atribui de forma automatica.
        public int LerMaiorId()
        {
            using (DbConnection conn = CreateConnection())
            {
                string sql = "SELECT MAX(Id) FROM Evento";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    object obj = cmd.ExecuteScalar();

                    if (obj == System.DBNull.Value)
                    {
                        return 0;
                    }

                    return (int)obj;
                }
            }
        }

        // Extrai dados de um DbDataReader e os coloca no objeto Evento fornecido.
        private void ExtrairDados(DbDataReader dr, Evento evento)
        {
            evento.Id = (int)dr["Id"];
            evento.Nome = (string)dr["Nome"];
            evento.DataInicio = (string)dr["DataInicio"];
            evento.DataFinal = (string)dr["DataFinal"];
            evento.Descricao = (string)dr["Descricao"];


        }
    }
}
