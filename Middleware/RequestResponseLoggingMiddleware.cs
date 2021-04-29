using LogPublisher;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var builder = new StringBuilder();
            var requestBody = await GetRequestBody(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;
            //Create a new memory stream...
            using var responseBodyStream = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBodyStream;
            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);
            //Format the response from the server
            var responseBody = await FormatResponse(context.Response);
            builder.Append("Response: ").AppendLine(responseBody);
            builder.AppendLine("Response headers: ");
            foreach (var header in context.Response.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }
            //Save log to chosen datastore
            var a = builder.ToString();

            var message = new
            {
                Endpoint = context.Request.Path,
                IPAddress = context.Connection.RemoteIpAddress.ToString(),
                RequestHeaders = context.Request.Headers,
                RequestBody = requestBody,
                ResponseHeaders = context.Response.Headers,
                ResponseBody = responseBody,
                ResponseStatusCode = context.Response.StatusCode,
                Time = DateTime.Now
            };

            var logRecord = JsonConvert.SerializeObject(message);

            Publisher.Send("logging", logRecord);

            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;
            return body;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            //...and copy it into a string
            string body = await new StreamReader(response.Body).ReadToEndAsync();
            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return body;
        }
    }
}
