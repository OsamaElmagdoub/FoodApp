using MediatR;

namespace FoodApp.DTOs
{
    public class ControllerParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }

        public ControllerParameters(IMediator mediator, UserState userState)
        {
            Mediator = mediator;
            UserState = userState;
        }
    }
}
