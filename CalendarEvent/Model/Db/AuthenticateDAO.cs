using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;


namespace CalendarEvent.Model.Db
{
    class AuthenticateDAO : Dao
    {
        // Função de adicionar parametros ao SQL Para simplificação de querys.
        void AddParameter(DbCommand cmd, string name, object value)
        {
            if (value == null)
            {
                // Se o valor for nulo, atribui DbNull.
                value = DBNull.Value;
            }

            CreateParameter(cmd, name, value, null);
        }
        // Realiza a leitura dos dados para autenticação do login
        public int LoginAuthenticate(string Email, string Password)
        {
            int count = 0;
            using (DbConnection conn = CreateConnection())
            {
                string sql = "SELECT Email, Password FROM Cliente WHERE Email = @Email AND BINARY_CHECKSUM(Password) = BINARY_CHECKSUM(@Password)";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    AddParameter(cmd, "@Email", Email);
                    AddParameter(cmd, "@Password", Password);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            count++;
                        }
                        return count;
                    }
                }
            }
        }
    }
}
