using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.WebApi.Controllers;

public class AddressController : BaseController<IAddressService, Address, AddressRequestDto, AddressResponseDto>
{
    public AddressController(IAddressService service, IMapper<Address, AddressRequestDto, AddressResponseDto> mapper) : base(service, mapper)
    {
    }
}