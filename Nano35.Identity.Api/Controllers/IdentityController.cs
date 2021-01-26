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
            try
            {
                var query = new GetAllUsersQuery();
                var result = await this._mediator.Send(query);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var query = new GetAllRolesQuery();
                var result = await this._mediator.Send(query);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var query = new GetUserByIdQuery() { UserId = id };
                var result = await this._mediator.Send(query);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            try
            {
                var query = new GetRoleByIdQuery() { RoleId = id };
                var result = await this._mediator.Send(query);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand message)
        {
            try
            {
                if (message == null) return BadRequest();
                var result = await this._mediator.Send(message);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
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
            try
            {
                if (message == null) return BadRequest();
                var result = await this._mediator.Send(message);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordQuery message)
        {
            try
            {
                if (message == null) return BadRequest();
                var result = await this._mediator.Send(message);
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
    }
}