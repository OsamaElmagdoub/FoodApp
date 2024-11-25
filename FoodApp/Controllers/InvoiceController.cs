using FoodApp.Abstraction;
using FoodApp.CQRS.Invoices.Query;
using FoodApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Controllers;

public class InvoiceController : BaseController
{
    public InvoiceController(ControllerParameters controllerParameters) : base(controllerParameters) { }


    [HttpPost("CreateInvoice")]
    public async Task<Result<InvoiceToReturnDto>> CreateInvoice(int orderId)
    {
        var command = new CreateInvoiceCommand(orderId);
        var result = await _mediator.Send(command);
        return result;
    }

}
