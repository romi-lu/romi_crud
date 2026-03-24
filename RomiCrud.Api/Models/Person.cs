namespace RomiCrud.Api.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;

    public int DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = null!;

    public int PersonTypeId { get; set; }
    public PersonType PersonType { get; set; } = null!;

    public int GenderId { get; set; }
    public Gender Gender { get; set; } = null!;
}
