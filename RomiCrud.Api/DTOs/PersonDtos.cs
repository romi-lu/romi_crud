using System.ComponentModel.DataAnnotations;

namespace RomiCrud.Api.DTOs;

public class PersonReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public int DocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; } = string.Empty;
    public int PersonTypeId { get; set; }
    public string PersonTypeName { get; set; } = string.Empty;
    public int GenderId { get; set; }
    public string GenderName { get; set; } = string.Empty;
}

public class PersonCreateDto
{
    [Required, MaxLength(128)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(128)]
    public string LastName { get; set; } = string.Empty;

    [Required, MaxLength(32)]
    public string DocumentNumber { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DocumentTypeId { get; set; }

    [Range(1, int.MaxValue)]
    public int PersonTypeId { get; set; }

    [Range(1, int.MaxValue)]
    public int GenderId { get; set; }
}

public class PersonUpdateDto : PersonCreateDto
{
}
