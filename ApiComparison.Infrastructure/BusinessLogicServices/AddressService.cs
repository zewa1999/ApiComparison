using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AddressService : BaseService<Address>, IAddressService
{
    public AddressService(IAddressRepository repository,
                          IValidator<Address> validator) : base(repository, validator)
    {
    }
}