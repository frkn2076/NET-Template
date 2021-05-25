﻿namespace Infra.Constants
{
	public class PrebuiltVariables
	{
		
#if PROD 

         public const string ENVIRONMENT = "PROD"; 
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
         public const string RedisHost = "localhost"; 
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
         

#elif UAT

         public const string ENVIRONMENT = "UAT"; 
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
         public const string RedisHost = "localhost"; 
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
         
#else

         public const string ENVIRONMENT = "DEV"; 
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
         public const string RedisHost = "localhost"; 
         public const string RedisPort = "6379"; 
         public const string RedisExpireDurationMinutes = "60"; 
         public const string PostgreUser = "root"; 
         public const string PostgrePassword = "12345"; 
         public const string PostgreHost = "registerdb"; 
         public const string PostgrePort = "5432"; 
         public const string PostgreTimeout = "5"; 
         public const string PostgreRegisterDB = "RegisterDB"; 
         public const string PostgreLocalizerDB = "LocalizerDB"; 
         public const string GatewayAPIPort = "5000"; 
         public const string RegisterAPIPort = "5004"; 
         public const string SMTPFromMailAddress = "ozturkfurkan1994@gmail.com"; 
         public const string SMTPFromMailPassword = "*****"; 
         public const string SMTPHost = "smtp.gmail.com"; 
         public const string SMTPPort = "587"; 
         
#endif

    }
}