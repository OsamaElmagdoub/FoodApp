using FoodApp.Abstraction;
using FoodApp.CQRS.Orders.Command;
using FoodApp.CQRS.Orders.Queries;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using ProjectManagementSystem.Helper;
using FoodApp.Data.Entities;

namespace FoodApp.CQRS.Invoices.Query;

public record CreateInvoiceCommand(int OrderId) : IRequest<Result<InvoiceToReturnDto>>;

public class GenerateInvoiceCommandHandler : BaseRequestHandler<CreateInvoiceCommand, Result<InvoiceToReturnDto>>
{
    public GenerateInvoiceCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<InvoiceToReturnDto>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
        if (!orderResult.IsSuccess)
        {
            return Result.Failure<InvoiceToReturnDto>(OrderErrors.OrderNotFound);
        }

        var order = orderResult.Data;

        var invoice = new Invoice
        {
            OrderId = order.Id,
            TotalPrice = order.TotalPrice
        };

        await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
        await _unitOfWork.SaveChangesAsync();

        var invoiceToReturnDto = invoice.Map<InvoiceToReturnDto>();

        return Result.Success(invoiceToReturnDto);
    }
}
