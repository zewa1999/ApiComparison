using ApiComparison.Contracts.DishDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.Mapping.Mappers;

internal class DishMapper : IDishMapper
{
    public Dish RequestToEntity(DishRequestDto requestDto)
    {
        return new Dish
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            PhotoUrl = requestDto.PhotoUrl
        };
    }

    public DishResponseDto EntityToResponse(Dish dish)
    {
        return new DishResponseDto
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            PhotoUrl = dish.PhotoUrl
        };
    }
}