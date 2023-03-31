using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapper;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AddressService : BaseService<Address, AddressRequestDto, AddressResponseDto>, IAddressService
{
    public AddressService(IBaseRepository<Address> repository,
                          IValidator<AddressRequestDto> validator,
                          IBaseMapper<Address, AddressRequestDto, AddressResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
