using ApiComparison.Application.Interfaces;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using User = ApiComparison.GrpcAPI.User;

namespace ApiComparison.GrpcApi.Services;

public class UserService : User.UserBase
{
    private readonly IUserService _userService;
    private readonly IAddressService _addressService;
    private readonly IAccountService _accountService;

    public UserService(IUserService userService, IAddressService addressService, IAccountService accountService)
    {
        _userService = userService;
        _addressService = addressService;
        _accountService = accountService;
    }

    public override async Task<UserResponseDto> GetUser(Id request, ServerCallContext context)
    {
        Domain.Entities.User user = null!;

        if (!string.IsNullOrEmpty(request.Id_))
        {
            var guid = Guid.TryParse(request.Id_, out var userId);
            user = await _userService.GetByID(userId, context.CancellationToken);
        }

        return new UserResponseDto
        {
            Id = user.Id.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Bio = user.Bio,
            AccountResponseDto = new AccountResponseDto()
            {
                Id = user.Account.Id.ToString(),
                Email = user.Account.Email,
                Password = user.Account.Password,
                Username = user.Account.Username
            },
            AddressResponseDto = new AddressResponseDto()
            {
                Id = user.Address.Id.ToString(),
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
            }
        };
    }

    public override async Task<UserListResponseDto> GetUsers(UserRequestDto request, ServerCallContext context)
    {
        var users = await _userService.GetAll(context.CancellationToken);

        return new UserListResponseDto
        {
            Items = { users.Select((Domain.Entities.User user) => new UserResponseDto
            {
                Id = user.Id.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Bio = user.Bio,
            AccountResponseDto = new AccountResponseDto()
            {
                Id = user.Account.Id.ToString(),
                Email = user.Account.Email,
                Password = user.Account.Password,
                Username = user.Account.Username
            },
            AddressResponseDto = new AddressResponseDto()
            {
                Id = user.Address.Id.ToString(),
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
            }
            })
            }
        };
    }

    public override async Task<UserResponseDto> PostUser(UserRequestDto request, ServerCallContext context)
    {
        Domain.Entities.User user = null!;

        Guid.TryParse(request.AddressId, out var addressId);
        Guid.TryParse(request.AccountId, out var accountId);
        var address = await _addressService.GetByID(addressId, context.CancellationToken);
        var account = await _accountService.GetByID(accountId, context.CancellationToken);
        user = await _userService.Insert(new Domain.Entities.User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Bio = request.Bio,
            Account = account,
            Address = address,
            AccountId = account.Id,
            AddressId = address.Id,
        },
             context.CancellationToken);

        return new UserResponseDto
        {
            Id = user.Id.ToString(),
            Bio = user.Bio,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccountResponseDto = new AccountResponseDto()
            {
                Id = user.Account.Id.ToString(),
                Email = user.Account.Email,
                Password = user.Account.Password,
                Username = user.Account.Username
            },
            AddressResponseDto = new AddressResponseDto()
            {
                Id = user.Address.Id.ToString(),
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
            }
        };
    }

    public override async Task<Empty> PutUser(UserPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.AccountId, out var accountId);
            Guid.TryParse(request.AddressId, out var addressId);
            await _userService.Update(addressId, new Domain.Entities.User
            {
                Bio = request.Bio,
                FirstName = request.FirstName,
                LastName = request.LastName,
                AccountId = accountId,
                AddressId = addressId
            }, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteUser(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var userId);
            await _addressService.DeleteById(userId, context.CancellationToken);
        }

        return new Empty();
    }
}