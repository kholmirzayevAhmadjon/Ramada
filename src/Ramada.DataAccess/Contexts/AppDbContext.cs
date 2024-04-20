using Microsoft.EntityFrameworkCore;
using Ramada.Domain.Entities.Users;

namespace Ramada.DataAccess.Contexts;
using BCrypt.Net;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                RoleId = 1,
                Password = BCrypt.HashPassword("SuperAdmin1", BCrypt.GenerateSalt(12)),
                Phone = "+998900223538",
                Email = "makhammadsoliyev@gmail.com",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            },
            new User()
            {
                Id = 2,
                RoleId = 1,
                Password = BCrypt.HashPassword("SuperAdmin2", BCrypt.GenerateSalt(12)),
                Phone = "+998901234567",
                Email = "axmadjon@gmail.com",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            },
            new User()
            {
                Id = 3,
                RoleId = 1,
                Password = BCrypt.HashPassword("SuperAdmin3", BCrypt.GenerateSalt(12)),
                Phone = "+998903478923",
                Email = "faxrulloh@gmail.com",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );

        modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    DeletedAt = null,
                    IsDeleted = false,
                    UpdatedAt = null,
                    UpdatedByUserId = null,
                    CreatedByUserId = 1,
                    DeletedByUserId = null
                },
                new Role()
                {
                    Id = 2,
                    Name = "Customer",
                    CreatedAt = DateTime.UtcNow,
                    DeletedAt = null,
                    IsDeleted = false,
                    UpdatedAt = null,
                    UpdatedByUserId = null,
                    CreatedByUserId = 1,
                    DeletedByUserId = null
                },
                new Role()
                {
                    Id = 3,
                    Name = "Hostel",
                    CreatedAt = DateTime.UtcNow,
                    DeletedAt = null,
                    IsDeleted = false,
                    UpdatedAt = null,
                    UpdatedByUserId = null,
                    CreatedByUserId = 1,
                    DeletedByUserId = null
                }
                );
    }
}
