using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
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

    public override async Task<AddressResponseDto> GetAddress(Id id, ServerCallContext context)
    {
        Domain.Entities.Address address = null!;

        if (!string.IsNullOrEmpty(id.Id_))
        {
            Guid.TryParse(id.Id_, out var addressId);
            address = await _addressService.GetByIdAsync(addressId, context.CancellationToken);
        }

        return new AddressResponseDto()
        {
            Id = id,
            City = address.City,
            Street = address.Street,
            StreetNumber = address.StreetNumber
        };
    }

    public override async Task<AddressListResponseDto> GetAddresses(AddressRequestDto request, ServerCallContext context)
    {
        var addresses = await _addressService.GetAllAsync(context.CancellationToken);

        var addressesListResponseDto = new AddressListResponseDto();

        foreach (var address in addresses)
        {
            Id id = new Id();
            id.Id_ = address.Id.ToString();

            addressesListResponseDto.Items.Add(new AddressResponseDto
            {
                Id = id,
                City = address.City,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
            });
        }
        return addressesListResponseDto;
    }


    public override async Task<AddressResponseDto> PostAddress(AddressRequestDto request, ServerCallContext context)
    {
        var address = await _addressService.InsertAsync(new Domain.Entities.Address
        {
            City = request.City,
            Street = request.Street,
            StreetNumber = request.StreetNumber
        }, context.CancellationToken);

        Id id = new Id();
        id.Id_ = address.Id.ToString();
        return new AddressResponseDto()
        {
            Id = id,
            City = address.City,
            Street = address.Street,
            StreetNumber = address.StreetNumber
        };
    }

    public override async Task<Empty> PutAddress(AddressPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.Id_, out var addressId);
            await _addressService.UpdateAsync(addressId, new Domain.Entities.Address
            {
                Id = addressId,
                City = request.City,
                Street = request.Street,
                StreetNumber = request.StreetNumber
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