using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RomiCrud.Api.Data;
using RomiCrud.Api.DTOs;
using RomiCrud.Api.Models;

namespace RomiCrud.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly AppDbContext _db;

    public PersonsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonReadDto>>> GetAll()
    {
        var list = await _db.Persons
            .AsNoTracking()
            .Include(p => p.DocumentType)
            .Include(p => p.PersonType)
            .Include(p => p.Gender)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Select(p => new PersonReadDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DocumentNumber = p.DocumentNumber,
                DocumentTypeId = p.DocumentTypeId,
                DocumentTypeName = p.DocumentType.Name,
                PersonTypeId = p.PersonTypeId,
                PersonTypeName = p.PersonType.Name,
                GenderId = p.GenderId,
                GenderName = p.Gender.Name
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonReadDto>> GetById(int id)
    {
        var dto = await _db.Persons
            .AsNoTracking()
            .Include(p => p.DocumentType)
            .Include(p => p.PersonType)
            .Include(p => p.Gender)
            .Where(p => p.Id == id)
            .Select(p => new PersonReadDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DocumentNumber = p.DocumentNumber,
                DocumentTypeId = p.DocumentTypeId,
                DocumentTypeName = p.DocumentType.Name,
                PersonTypeId = p.PersonTypeId,
                PersonTypeName = p.PersonType.Name,
                GenderId = p.GenderId,
                GenderName = p.Gender.Name
            })
            .FirstOrDefaultAsync();

        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<PersonReadDto>> Create([FromBody] PersonCreateDto dto)
    {
        if (!await _db.DocumentTypes.AnyAsync(x => x.Id == dto.DocumentTypeId))
            return BadRequest("Tipo de documento inválido.");
        if (!await _db.PersonTypes.AnyAsync(x => x.Id == dto.PersonTypeId))
            return BadRequest("Tipo de persona inválido.");
        if (!await _db.Genders.AnyAsync(x => x.Id == dto.GenderId))
            return BadRequest("Género inválido.");

        var entity = new Person
        {
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            DocumentNumber = dto.DocumentNumber.Trim(),
            DocumentTypeId = dto.DocumentTypeId,
            PersonTypeId = dto.PersonTypeId,
            GenderId = dto.GenderId
        };

        _db.Persons.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, await GetReadDto(entity.Id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PersonUpdateDto dto)
    {
        var entity = await _db.Persons.FindAsync(id);
        if (entity is null)
            return NotFound();

        if (!await _db.DocumentTypes.AnyAsync(x => x.Id == dto.DocumentTypeId))
            return BadRequest("Tipo de documento inválido.");
        if (!await _db.PersonTypes.AnyAsync(x => x.Id == dto.PersonTypeId))
            return BadRequest("Tipo de persona inválido.");
        if (!await _db.Genders.AnyAsync(x => x.Id == dto.GenderId))
            return BadRequest("Género inválido.");

        entity.FirstName = dto.FirstName.Trim();
        entity.LastName = dto.LastName.Trim();
        entity.DocumentNumber = dto.DocumentNumber.Trim();
        entity.DocumentTypeId = dto.DocumentTypeId;
        entity.PersonTypeId = dto.PersonTypeId;
        entity.GenderId = dto.GenderId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Persons.FindAsync(id);
        if (entity is null)
            return NotFound();

        _db.Persons.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<PersonReadDto?> GetReadDto(int id)
    {
        return await _db.Persons
            .AsNoTracking()
            .Include(p => p.DocumentType)
            .Include(p => p.PersonType)
            .Include(p => p.Gender)
            .Where(p => p.Id == id)
            .Select(p => new PersonReadDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DocumentNumber = p.DocumentNumber,
                DocumentTypeId = p.DocumentTypeId,
                DocumentTypeName = p.DocumentType.Name,
                PersonTypeId = p.PersonTypeId,
                PersonTypeName = p.PersonType.Name,
                GenderId = p.GenderId,
                GenderName = p.Gender.Name
            })
            .FirstOrDefaultAsync();
    }
}
