using Microsoft.EntityFrameworkCore;

namespace Ramada.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
}
