using FoodApp.Abstraction;
using FoodApp.CQRS.Discounts.Commands;
using FoodApp.CQRS.Discounts.Queries;
using FoodApp.DTOs;
using FoodApp.Errors;
using FoodApp.Response;
using FoodApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Controllers
{

    public class DiscountController : BaseController
    {
        public DiscountController(ControllerParameters controllerParameters):base(controllerParameters) { }

        [HttpPost("AddDiscount")]
        public async Task<Result<int>> AddDiscount(AddDiscountViewModel viewModel)
        {
            var command = viewModel.Map<AddDiscountCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteDiscount/{id}")]
        public async Task<Result<bool>> DeleteDiscount(int id)
        {
            var result = await _mediator.Send(new DeleteDiscountCommand(id));
            return result;
        }

        [HttpPost("UpdateDiscount/{id}")]
        public async Task<Result<bool>> UpdateDiscount(int id, UpdateDiscountViewModel viewModel)
        {
            var command = new UpdateDiscountCommand(id, viewModel.DiscountPercent, viewModel.StartDate, viewModel.EndDate);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("DeactivateDiscount")]
        public async Task<Result<bool>> DeactivateDiscount(int discountId)
        {
            var command = new DeactivateDiscountCommand(discountId);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("ViewDiscount/{id}")]
        public async Task<Result<DiscountToReturnDto>> GetDiscountById(int id)
        {
            var result = await _mediator.Send(new GetDiscountByIdQuery(id));
            if(!result.IsSuccess)
            {
                return Result.Failure<DiscountToReturnDto>(DiscountErrors.DiscountNotFound);
            }
            var discount = result.Data.Map<DiscountToReturnDto>();
            return Result.Success(discount);
        }

        [HttpGet("GetActiveDiscounts")]
        public async Task<Result<IEnumerable<DiscountToReturnDto>>> GetAllActiveDiscounts()
        {
            var result = await _mediator.Send(new GetAllActiveDiscountsQuery());
            return result;
        }



        [HttpPost("ApplyDiscount")]
        public async Task<Result<decimal>> ApplyDiscount(ApplyDiscountViewModel viewModel)
        {
            var command = viewModel.Map<ApplyDiscountCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
