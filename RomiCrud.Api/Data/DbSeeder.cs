using Microsoft.EntityFrameworkCore;
using RomiCrud.Api.Models;

namespace RomiCrud.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await db.Database.MigrateAsync();

        if (!await db.DocumentTypes.AnyAsync())
        {
            db.DocumentTypes.AddRange(
                new DocumentType { Code = "DNI", Name = "Documento Nacional de Identidad" },
                new DocumentType { Code = "PAS", Name = "Pasaporte" },
                new DocumentType { Code = "LC", Name = "Libreta Cívica" });
        }

        if (!await db.PersonTypes.AnyAsync())
        {
            db.PersonTypes.AddRange(
                new PersonType { Code = "NAT", Name = "Persona natural" },
                new PersonType { Code = "JUR", Name = "Persona jurídica (representante)" });
        }

        if (!await db.Genders.AnyAsync())
        {
            db.Genders.AddRange(
                new Gender { Code = "M", Name = "Masculino" },
                new Gender { Code = "F", Name = "Femenino" },
                new Gender { Code = "X", Name = "Otro / no informa" });
        }

        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new ApplicationUser
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
            });
        }

        await db.SaveChangesAsync();
    }
}
