using FoodApp.Abstraction;
using FoodApp.CQRS.Orders.Command;
using FoodApp.DTOs;
using FoodApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Controllers
{

    public class OrderController : BaseController
    {
        public OrderController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("CreateOrder")]
        public async Task<Result<OrderToReturnDto>> MakeOrder(CreateOrderViewModel viewModel)
        {
            var command = viewModel.Map<CreateOrderCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        // Authorize for Cashier Only
        [HttpPost("UpdateOrderStatus")]
        public async Task<Result<bool>> UpdateOrderStatus([FromForm]UpdateOrderStatusViewModel viewModel)
        {
            var command = viewModel.Map<UpdateOrderStatusCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
