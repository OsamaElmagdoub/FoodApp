using FoodApp.Abstraction;
using FoodApp.CQRS.Users.Queries;
using FoodApp.DTOs;
using FoodApp.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;

        public BaseController(ControllerParameters controllerParameters)
        {
            _mediator = controllerParameters.Mediator;
            _userState = controllerParameters.UserState;

            var loggedUser = new HttpContextAccessor().HttpContext.User;

            _userState.Role = loggedUser.FindFirst("RoleID")?.Value ?? string.Empty;
            _userState.ID = loggedUser.FindFirst("UserId")?.Value ?? string.Empty;
            _userState.Name = loggedUser.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

 
        }
    }
}
