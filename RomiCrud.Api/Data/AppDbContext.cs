using Microsoft.EntityFrameworkCore;
using RomiCrud.Api.Models;

namespace RomiCrud.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<PersonType> PersonTypes => Set<PersonType>();
    public DbSet<Gender> Genders => Set<Gender>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<ApplicationErrorLog> ApplicationErrorLogs => Set<ApplicationErrorLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentType>(e =>
        {
            e.ToTable("TiposDocumento");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Code).HasMaxLength(16);
            e.Property(x => x.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<PersonType>(e =>
        {
            e.ToTable("TiposPersona");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Code).HasMaxLength(16);
            e.Property(x => x.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<Gender>(e =>
        {
            e.ToTable("Generos");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Code).HasMaxLength(16);
            e.Property(x => x.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<Person>(e =>
        {
            e.ToTable("Personas");
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).HasMaxLength(128);
            e.Property(x => x.LastName).HasMaxLength(128);
            e.Property(x => x.DocumentNumber).HasMaxLength(32);
            e.HasIndex(x => new { x.DocumentTypeId, x.DocumentNumber }).IsUnique();
            e.HasOne(x => x.DocumentType).WithMany(x => x.Persons).HasForeignKey(x => x.DocumentTypeId);
            e.HasOne(x => x.PersonType).WithMany(x => x.Persons).HasForeignKey(x => x.PersonTypeId);
            e.HasOne(x => x.Gender).WithMany(x => x.Persons).HasForeignKey(x => x.GenderId);
        });

        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Username).IsUnique();
            e.Property(x => x.Username).HasMaxLength(64);
        });

        modelBuilder.Entity<ApplicationErrorLog>(e =>
        {
            e.ToTable("ErroresAplicacion");
            e.HasKey(x => x.Id);
            e.Property(x => x.Message).HasMaxLength(2000);
            e.Property(x => x.StackTrace).HasMaxLength(8000);
            e.Property(x => x.RequestPath).HasMaxLength(512);
            e.Property(x => x.RequestMethod).HasMaxLength(16);
        });
    }
}
