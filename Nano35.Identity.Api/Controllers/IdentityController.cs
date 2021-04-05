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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IGetUserByIdRequestContract>) _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>));
            var validator = (IValidator<IGetUserByIdRequestContract>) _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>));
            
            return await
                new ConvertedGetUserByIdOnHttpContext(
                new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(logger,
                    new ValidatedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(validator,
                        new GetUserByIdUseCase(bus)))
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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IGetAllUsersRequestContract>) _services.GetService(typeof(ILogger<IGetAllUsersRequestContract>));
            //var validator = (IValidator<IGetUserByIdRequestContract>) _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>));

            return await
                new ConvertedGetAllUsersOnHttpContext(
                    new LoggedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(logger,
                        new GetAllUsersUseCase(bus)))
                    .Ask(message);
        }

        [HttpGet]
        [Route("GetUserFromToken")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGetUserByIdSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGetUserByIdErrorResultContract))] 
        public async Task<IActionResult> GetUserFromToken()
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IGetUserByIdRequestContract>) _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));

            return await
                new ConvertedGetUserByTokenOnHttpContext(
                new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(logger,
                    new GetUserByTokenUseCase(bus, auth)))
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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IRegisterRequestContract>) _services.GetService(typeof(ILogger<IRegisterRequestContract>));
            var validator = (IValidator<IRegisterRequestContract>) _services.GetService(typeof(IValidator<IRegisterRequestContract>));
            
            return await 
                new ConvertedRegisterOnHttpContext(
                    new LoggedPipeNode<IRegisterRequestContract, IRegisterResultContract>(logger,
                        new ValidatedPipeNode<IRegisterRequestContract, IRegisterResultContract>(validator,
                            new RegisterUseCase(bus)))).Ask(body);
        }
        
        [HttpPost]
        [Route("Authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IGenerateTokenSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGenerateTokenErrorResultContract))] 
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateUserTokenHttpBody body)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IGenerateTokenRequestContract>) _services.GetService(typeof(ILogger<IGenerateTokenRequestContract>));
            var validator = (IValidator<IGenerateTokenRequestContract>) _services.GetService(typeof(IValidator<IGenerateTokenRequestContract>));
            
            return await 
                new ConvertedGenerateTokenOnHttpContext(
                    new LoggedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(logger,
                        new ValidatedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(validator, 
                            new GenerateTokenUseCase(bus)))).Ask(body);
        }

        [HttpPatch]
        [Route("UpdatePhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUpdatePhoneSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IUpdatePhoneErrorResultContract))] 
        public async Task<IActionResult> UpdatePhone(
            [FromBody] UpdatePhoneHttpBody body)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IUpdatePhoneRequestContract>) _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>));
            var validator = (IValidator<IUpdatePhoneRequestContract>) _services.GetService(typeof(IValidator<IUpdatePhoneRequestContract>));

            
            return await
                new ConvertedUpdatePhoneOnHttpContext(
                        new LoggedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(logger,
                            new ValidatedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(validator,
                                new UpdatePhoneUseCase(bus))))
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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IUpdatePasswordRequestContract>) _services.GetService(typeof(ILogger<IUpdatePasswordRequestContract>));
            var validator = (IValidator<IUpdatePasswordRequestContract>) _services.GetService(typeof(IValidator<IUpdatePasswordRequestContract>));

            
            return await
                new ConvertedUpdatePasswordOnHttpContext(
                        new LoggedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(logger,
                            new ValidatedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(validator,
                                new UpdatePasswordUseCase(bus))))
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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IUpdateNameRequestContract>) _services.GetService(typeof(ILogger<IUpdateNameRequestContract>));
            var validator = (IValidator<IUpdateNameRequestContract>) _services.GetService(typeof(IValidator<IUpdateNameRequestContract>));

            return await
                new ConvertedUpdateNameOnHttpContext(
                        new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(logger,
                            new ValidatedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(validator,
                                new UpdateNameUseCase(bus))))
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
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<IUpdateEmailRequestContract>) _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>));
            var validator = (IValidator<IUpdateEmailRequestContract>) _services.GetService(typeof(IValidator<IUpdateEmailRequestContract>));

            return await
                new ConvertedUpdateEmailOnHttpContext(
                new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(logger,
                        new ValidatedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(validator,
                            new UpdateEmailUseCase(bus))))
                    .Ask(body);
        }
    }
}