using FoodApp.Abstraction;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.CQRS.Orders.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<Result<Order>>;

public class GetOrderByIdQueryHandler : BaseRequestHandler<GetOrderByIdQuery, Result<Order>>
{
    public GetOrderByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
        if (order == null)
        {
            return Result.Failure<Order>(OrderErrors.OrderNotFound);
        }

        return Result.Success(order);
    }
}
