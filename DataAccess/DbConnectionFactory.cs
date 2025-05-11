using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbConnectionFactory
    {
        private static DbConnectionFactory _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;

        private DbConnectionFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MusicGawrEntities"].ConnectionString;
        }

        public static DbConnectionFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new DbConnectionFactory();
                    }
                }
                return _instance;
            }
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
