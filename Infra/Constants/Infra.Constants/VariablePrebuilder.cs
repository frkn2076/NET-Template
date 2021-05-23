namespace Infra.Constants
{
	public class PrebuiltVariables
	{
		 public const string EnvironmentFile = "Development.env"; 
         public const string AppName = "NET-Template"; 
         public const string MongoLogUser = "root"; 
         public const string MongoLogPassword = "12345"; 
         public const string MongoLogHost = "localhost"; 
         public const string MongoLogPort = "27017"; 
         public const string MongoLogDB = "LogDB"; 
         public const string MongoLogTimeout = "5"; 
         public const string LogQueueHostName = "localhost"; 
         public const string ReqResLoggingQueue = "reqreslogging"; 
         public const string ErrorLoggingQueue = "errorlogging"; 
         public const string JwtSecretKey = "llgfusXQtnrxgKtWKpqEzLqAutpYWglI"; 
         public const string JwtExpireDurationMinutes = "60"; 
         public const string RedisCacheConnection = "localhost: 6379"; 
         public const string PostgreRegisterDB = "RegisterDB"; 
         public const string PostgreLocalizerDB = "LocalizerDB"; 
         public const string PostgreRegisterUser = "root"; 
         public const string PostgreRegisterPassword = "12345"; 
         public const string PostgreRegisterHost = "registerdb"; 
         public const string PostgreRegisterPort = "5432"; 
         public const string PostgreRegisterTimeout = "5"; 
         public const string RegisterAPIPort = "5004"; 
         
    }
}