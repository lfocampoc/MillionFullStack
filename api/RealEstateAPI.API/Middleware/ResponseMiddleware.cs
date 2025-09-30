using RealEstateAPI.Core.DTOs;
using System.Net;
using System.Text.Json;

namespace RealEstateAPI.API.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseMiddleware> _logger;

        public ResponseMiddleware(RequestDelegate next, ILogger<ResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                // Solo procesar respuestas exitosas (200-299)
                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    await HandleSuccessResponse(context, responseBody, originalBodyStream);
                }
                else
                {
                    await HandleErrorResponse(context, responseBody, originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el middleware de respuestas");
                await HandleExceptionResponse(context, ex, originalBodyStream);
            }
        }

        private async Task HandleSuccessResponse(HttpContext context, MemoryStream responseBody, Stream originalBodyStream)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

            object? data = null;
            string message = "Operación exitosa";

            // Si hay contenido en la respuesta, intentar deserializarlo
            if (!string.IsNullOrEmpty(responseBodyText))
            {
                try
                {
                    data = JsonSerializer.Deserialize<object>(responseBodyText);
                }
                catch
                {
                    data = responseBodyText;
                }
            }

            var apiResponse = ApiResponse<object>.SuccessResult(data ?? new object(), message);

            var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = null;

            responseBody.SetLength(0);
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.WriteAsync(System.Text.Encoding.UTF8.GetBytes(jsonResponse));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task HandleErrorResponse(HttpContext context, MemoryStream responseBody, Stream originalBodyStream)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

            string message = GetErrorMessage(context.Response.StatusCode);
            string error = responseBodyText;

            var apiResponse = ApiResponse<object>.ErrorResult(error, message);

            var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = null;

            responseBody.SetLength(0);
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.WriteAsync(System.Text.Encoding.UTF8.GetBytes(jsonResponse));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task HandleExceptionResponse(HttpContext context, Exception ex, Stream originalBodyStream)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var apiResponse = ApiResponse<object>.ErrorResult(ex.Message, "Error interno del servidor");

            var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = null;

            await context.Response.WriteAsync(jsonResponse);
        }

        private string GetErrorMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Solicitud incorrecta",
                401 => "No autorizado",
                403 => "Acceso prohibido",
                404 => "Recurso no encontrado",
                500 => "Error interno del servidor",
                _ => "Error en la operación"
            };
        }
    }
}
