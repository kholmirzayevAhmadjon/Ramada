using Ramada.DataAccess.Repositories;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Customers;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Entities.Payments;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Entities.Users;

namespace Ramada.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Room> Rooms { get; }
    IRepository<Hostel> Hostels { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Booking> Bookings { get; }
    IRepository<Address> Addresses { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Facility> Facilities { get; }
    IRepository<RoomAsset> RoomAssets { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<RoomFacility> RoomFacilities { get; }
    IRepository<UserPermission> UsersPermissions { get; }

    ValueTask<bool> SaveAsync();
}
