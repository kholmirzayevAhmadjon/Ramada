using AutoMapper;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Customers;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Entities.Payments;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Entities.Users;
using Ramada.Service.DTOs.Addresses;
using Ramada.Service.DTOs.Assets;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.DTOs.Customers;
using Ramada.Service.DTOs.Facilities;
using Ramada.Service.DTOs.Hostels;
using Ramada.Service.DTOs.Payments;
using Ramada.Service.DTOs.Roles;
using Ramada.Service.DTOs.RoomAssets;
using Ramada.Service.DTOs.RoomFacilities;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.DTOs.UserPermissions;
using Ramada.Service.DTOs.Users;

namespace Ramada.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddressCreateModel, Address>().ReverseMap();
        CreateMap<AddressUpdateModel, Address>().ReverseMap();
        CreateMap<Address, AddressViewModel>().ReverseMap();

        CreateMap<AssetCreateModel, Asset>().ReverseMap();
        CreateMap<Asset, AssetViewModel>().ReverseMap();

        CreateMap<BookingCreateModel, Booking>().ReverseMap();
        CreateMap<BookingUpdateModel, Booking>().ReverseMap();
        CreateMap<Booking, BookingViewModel>().ReverseMap();

        CreateMap<CustomerCreateModel, Customer>().ReverseMap();
        CreateMap<CustomerUpdateModel, Customer>().ReverseMap();
        CreateMap<Customer, CustomerViewModel>().ReverseMap();

        CreateMap<FacilityCreateModel, Facility>().ReverseMap();
        CreateMap<FacilityUpdateModel, Facility>().ReverseMap();
        CreateMap<Facility, FacilityViewModel>().ReverseMap();

        CreateMap<HostelCreateModel, Hostel>().ReverseMap();
        CreateMap<HostelUpdateModel, Hostel>().ReverseMap();
        CreateMap<Hostel, HostelViewModel>().ReverseMap();

        CreateMap<PaymentCreateModel, Payment>().ReverseMap();
        CreateMap<Payment, PaymentViewModel>().ReverseMap();

        CreateMap<PermissionCreateModel, Permission>().ReverseMap();
        CreateMap<PermissionUpdateModel, Permission>().ReverseMap();
        CreateMap<Permission, PermissionViewModel>().ReverseMap();

        CreateMap<RoleCreateModel, Role>().ReverseMap();
        CreateMap<RoleUpdateeModel, Role>().ReverseMap();
        CreateMap<Role, RoleViewModel>().ReverseMap();

        CreateMap<RoomCreateModel, Room>().ReverseMap();
        CreateMap<RoomUpdateModel, Room>().ReverseMap();
        CreateMap<Room, RoomViewModel>().ReverseMap();

        CreateMap<RoomAssetCreateModel, RoomAsset>().ReverseMap();
        CreateMap<RoomAsset, RoomAssetViewModel>().ReverseMap();

        CreateMap<RoomFacilityCreateModel, RoomFacility>().ReverseMap();
        CreateMap<RoomFacility, RoomFacilityViewModel>().ReverseMap();

        CreateMap<UserPermissionCreateModel, UserPermission>().ReverseMap();
        CreateMap<UserPermission, UserPermissionViewModel>().ReverseMap();

        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}
