using Register.DataAccess;
using System;

namespace Register.Service
{
    public class Helper
    {
        public static string GetPGSQLConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("PostgreRegisterHost");
            var port = Environment.GetEnvironmentVariable("PostgreRegisterPort");
            var database = Environment.GetEnvironmentVariable("PostgreRegisterDB");
            var user = Environment.GetEnvironmentVariable("PostgreRegisterUser");
            var password = Environment.GetEnvironmentVariable("PostgreRegisterPassword");
            var timeout = Environment.GetEnvironmentVariable("PostgreRegisterTimeout");
            var pgsqlConnecton = $"Server={host};Port={port};Userid={user};Password={password};Timeout={timeout};Database={database}";
            return pgsqlConnecton;
        }

        
    }
}
