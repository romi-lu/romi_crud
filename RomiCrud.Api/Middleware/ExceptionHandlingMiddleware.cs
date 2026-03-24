using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RomiCrud.Api.Data;
using RomiCrud.Api.Models;

namespace RomiCrud.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory scopeFactory)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado");
            await LogAndRespondAsync(context, scopeFactory, ex);
        }
    }

    private static async Task LogAndRespondAsync(
        HttpContext context,
        IServiceScopeFactory scopeFactory,
        Exception exception)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var entry = new ApplicationErrorLog
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            RequestPath = context.Request.Path.Value,
            RequestMethod = context.Request.Method,
            CreatedAtUtc = DateTime.UtcNow
        };

        int? logId = null;
        try
        {
            db.ApplicationErrorLogs.Add(entry);
            await db.SaveChangesAsync();
            logId = entry.Id;
        }
        catch (Exception dbEx)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            logger.LogCritical(dbEx, "No se pudo persistir el error en base de datos");
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var body = JsonSerializer.Serialize(new
        {
            error = "Ocurrió un error en el servidor.",
            errorLogId = logId
        });

        await context.Response.WriteAsync(body);
    }
}
