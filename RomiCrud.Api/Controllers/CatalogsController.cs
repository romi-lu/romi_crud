using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RomiCrud.Api.Data;
using RomiCrud.Api.Models;

namespace RomiCrud.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly AppDbContext _db;

    public CatalogsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("document-types")]
    public async Task<ActionResult<IEnumerable<DocumentType>>> DocumentTypes() =>
        Ok(await _db.DocumentTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync());

    [HttpGet("person-types")]
    public async Task<ActionResult<IEnumerable<PersonType>>> PersonTypes() =>
        Ok(await _db.PersonTypes.AsNoTracking().OrderBy(x => x.Name).ToListAsync());

    [HttpGet("genders")]
    public async Task<ActionResult<IEnumerable<Gender>>> Genders() =>
        Ok(await _db.Genders.AsNoTracking().OrderBy(x => x.Name).ToListAsync());
}
