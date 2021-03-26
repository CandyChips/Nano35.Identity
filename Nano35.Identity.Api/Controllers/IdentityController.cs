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
            var logger = (ILogger<LoggedGetUserByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByIdRequest>));
            var validator = (IValidator<IGetUserByIdRequestContract>) _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>));
            
            return await
                new ConvertedGetUserByIdOnHttpContext(
                new LoggedGetUserByIdRequest(logger,
                    new ValidatedGetUserByIdRequest(validator,
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
            var logger = (ILogger<LoggedGetAllUsersRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUsersRequest>));
            //var validator = (IValidator<IGetUserByIdRequestContract>) _services.GetService(typeof(IValidator<IGetUserByIdRequestContract>));

            return await
                new ConvertedGetAllUsersOnHttpContext(
                        new LoggedGetAllUsersRequest(logger,
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
            var logger = (ILogger<LoggedGetUserByTokenRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByTokenRequest>));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));

            return await
                new ConvertedGetUserByTokenOnHttpContext(
                new LoggedGetUserByTokenRequest(logger,
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
            var logger = (ILogger<LoggedRegisterRequest>) _services.GetService(typeof(ILogger<LoggedRegisterRequest>));
            var validator = (IValidator<IRegisterRequestContract>) _services.GetService(typeof(IValidator<IRegisterRequestContract>));
            
            return await 
                new ConvertedRegisterOnHttpContext(
                    new LoggedRegisterRequest(logger,
                        new ValidatedRegisterRequest(validator,
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
            var logger = (ILogger<LoggedGenerateTokenRequest>) _services.GetService(typeof(ILogger<LoggedGenerateTokenRequest>));
            var validator = (IValidator<IGenerateTokenRequestContract>) _services.GetService(typeof(IValidator<IGenerateTokenRequestContract>));
            
            return await 
                new ConvertedGenerateTokenOnHttpContext(
                    new LoggedGenerateTokenRequest(logger,
                        new ValidatedGenerateTokenRequest(validator, 
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
            var logger = (ILogger<LoggedUpdatePhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdatePhoneRequest>));
            var validator = (IValidator<IUpdatePhoneRequestContract>) _services.GetService(typeof(IValidator<IUpdatePhoneRequestContract>));

            
            return await
                new ConvertedUpdatePhoneOnHttpContext(
                        new LoggedUpdatePhoneRequest(logger,
                            new ValidatedUpdatePhoneRequest(validator,
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
            var logger = (ILogger<LoggedUpdatePasswordRequest>) _services.GetService(typeof(ILogger<LoggedUpdatePasswordRequest>));
            var validator = (IValidator<IUpdatePasswordRequestContract>) _services.GetService(typeof(IValidator<IUpdatePasswordRequestContract>));

            
            return await
                new ConvertedUpdatePasswordOnHttpContext(
                        new LoggedUpdatePasswordRequest(logger,
                            new ValidatedUpdatePasswordRequest(validator,
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
            var logger = (ILogger<LoggedUpdateNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateNameRequest>));
            var validator = (IValidator<IUpdateNameRequestContract>) _services.GetService(typeof(IValidator<IUpdateNameRequestContract>));

            return await
                new ConvertedUpdateNameOnHttpContext(
                        new LoggedUpdateNameRequest(logger,
                            new ValidatedUpdateNameRequest(validator,
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
            var logger = (ILogger<LoggedUpdateEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateEmailRequest>));
            var validator = (IValidator<IUpdateEmailRequestContract>) _services.GetService(typeof(IValidator<IUpdateEmailRequestContract>));

            return await
                new ConvertedUpdateEmailOnHttpContext(
                new LoggedUpdateEmailRequest(logger,
                        new ValidatedUpdateEmailRequest(validator,
                            new UpdateEmailUseCase(bus))))
                    .Ask(body);
        }
    }
}