using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AddressService : BaseService<Address, AddressRequestDto, AddressResponseDto>, IAddressService
{
    public AddressService(IAddressRepository repository,
                          IValidator<AddressRequestDto> validator,
                          IMapper<Address, AddressRequestDto, AddressResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
