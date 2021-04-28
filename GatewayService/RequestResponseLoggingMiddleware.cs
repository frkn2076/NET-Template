using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayService
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private IModel _channel;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "logging", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var builder = new StringBuilder();
            var request = await FormatRequest(context.Request);
            builder.Append("Request: ").AppendLine(request);
            builder.Append("Ip Adress: ").AppendLine(context.Connection.RemoteIpAddress.ToString());
            builder.AppendLine("Request headers: ");
            context.Request.Headers.ToList().ForEach(header => builder.Append(header.Key).Append(": ").AppendLine(header.Value));

            var message = builder.ToString();
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "logging", basicProperties: null, body: body);

            ////Copy a pointer to the original response body stream
            //var originalBodyStream = context.Response.Body;
            ////Create a new memory stream...
            //using var responseBody = new MemoryStream();
            ////...and use that for the temporary response body
            //context.Response.Body = responseBody;
            ////Continue down the Middleware pipeline, eventually returning to this class
            //await _next(context);
            ////Format the response from the server
            //var response = await FormatResponse(context.Response);
            //builder.Append("Response: ").AppendLine(response);
            //builder.AppendLine("Response headers: ");
            //foreach (var header in context.Response.Headers)
            //{
            //    builder.Append(header.Key).Append(':').AppendLine(header.Value);
            //}
            ////Save log to chosen datastore
            //_logger.LogInformation(builder.ToString());
            ////Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            //await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            // Do some processing with body…
            var formattedRequest = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;
            return formattedRequest;
        }

        //private async Task<string> FormatResponse(HttpResponse response)
        //{
        //    //We need to read the response stream from the beginning...
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    //...and copy it into a string
        //    string text = await new StreamReader(response.Body).ReadToEndAsync();
        //    //We need to reset the reader for the response so that the client can read it.
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
        //    return $"{response.StatusCode}: {text}";
        //}
    }
}
