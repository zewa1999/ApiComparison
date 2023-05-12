using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiComparison.WebApi.Controllers;

[Route("addresses")]
public class AddressController : BaseController<IAddressService, Address, AddressRequestDto, AddressResponseDto>
{
    public AddressController(IAddressService service, IMapper<Address, AddressRequestDto, AddressResponseDto> mapper) : base(service, mapper)
    {
    }
}