# NET-Template

1 - Prebuild the project to generate source code for environment variables as constant in PrebuiltVariables.cs file.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**PATH: ..\Infra\Constants\Infra.Constants\PrebuiltVariables.cs** 

2 - 

# WORK IN PROGRESS

* gRPC for communication between microservices
* Ocelot as a gateway
* JWT Authorization by Ocelot
* Rate limit is 30 requests/minute
* RabbitMQ for logging system
* Redis for distributed cache
* Mapster for object mapping


## Docker

* docker run --name local-redis -p 6379:6379 -d redis => Redis distrubuted cache
* docker run -it rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management  => RabbitMQ
* docker run --name mongodb -p 27017:27017 mongo => Mongo db
* docker run -p 5432:5432 --name some-postgres -e POSTGRES_PASSWORD=12345 -e POSTGRES_USER=root -d postgres => for postgre db, register service

## To Run
* docker-compose --env-file Development.env up
* docker-compose --env-file Development.env down 
* docker-compose --env-file Development.env down --rmi all

## TO DO
* Fluent Validation
* Ef core to dapper
