using FoodApp.Abstraction;
using FoodApp.CQRS.Orders.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;

namespace FoodApp.CQRS.Orders.Command;

public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<Result<bool>>;

public class UpdateOrderStatusCommandHandler : BaseRequestHandler<UpdateOrderStatusCommand, Result<bool>>
{
    public UpdateOrderStatusCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
        if (!orderResult.IsSuccess)
        {
            return Result.Failure<bool>(OrderErrors.OrderNotFound);
        }

        var order = orderResult.Data;
        order.status = request.NewStatus;

        _unitOfWork.Repository<Order>().Update(order);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(true);
    }
}




