using ApiComparison.Application.Interfaces;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Address = ApiComparison.GrpcAPI.Address;

namespace ApiComparison.GrpcApi.Services;

public class AddressService : Address.AddressBase
{
    private readonly IAddressService _addressService;
    private readonly IUserService _userService;

    public AddressService(IAddressService addressService, IUserService userService)
    {
        _addressService = addressService;
        _userService = userService;
    }

    public override async Task<AddressResponseDto> GetAddress(Id request, ServerCallContext context)
    {
        Domain.Entities.Address address = null!;

        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var addressId);
            address = await _addressService.GetByIdAsync(addressId, context.CancellationToken);
        }

        return new AddressResponseDto()
        {
            Id = request.Id_,
            City = address.City,
            Street = address.Street,
            StreetNumber = address.StreetNumber,
            User = new UserResponseDto()
            {
                Id = address.User.Id.ToString(),
                FirstName = address.User.FirstName,
                LastName = address.User.LastName,
                Bio = address.User.Bio,
                AccountResponseDto = new AccountResponseDto()
                {
                    Id = address.User.Account.Id.ToString(),
                    Email = address.User.Account.Email,
                    Password = address.User.Account.Password,
                    Username = address.User.Account.Username
                },
                AddressResponseDto = new AddressResponseDto()
                {
                    Id = address.User.Address.Id.ToString(),
                    City = address.User.Address.City,
                    Street = address.User.Address.Street,
                    StreetNumber = address.User.Address.StreetNumber,
                }
            }
        };
    }

    public override async Task<AddressListResponseDto> GetAddresses(AddressRequestDto request, ServerCallContext context)
    {
        var addresses = await _addressService.GetAllAsync(context.CancellationToken);

        var addressesListResponseDto = new AddressListResponseDto();

        foreach (var address in addresses)
        {
            addressesListResponseDto.Items.Add(new AddressResponseDto
            {
                Id = address.Id.ToString(),
                City = address.City,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
                User = new UserResponseDto()
                {
                    Id = address.User.Id.ToString(),
                    FirstName = address.User.FirstName,
                    LastName = address.User.LastName,
                    Bio = address.User.Bio,
                    AccountResponseDto = new AccountResponseDto()
                    {
                        Id = address.User.Account.Id.ToString(),
                        Email = address.User.Account.Email,
                        Password = address.User.Account.Password,
                        Username = address.User.Account.Username
                    },
                    AddressResponseDto = new AddressResponseDto()
                    {
                        Id = address.User.Address.Id.ToString(),
                        City = address.User.Address.City,
                        Street = address.User.Address.Street,
                        StreetNumber = address.User.Address.StreetNumber,
                    }
                }
            });
        }
        return addressesListResponseDto;
    }

    public override async Task<AddressResponseDto> PostAddress(AddressRequestDto request, ServerCallContext context)
    {
        Guid.TryParse(request.UserId, out var addressId);

        var user = await _userService.GetByIdAsync(addressId, context.CancellationToken);

        var address = await _addressService.InsertAsync(new Domain.Entities.Address
        {
            City = request.City,
            Street = request.Street,
            StreetNumber = request.StreetNumber,
            User = user,
        }, context.CancellationToken);

        return new AddressResponseDto()
        {
            Id = address.Id.ToString(),
            City = address.City,
            Street = address.Street,
            StreetNumber = address.StreetNumber,
            User = new UserResponseDto()
            {
                Id = address.User.Id.ToString(),
                FirstName = address.User.FirstName,
                LastName = address.User.LastName,
                Bio = address.User.Bio,
                AccountResponseDto = new AccountResponseDto()
                {
                    Id = address.User.Account.Id.ToString(),
                    Email = address.User.Account.Email,
                    Password = address.User.Account.Password,
                    Username = address.User.Account.Username
                },
                AddressResponseDto = new AddressResponseDto()
                {
                    Id = address.User.Address.Id.ToString(),
                    City = address.User.Address.City,
                    Street = address.User.Address.Street,
                    StreetNumber = address.User.Address.StreetNumber,
                }
            }
        };
    }

    public override async Task<Empty> PutAddress(AddressPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var addressId);
            Guid.TryParse(request.Id.Id_, out var userId);
            await _addressService.UpdateAsync(addressId, new Domain.Entities.Address
            {
                Id = addressId,
                City = request.City,
                Street = request.Street,
                StreetNumber = request.StreetNumber,
                UserId = userId
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteAddress(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var accountId);
            await _addressService.DeleteByIdAsync(accountId, context.CancellationToken);
        }

        return new Empty();
    }
}