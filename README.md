# NET-Template

## Use Environment Variables as constant.
* Prebuild the **Infra.Constants** project to generate source code for environment variables as constant in PrebuiltVariables.cs file.  

*PATH: ..\Infra\Constants\Infra.Constants\PrebuiltVariables.cs* 

Add your environment files as harcoded in **PrebuiltVariables.tt** file like

   ```csharp
   var DEVEnvFile = @"C:\.NETProjects\NET-Template\Development.env";
   var UATEnvFile = @"C:\.NETProjects\NET-Template\Testing.env";
   var PRODEnvFile = @"C:\.NETProjects\NET-Template\Production.env";
   ```

## Decide Environment by selected configuration on Visual Studio like
* You can select desired configuration (**DEV**, **UAT**, **PROD**) and run/debug the project.

<p align="left">
  <img src="https://github.com/frkn2076/NET-Template/blob/main/resources/Configurations.PNG">
</p>

## Decide Environment on Docker
* Just add **environment file** parameter to your docker compose command like,
  - **docker-compose --env-file Testing.env up**
  - **docker-compose --env-file Production.env down** 

# WORK IN PROGRESS

* gRPC for communication between microservices
* Ocelot as a gateway
* JWT Authorization by Ocelot
* Rate limit is 30 requests/minute
* RabbitMQ for logging system
* Redis for distributed cache
* Mapster for object mapping
* T4 Template for source code generation


## Docker

* docker run --name local-redis -p 6379:6379 -d redis => Redis distrubuted cache
* docker run -it rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management  => RabbitMQ
* docker run --name mongodb -p 27017:27017 mongo => Mongo db
* docker run -p 5432:5432 --name some-postgres -e POSTGRES_PASSWORD=12345 -e POSTGRES_USER=root -d postgres => for postgre db, register service

## To Run 
* docker-compose --env-file Testing.env up
* docker-compose --env-file Development.env down 

## TO DO
* Fluent Validation
