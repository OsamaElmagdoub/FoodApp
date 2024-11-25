using FoodApp.Abstraction;
using FoodApp.CQRS.Discounts.Commands;
using FoodApp.CQRS.Recipes.Queries;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.Errors;
using Hangfire.Server;
using MediatR;
using ProjectManagementSystem.Helper;
using System.ComponentModel.DataAnnotations;

namespace FoodApp.CQRS.Orders.Command
{
    public record CreateOrderCommand(List<OrderItemDto> OrderItems, AddressDto ShippingAddress) : IRequest<Result<OrderToReturnDto>>;

    public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, Result<OrderToReturnDto>>
    {
        public CreateOrderCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<OrderToReturnDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in request.OrderItems)
            {
                var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(item.RecipeId));
                var recipe = recipeResult.Data;


                var discount = recipe.RecipeDiscounts
                    .Select(x => x.Discount.DiscountPercent)
                    .FirstOrDefault();

                var discountedPrice = recipe.Price - (recipe.Price * (discount / 100));

                var totalAmountForItem = item.Quantity * discountedPrice;
                totalAmount += totalAmountForItem;

                var orderItem = new OrderItem
                {
                    RecipeId = recipe.Id,
                    RecipeName = recipe.Name,
                    Quantity = item.Quantity,
                    Price = recipe.Price               
                };

                orderItems.Add(orderItem);
            }
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<OrderToReturnDto>(UserErrors.UserNotAuthenticated);
            }

            var order = new Order
            {
                UserId =  int.Parse(userId),
                TotalPrice = totalAmount,
                OrderItems = orderItems,
                ShppingAddress = request.ShippingAddress.Map<Address>()
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var mappedOrder = order.Map<OrderToReturnDto>();

            return Result.Success(mappedOrder);
        }
    }

 
   
}
