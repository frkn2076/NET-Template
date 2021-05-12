using System;

namespace Infra.Helper
{
    public class Helper
    {
        public static string GetPostgreDatabaseConnection(string database)
        {
            var host = Environment.GetEnvironmentVariable("PostgreRegisterHost");
            var port = Environment.GetEnvironmentVariable("PostgreRegisterPort");
            var user = Environment.GetEnvironmentVariable("PostgreRegisterUser");
            var password = Environment.GetEnvironmentVariable("PostgreRegisterPassword");
            var timeout = Environment.GetEnvironmentVariable("PostgreRegisterTimeout");
            var pgsqlConnecton = $"Server={host};Port={port};Userid={user};Password={password};Timeout={timeout};Database={database}";
            return pgsqlConnecton;
        }
    }
}
