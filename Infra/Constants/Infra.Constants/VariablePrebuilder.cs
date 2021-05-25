﻿namespace Infra.Constants
{
	public class PrebuiltVariables
	{
		
#if PROD 

         public const string MongoUser = "Frkn"; 
         public const string MongoPassword = "adv12f5gfba3sd3"; 
         public const string LogQueueHostName = "localhost"; 
         public const string LoggingQueue = "logging"; 
         public const string JwtSecretKey = "llgfusXQtnrxgKtWKpqEzLqAutpYWglI"; 
         public const string JwtExpireDurationMinutes = "60"; 
         public const string RedisCacheConnection = "localhost: 6379"; 
         

#elif UAT

         public const string MongoUser = "Frkn"; 
         public const string MongoPassword = "adv12f5gfba3sd3"; 
         public const string LogQueueHostName = "localhost"; 
         public const string LoggingQueue = "logging"; 
         public const string JwtSecretKey = "llgfusXQtnrxgKtWKpqEzLqAutpYWglI"; 
         public const string JwtExpireDurationMinutes = "60"; 
         public const string RedisCacheConnection = "localhost: 6379"; 
         
#else

         public const string EnvironmentFile = "Development.env"; 
         public const string AppName = "NET-Template"; 
         public const string MongoUser = "root"; 
         public const string MongoPassword = "12345"; 
         public const string MongoHost = "localhost"; 
         public const string MongoPort = "27017"; 
         public const string MongoTimeout = "5"; 
         public const string MongoLogDB = "LogDB"; 
         public const string RabbitMQHost = "localhost"; 
         public const string ReqResLoggingQueue = "reqreslogging"; 
         public const string ErrorLoggingQueue = "errorlogging"; 
         public const string JwtSecretKey = "llgfusXQtnrxgKtWKpqEzLqAutpYWglI"; 
         public const string JwtScheme = "TestKey"; 
         public const string JwtExpireDurationMinutes = "60"; 
         public const string RedisHost = "localhost:6379"; 
         public const string RedisPort = "6379"; 
         public const string RedisExpireDurationMinutes = "60"; 
         public const string PostgreUser = "root"; 
         public const string PostgrePassword = "12345"; 
         public const string PostgreHost = "registerdb"; 
         public const string PostgrePort = "5432"; 
         public const string PostgreTimeout = "5"; 
         public const string PostgreRegisterDB = "RegisterDB"; 
         public const string PostgreLocalizerDB = "LocalizerDB"; 
         public const string RegisterAPIPort = "5004"; 
         public const string SMTPFromMailAddress = "ozturkfurkan1994@gmail.com"; 
         public const string SMTPFromMailPassword = "*****"; 
         public const string SMTPHost = "smtp.gmail.com"; 
         public const string SMTPPort = "587"; 
         
#endif

    }
}