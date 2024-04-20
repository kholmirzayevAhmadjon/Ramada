using Ramada.DataAccess.Contexts;
using Ramada.DataAccess.Repositories;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Customers;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Entities.Payments;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Entities.Users;

namespace Ramada.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<User> Users { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<Asset> Assets { get; }
    public IRepository<Room> Rooms { get; }
    public IRepository<Hostel> Hostels { get; }
    public IRepository<Payment> Payments { get; }
    public IRepository<Booking> Bookings { get; }
    public IRepository<Address> Addresses { get; }
    public IRepository<Customer> Customers { get; }
    public IRepository<Facility> Facilities { get; }
    public IRepository<RoomAsset> RoomAssets { get; }
    public IRepository<Permission> Permissions { get; }
    public IRepository<RoomFacility> RoomFacilities { get; }
    public IRepository<UserPermission> UsersPermissions { get; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Roles = new Repository<Role>(_context);
        Assets = new Repository<Asset>(_context);
        Rooms = new Repository<Room>(_context);
        Hostels = new Repository<Hostel>(_context);
        Payments = new Repository<Payment>(_context);
        Bookings = new Repository<Booking>(_context);
        Addresses = new Repository<Address>(_context);
        Customers = new Repository<Customer>(_context);
        Facilities = new Repository<Facility>(_context);
        RoomAssets = new Repository<RoomAsset>(_context);
        Permissions = new Repository<Permission>(_context);
        RoomFacilities = new Repository<RoomFacility>(_context);
        UsersPermissions = new Repository<UserPermission>(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}