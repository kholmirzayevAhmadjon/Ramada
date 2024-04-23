using Microsoft.EntityFrameworkCore;
using Ramada.Domain.Entities.Users;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Customers;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Entities.Payments;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Enums;

namespace Ramada.DataAccess.Contexts;
using BCrypt.Net;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Hostel> Hostels { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<RoomAsset> RoomAssets { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RoomFacility> RoomFacilities { get; set; }
    public DbSet<UserPermissionn> UserPermissions { get; set; }


    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Users
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                RoleId = 1,
                Password = BCrypt.HashPassword("SuperAdmin123", BCrypt.GenerateSalt(12)),
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
                RoleId = 2,
                Password = BCrypt.HashPassword("Customer123", BCrypt.GenerateSalt(12)),
                Phone = "+998910052450",
                Email = "ahmadjon@gmail.com",
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
                RoleId = 3,
                Password = BCrypt.HashPassword("Hostel123", BCrypt.GenerateSalt(12)),
                Phone = "+998903478923",
                Email = "sayidahror@gmail.com",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Roles
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
        #endregion

        #region Customers
        modelBuilder.Entity<Customer>().HasData
            (
            new Customer()
            {
                Id = 1,
                FirstName = "Axamadjon",
                LastName = "Xolmirzayev",
                AssetId = null,
                UserId = 2,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Hostels
        modelBuilder.Entity<Hostel>().HasData
            (
            new Hostel()
            {
                Id = 1,
                Name = "777",
                Description = "5 star",
                AssetId = null,
                AddressId = 1,
                UserId = 3,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Addresses
        modelBuilder.Entity<Address>().HasData
            (
            new Address()
            {
                Id = 1,
                Latitude = "41.3172712",
                Longitude = "69.2626015",
                City = "Tashkent",
                Street = "Asr",
                PostCode = 120012,
                HouseNumber = 77,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Permission
        modelBuilder.Entity<Permission>().HasData
            (
            new Permission()
            {
                Id = 1,
                Method = "Post",
                Controller = "Users",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region UserPermissions
        modelBuilder.Entity<UserPermissionn>().HasData
            (
            new UserPermissionn()
            {
                Id = 1,
                PermissionId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Rooms
        modelBuilder.Entity<Room>().HasData
            (
            new Room()
            {
                Id = 1,
                HostelId = 1, 
                Price = 67,
                RoomNumber = 12,
                Description = "With Air Condition",
                Floor = 1,
                Size = "45m kv",
                MaxPeopleSize = 4,
                Status = RoomStatus.Empty,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region Facilities
        modelBuilder.Entity<Facility>().HasData
            (
            new Facility()
            {
                Id = 1,
                Name = "Desk",
                Description = "With Air Condition",
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion

        #region RoomFacilities
        modelBuilder.Entity<RoomFacility>().HasData
            (
            new RoomFacility()
            {
                Id = 1,
                RoomId = 1,
                FacilityId = 1,
                Count = 1,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null
            }
            );
        #endregion
    }
}
