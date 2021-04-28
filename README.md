# NET-Template

* Ocelot as a gateway
* JWT as auth
* Rate limit is 30 requests/minute
* RabbitMQ for logging
* Serilog for logging
* Redis for distributed cache


## Docker

* docker run --name local-redis -p 6379:6379 -d redis => Redis distrubuted cache
* docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management  => RabbitMQ
