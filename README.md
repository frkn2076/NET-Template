# NET-Template

* gRPC for communication between microservices
* Ocelot as a gateway
* JWT as auth
* Rate limit is 30 requests/minute
* RabbitMQ for logging system
* Redis for distributed cache
* Mapster for object mapping


## Docker

* docker run --name local-redis -p 6379:6379 -d redis => Redis distrubuted cache
* docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management  => RabbitMQ
* docker run --name mongodb -p 27017:27017 mongo => Mongo db
* docker run -p 5432:5432 --name some-postgres -e POSTGRES_PASSWORD=toortoor -e POSTGRES_USER=root -d onjin/alpine-postgres => for postgre db, register service

## To Run
* docker-compose --env-file Development.env up
* docker-compose --env-file Development.env down 
* docker-compose --env-file Development.env down --rmi all
