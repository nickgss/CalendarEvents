/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//     Framework de Conxão com Banco de dados DAO
//          Definition File
//
//          Version 1.0.1
//
//                          by: "G"
//                      Created: February 11, 2017
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace Db
{
    public abstract class Dao
    {
        /// <summary>
        /// Variaveis necessárias para cricação de conexão com o banco usando Factories
        /// </summary>
        static DbProviderFactory providerFactory;
        static string connStr;
        static bool initialized;

        /// <summary>
        /// Contrutor Init com parametros
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="connString"></param>
        public static void Init(string providerName, string connString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connStr = connString;
            initialized = true;
        }

        /// <summary>
        /// Caso nao defina parametros o construtor padrão assume a seguinte funcção manual!
        /// </summary>
        public static void Init()
        {
            Init(ConfigurationManager.AppSettings["dbProvider"], ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString);
        }
        /// <summary>
        /// Cria a Conexão
        /// </summary>
        /// <returns></returns>
        protected DbConnection CreateConnection()
        {
            // Se nao foi iniciada chama o Init
            if (!initialized)
            {
                Init();
            }
            DbConnection conn = providerFactory.CreateConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Cria um Comando
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected DbCommand CreateCommand(DbConnection conn, string text)
        {
            DbCommand cmd = providerFactory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = text;
            return cmd;
        }

        /// <summary>
        /// Cria parametros ás query
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DbParameter CreateParameter(DbCommand cmd, string name, object value, DbType? type)
        {
            DbParameter param = providerFactory.CreateParameter();
            param.ParameterName = name;
            param.Value = value;

            if (type.HasValue)
            {
                param.DbType = type.Value;
            }

            cmd.Parameters.Add(param);

            return param;

        }




    }
}
