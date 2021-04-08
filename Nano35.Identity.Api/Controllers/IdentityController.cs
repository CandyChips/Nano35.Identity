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
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        public IdentityController(IServiceProvider  services)
        {
            _services = services;
        }
        
        [HttpGet]
        [Route("GetUserById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetUserByIdSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGetUserByIdErrorResultContract))]    
        public async Task<IActionResult> GetUserById(
            [FromQuery]GetUserByIdHttpQuery message)
        {
            return await
                new ConvertedGetUserByIdOnHttpContext(
                new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,
                    new ValidatedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                        _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>)) as IValidator<IGetUserByIdRequestContract>,
                        new GetUserByIdUseCase(_services.GetService(typeof(IBus)) as IBus)))
                ).Ask(message);
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetAllUsersSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGetAllUsersErrorResultContract))]    
        public async Task<IActionResult> GetAllUsers(
            [FromQuery]GetAllUsersHttpQuery message)
        {
            return await
                new ConvertedGetAllUsersOnHttpContext(
                        new LoggedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllUsersRequestContract>)) as ILogger<IGetAllUsersRequestContract>,
                            new ValidatedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(
                                _services.GetService(typeof(IValidator<IGetAllUsersRequestContract>)) as IValidator<IGetAllUsersRequestContract>,
                                    new GetAllUsersUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(message);
        }

        [HttpGet]
        [Route("GetUserFromToken")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetUserByIdSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGetUserByIdErrorResultContract))] 
        public async Task<IActionResult> GetUserFromToken()
        {
            return await
                new ConvertedGetUserByTokenOnHttpContext(
                new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,
                    new ValidatedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                        _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>)) as IValidator<IGetUserByIdRequestContract>,
                        new GetUserByTokenUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                    .Ask(new GetUserByIdRequestContract());
        }
        
        [HttpPost]
        [Route("Register")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IRegisterSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IRegisterErrorResultContract))] 
        public async Task<IActionResult> Register(
            [FromBody] RegisterHttpBody body)
        {
            return await 
                new ConvertedRegisterOnHttpContext(
                    new LoggedPipeNode<IRegisterRequestContract, IRegisterResultContract>(
                        _services.GetService(typeof(ILogger<IRegisterRequestContract>)) as ILogger<IRegisterRequestContract>,
                        new ValidatedPipeNode<IRegisterRequestContract, IRegisterResultContract>(
                            _services.GetService(typeof(IValidator<IRegisterRequestContract>)) as IValidator<IRegisterRequestContract>,
                            new RegisterUseCase(_services.GetService(typeof(IBus)) as IBus)))).Ask(body);
        }
        
        [HttpPost]
        [Route("Authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGenerateTokenSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGenerateTokenErrorResultContract))] 
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateUserTokenHttpBody body)
        {
            return await 
                new ConvertedGenerateTokenOnHttpContext(
                    new LoggedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(
                        _services.GetService(typeof(ILogger<IGenerateTokenRequestContract>)) as ILogger<IGenerateTokenRequestContract>,
                        new ValidatedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(
                            _services.GetService(typeof(IValidator<IGenerateTokenRequestContract>)) as IValidator<IGenerateTokenRequestContract>, 
                            new GenerateTokenUseCase(_services.GetService(typeof(IBus)) as IBus)))).Ask(body);
        }

        [HttpPatch]
        [Route("UpdatePhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdatePhoneSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IUpdatePhoneErrorResultContract))] 
        public async Task<IActionResult> UpdatePhone(
            [FromBody] UpdatePhoneHttpBody body)
        {
            return await
                new ConvertedUpdatePhoneOnHttpContext(
                        new LoggedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(
                            _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>)) as ILogger<IUpdatePhoneRequestContract>,
                            new ValidatedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>( 
                                _services.GetService(typeof(IValidator<IUpdatePhoneRequestContract>)) as IValidator<IUpdatePhoneRequestContract>,
                                new UpdatePhoneUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpPatch]
        [Route("UpdatePassword")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdatePasswordSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IUpdatePasswordErrorResultContract))] 
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordHttpBody body)
        {
            return await
                new ConvertedUpdatePasswordOnHttpContext(
                        new LoggedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(
                            _services.GetService(typeof(ILogger<IUpdatePasswordRequestContract>)) as ILogger<IUpdatePasswordRequestContract>,
                            new ValidatedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(
                                _services.GetService(typeof(IValidator<IUpdatePasswordRequestContract>)) as IValidator<IUpdatePasswordRequestContract>,
                                new UpdatePasswordUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpPatch]
        [Route("UpdateName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdateNameSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IUpdateNameErrorResultContract))] 
        public async Task<IActionResult> UpdateName(
            [FromBody] UpdateNameHttpBody body)
        {
            return await
                new ConvertedUpdateNameOnHttpContext(
                        new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateNameRequestContract>)) as ILogger<IUpdateNameRequestContract>,
                            new ValidatedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(
                                _services.GetService(typeof(IValidator<IUpdateNameRequestContract>)) as IValidator<IUpdateNameRequestContract>,
                                new UpdateNameUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpPatch]
        [Route("UpdateEmail")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdateEmailSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IUpdateEmailErrorResultContract))] 
        public async Task<IActionResult> UpdateEmail(
            [FromBody] UpdateEmailHttpBody body)
        {
            return await
                new ConvertedUpdateEmailOnHttpContext(
                new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>)) as ILogger<IUpdateEmailRequestContract>,
                        new ValidatedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(
                            _services.GetService(typeof(IValidator<IUpdateEmailRequestContract>)) as IValidator<IUpdateEmailRequestContract>,
                            new UpdateEmailUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
    }
}