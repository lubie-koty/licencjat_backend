namespace notes_backend.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using notes_backend.Entities.Models;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Note> Notes => Set<Note>();
}
