using FoodApp.Abstraction;
using FoodApp.CQRS.Roles.Commands;
using FoodApp.CQRS.Roles.Queries;
using FoodApp.CQRS.UserRoles.Commands;
using FoodApp.Data.Entities;
using FoodApp.DTOs;
using FoodApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Controllers
{
    public class RolesController : BaseController
    {
        public RolesController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [Authorize]
        [HttpPost("AddRole")]
        public async Task<Result<bool>> AddRoleToUser([FromBody] CreateRoleViewModel viewModel)
        {
            var command = viewModel.Map<CreateRoleCommand>();
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpPost("AssignRoleToUser")]
        public async Task<Result<bool>> AssignRoleToUser([FromBody] AssignRoleToUserViewModel viewModel)
        {
            var response = await _mediator.Send(new AddRoleToUserCommand(viewModel.UserId, viewModel.RoleName));
            return response;
        }
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<Result<bool>> RemoveRoleFromUser(int userId, int roleId)
        {
            var response = await _mediator.Send(new RemoveRoleFromUserCommand(userId, roleId));
            return response;
        }
        [HttpDelete("RemoveRole/{roleId}")]
        public async Task<Result<bool>> RemoveRole(int roleId)
        {
            var response = await _mediator.Send(new RemoveRoleCommand(roleId));
            return response;
        }
        [HttpGet("GetAllRoles")]
        public async Task<Result<List<Role>>> GetAllRoles()
        {
            var response = await _mediator.Send(new GetAllRolesQuery());
            return response;
        } 

    }
}
