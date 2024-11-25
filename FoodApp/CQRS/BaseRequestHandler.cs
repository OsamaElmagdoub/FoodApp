using FoodApp.DTOs;
using FoodApp.Helper;
using FoodApp.Repository.Interface;
using MediatR;

namespace FoodApp.CQRS
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly EmailSenderHelper _emailSenderHelper;

        public BaseRequestHandler(RequestParameters requestParameters)
        {
             _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _unitOfWork = requestParameters.UnitOfWork;
            _emailSenderHelper = requestParameters.EmailSenderHelper;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
       
    }
}
