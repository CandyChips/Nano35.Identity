using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Services.Requests;

namespace Nano35.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IMediator _mediator;

        public IdentityController(
            ILogger<IdentityController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var request = new GetAllUsersQuery();
            
            var result = await this._mediator.Send(request);

            return result switch
            {
                IGetAllUsersSuccessResultContract => Ok(result),
                IGetAllUsersErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var request = new GetAllRolesQuery();
            
            var result = await this._mediator.Send(request);

            return result switch
            {
                IGetAllRolesSuccessResultContract => Ok(result),
                IGetAllRolesErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var request = new GetUserByIdQuery() {UserId = id};
            
            var result = await this._mediator.Send(request);

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetUserFromToken")]
        public async Task<IActionResult> GetUserFromToken(Guid id)
        {
            var request = new GetUserFromTokenQuery();
            
            var result = await this._mediator.Send(request);

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var request = new GetRoleByIdQuery() {RoleId = id};
            
            var result = await this._mediator.Send(request);

            return result switch
            {
                IGetRoleByIdSuccessResultContract => Ok(result),
                IGetRoleByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand message)
        {
            var result = await this._mediator.Send(message);
            
            return result switch
            {
                IRegisterSuccessResultContract => Ok(result),
                IRegisterErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateTokenQuery message)
        {
            var result = await this._mediator.Send(message);

            return result switch
            {
                IGenerateTokenSuccessResultContract => Ok(result),
                IGenerateTokenErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdatePhone")]
        public async Task<IActionResult> UpdatePhone(
            [FromBody] UpdatePhoneQuery message)
        {
            var result = await this._mediator.Send(message);
            
            return result switch
            {
                IUpdatePhoneSuccessResultContract => Ok(result),
                IUpdatePhoneErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordQuery message)
        {
            var result = await this._mediator.Send(message);
            
            return result switch
            {
                IUpdatePasswordSuccessResultContract => Ok(result),
                IUpdatePasswordErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
    }
}