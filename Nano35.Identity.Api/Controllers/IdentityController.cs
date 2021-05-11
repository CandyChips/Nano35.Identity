using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;
using Nano35.HttpContext.identity;
using Nano35.Identity.Api.Requests;
using Nano35.Identity.Api.Requests.GenerateToken;
using Nano35.Identity.Api.Requests.GetAllUsers;
using Nano35.Identity.Api.Requests.GetUserById;
using Nano35.Identity.Api.Requests.GetUserByToken;
using Nano35.Identity.Api.Requests.Register;
using Nano35.Identity.Api.Requests.UpdateEmail;
using Nano35.Identity.Api.Requests.UpdateName;
using Nano35.Identity.Api.Requests.UpdatePassword;
using Nano35.Identity.Api.Requests.UpdatePhone;

namespace Nano35.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IServiceProvider  _services;
        public IdentityController(IServiceProvider  services) => _services = services;

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetUserByIdResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]    
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result =
                await new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                        _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,
                        new GetUserById(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetUserByIdRequestContract() {UserId = id});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("All")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetAllUsersResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]    
        public async Task<IActionResult> GetAllUsers()
        {
            var result = 
                await new LoggedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUsersRequestContract>)) as ILogger<IGetAllUsersRequestContract>,
                    new GetAllUsers(
                        _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllUsersRequestContract());
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet]
        [Route("FromToken")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetUserByIdResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> GetUserFromToken()
        {
            var result =
                await new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,
                    new GetUserByToken(
                        _services.GetService((typeof(IBus))) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                .Ask(new GetUserByIdRequestContract());
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IRegisterResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> Register(
            [FromBody] RegisterHttpBody body)
        {
            var result =
                await new LoggedPipeNode<IRegisterRequestContract, IRegisterResultContract>(
                    _services.GetService(typeof(ILogger<IRegisterRequestContract>)) as ILogger<IRegisterRequestContract>,
                    new Register(
                        _services.GetService((typeof(IBus))) as IBus))
                .Ask(new RegisterRequestContract()
                    {Email = body.Email,
                     NewUserId = body.NewId,
                     Password = body.Password,
                     Phone = body.Phone});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPost]
        [Route("Authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGenerateTokenResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateUserTokenHttpBody body)
        {
            var result =
                await new LoggedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(
                    _services.GetService(typeof(ILogger<IGenerateTokenRequestContract>)) as ILogger<IGenerateTokenRequestContract>,
                    new GenerateToken(
                        _services.GetService((typeof(IBus))) as IBus))
                .Ask(new GenerateTokenRequestContract() {Login = body.Login, Password = body.Password});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch]
        [Route("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdatePhoneResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhoneHttpBody body, Guid id)
        {
            var result =
                await new LoggedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>)) as ILogger<IUpdatePhoneRequestContract>,
                    new UpdatePhone(
                        _services.GetService((typeof(IBus))) as IBus))
                .Ask(new UpdatePhoneRequestContract() { UserId = id , Phone = body.Phone });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch]
        [Route("{id}/Password")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdatePasswordResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordHttpBody body, Guid id)
        {
            var result =
                await new LoggedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(
                    _services.GetService(typeof(ILogger<IUpdatePasswordRequestContract>)) as ILogger<IUpdatePasswordRequestContract>,
                    new UpdatePassword(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new UpdatePasswordRequestContract() { UserId =  id, Password = body.Password });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch]
        [Route("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdateNameResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameHttpBody body, Guid id)
        {
            var result =
                await new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateNameRequestContract>)) as ILogger<IUpdateNameRequestContract>,
                    new UpdateName(
                        _services.GetService((typeof(IBus))) as IBus))
                .Ask(new UpdateNameRequestContract() { UserId = id, Name = body.Name });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch]
        [Route("{id}/Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdateEmailResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))] 
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailHttpBody body, Guid id)
        {
            var result =
                await new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>( 
                    _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>)) as ILogger<IUpdateEmailRequestContract>,
                    new UpdateEmail(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new UpdateEmailRequestContract() { UserId = id, Email = body.Email });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}