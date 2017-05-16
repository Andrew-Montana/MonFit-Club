using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace MonFit_Club
{
    class DataBase
    {
        private static string server = "localhost";
        private static string port = "5432";
        private static string userId = "postgres";
        private static string password = "123";
        private static string database = "ovdejchuk_FitnessClub";
        private static string connect_params = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", server, port, userId, password, database);
    
        public static NpgsqlConnection connect = new NpgsqlConnection() { ConnectionString = connect_params } ;
    }
}
