# NET-Template (Work-in-Progress)

## Use Environment Variables as constant.
* Prebuild the **Infra.Constants** project to generate source code for environment variables as constant in PrebuiltVariables.cs file.  

*PATH: ..\Infra\Constants\Infra.Constants\PrebuiltVariables.cs* 

Add your environment files as harcoded in **PrebuiltVariables.tt** file like

   ```csharp
   var DEVEnvFile = @"C:\.NETProjects\NET-Template\Development.env";
   var UATEnvFile = @"C:\.NETProjects\NET-Template\Testing.env";
   var PRODEnvFile = @"C:\.NETProjects\NET-Template\Production.env";
   ```
<br>

## Decide Environment by selected configuration on Visual Studio like
* You can select desired configuration (**DEV**, **UAT**, **PROD**) and run/debug the project.

<p align="left">
  <img src="https://github.com/frkn2076/NET-Template/blob/main/resources/Configurations.PNG">
</p>

<br>


## Decide Environment on Docker
* Just add **environment file** parameter to your docker compose command like,
  - **docker-compose --env-file Testing.env up**
  - **docker-compose --env-file Production.env down** 

<br>

## You may have the external dependencies (db, rabbitmq, distibuted systems) on docker one by one for local development. 
* docker run --name redis-instance -p 6379:6379 -d redis => **Redis**
* docker run --name rabbitmq-instance -p 5672:5672 -p 15672:15672 -d rabbitmq:3-management-alpine  => **RabbitMQ**
* docker run --name mongo-instance -p 27017:27017 -e MONGO_USERNAME=root -e MONGO_USERNAME=12345 -d mongo => **Mongo Database**
* docker run --name postgre-instance -p 5432:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=12345 -d postgres:13-alpine => **PostgreSQL Database**

<br>

## Technologies
* gRPC
* Ocelot
* JWT Authorization
* RabbitMQ
* Redis
* Mapster
* T4 Templates

## Features
* Rate limit is 30 requests/minute


## TO DO
* Fluent Validation
* GraphQL
* Elastic Stack(Elastic Search and Kibana Monioring) or Graylog Monitoring
* Execution time thresholds
* Deploy pipeline CI/CD (Teamcity and Octopus) 
