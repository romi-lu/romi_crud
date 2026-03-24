namespace RomiCrud.Api.Models;

public class DocumentType
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<Person> Persons { get; set; } = new List<Person>();
}
