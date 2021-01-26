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
            var result = await this._mediator.Send(new GetAllUsersQuery());
            if (result is IGetAllUsersSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGetAllUsersErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await this._mediator.Send(new GetAllRolesQuery());
            if (result is IGetAllRolesSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGetAllRolesErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await this._mediator.Send(new GetUserByIdQuery() { UserId = id });
            if (result is IGetUserByIdSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGetUserByIdErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var result = await this._mediator.Send(new GetRoleByIdQuery() { RoleId = id });
            if (result is IGetRoleByIdSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGetRoleByIdErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand message)
        {
            var result = await this._mediator.Send(message);
            if (result is IRegisterSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IRegisterErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateTokenQuery message)
        {
            var result = await this._mediator.Send(message);
            if (result is IGenerateTokenSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGenerateTokenErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdatePhone")]
        public async Task<IActionResult> UpdatePhone(
            [FromBody] UpdatePhoneQuery message)
        {
            var result = await this._mediator.Send(message);
            if (result is IUpdatePhoneSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IUpdatePhoneErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordQuery message)
        {
            var result = await this._mediator.Send(message);
            if (result is IUpdatePasswordSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IUpdatePasswordErrorResultContract)
            {
                return BadRequest(result);
            }
            return BadRequest();
        }
    }
}