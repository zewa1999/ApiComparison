using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.GrpcAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Net;
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

    public override async Task<UserResponseDto> GetUser(Id id, ServerCallContext context)
    {
        Domain.Entities.User user = null!;

        if (!string.IsNullOrEmpty(id.Id_))
        {
            var guid = Guid.TryParse(id.Id_, out var userId);
            user = await _userService.GetByIdAsync(userId, context.CancellationToken);
        }

        return new UserResponseDto
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Bio = user.Bio
        };
    }

    public override async Task<UserListResponseDto> GetUsers(UserRequestDto request, ServerCallContext context)
    {
        var users = await _userService.GetAllAsync(context.CancellationToken);

        var response = new UserListResponseDto();
        foreach (var user in users)
        {
            Id id = new Id();
            id.Id_ = user.Id.ToString();
            response.Items.Add(new UserResponseDto
            {

                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Bio = user.Bio
            });
        }

        return response;
    }

    public override async Task<AccountResponseDto> GetUserAccount(Id request, ServerCallContext context)
    {
        Guid.TryParse(request.Id_, out var userId);

        var account = await _userService.GetUserAccount(userId, context.CancellationToken);
        Id id = new Id();
        id.Id_ = account.Id.ToString();

        return new AccountResponseDto
        {
            Id = id,
            Email = account.Email,
            Password = account.Password,
            Username = account.Username
        };
    }

    public override async Task<AddressResponseDto> GetUserAddress(Id request, ServerCallContext context)
    {
        Guid.TryParse(request.Id_, out var userId);

        var address = await _userService.GetUserAddress(userId, context.CancellationToken);
        Id id = new Id();
        id.Id_ = address.Id.ToString();

        return new AddressResponseDto
        {
            Id = id,
            City = address.City,
            Street = address.Street,
            StreetNumber = address.StreetNumber
        };
    }

    public override async Task<DishesListResponseDto> GetUserDishes(Id request, ServerCallContext context)
    {
        Guid.TryParse(request.Id_, out var userId);

        var dishes = await _userService.GetUserDishes(userId, context.CancellationToken);

        var response = new DishesListResponseDto();

        foreach(var dish in dishes)
        {
            Id id = new Id();
            id.Id_ = dish.Id.ToString();
            response.Items.Add(new DishResponseDto
            {
                Id = id,
                Description = dish.Description,
                Name= dish.Name,
                PhotoUrl = dish.PhotoUrl
            });
        }

        return response;
    }
    public override async Task<UserResponseDto> PostUser(UserPostRequestDto request, ServerCallContext context)
    {
        Domain.Entities.User user = null!;
        user = await _userService.InsertAsync(new Domain.Entities.User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Bio = request.Bio,
            Account = new Domain.Entities.Account
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            },
            Address = new Domain.Entities.Address
            {
                City = request.City,
                Street = request.Street,
                StreetNumber = request.StreetNumber,
            },
        },
             context.CancellationToken);

        Id id = new Id();
        id.Id_ = user.Id.ToString();
        return new UserResponseDto
        {
            Id = id,
            Bio = user.Bio,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public override async Task<Empty> PutUser(UserPutRequestDto request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id.Id_))
        {
            Guid.TryParse(request.Id.ToString(), out var userId);
            await _userService.UpdateAsync(userId, new Domain.Entities.User
            {
                Bio = request.Bio,
                FirstName = request.FirstName,
                LastName = request.LastName
            }, context.CancellationToken);
        }

        return new Empty();
    }


    public override async Task<Empty> DeleteUser(Id request, ServerCallContext context)
    {
        if (!string.IsNullOrEmpty(request.Id_))
        {
            Guid.TryParse(request.Id_, out var userId);
            await _addressService.DeleteByIdAsync(userId, context.CancellationToken);
        }

        return new Empty();
    }
}