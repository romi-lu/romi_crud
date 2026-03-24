using Microsoft.AspNetCore.Mvc;

namespace RomiCrud.Api.Controllers;

/// <summary>
/// Endpoint de demostración: fuerza una excepción para verificar que el middleware la registra en la tabla ErroresAplicacion.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TestErrorController : ControllerBase
{
    [HttpGet("force")]
    public IActionResult ForceError()
    {
        throw new InvalidOperationException("Error de prueba generado a propósito para el middleware.");
    }
}
