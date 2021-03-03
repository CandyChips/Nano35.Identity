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
            var validator =
                (IValidator<IGetUserByIdRequestContract>) _services.GetService(
                    typeof(IValidator<IGetUserByIdRequestContract>));

            var request = new GetUserByIdRequestContract()
            {
                UserId = message.UserId
            };
            
            var result = 
                await new LoggedGetUserByIdRequest(logger,
                    new ValidatedGetUserByIdRequest(validator,
                        new GetUserByIdRequest(bus))
                ).Ask(request);

            return result switch
            {
                IGetUserByIdSuccessResultContract success => Ok(success),
                IGetUserByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            var validator =
                (IValidator<IGetUserByIdRequestContract>) _services.GetService(
                    typeof(IValidator<IGetUserByIdRequestContract>));

            var request = new GetAllUsersRequestContract();
            
            var result = 
                await new LoggedGetAllUsersRequest(logger,
                    new GetAllUsersRequest(bus))
                    .Ask(request);

            return result switch
            {
                IGetAllUsersSuccessResultContract success => Ok(success),
                IGetAllUsersErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var result =
                await new LoggedGetUserByTokenRequest(logger,
                    new GetUserByTokenRequest(bus, auth))
                    .Ask(new GetUserByIdRequestContract());

            return result switch
            {
                IGetUserByIdSuccessResultContract success => Ok(success),
                IGetUserByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("Register")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IRegisterSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IRegisterErrorResultContract))] 
        public async Task<IActionResult> Register(
            [FromBody] RegisterHttpBody body,
            [FromHeader] RegisterHttpHeader head)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedRegisterRequest>) _services.GetService(typeof(ILogger<LoggedRegisterRequest>));
            var validator = (IValidator<IRegisterRequestContract>) _services.GetService(typeof(IValidator<IRegisterRequestContract>));

            var request = new RegisterRequestContract()
            {
                NewUserId = head.NewId,
                Phone = body.Phone,
                Email = body.Email,
                Password = body.Password,
                PasswordConfirm = body.PasswordConfirm
            };
                
            var result =
                await new LoggedRegisterRequest(logger,
                        new ValidatedRegisterRequest(validator,
                            new RegisterRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IRegisterSuccessResultContract success => Ok(success),
                IRegisterErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
            
            var request = new GenerateTokenRequestContract()
            {
                Login = body.Login,
                Password = body.Password
            };
            
            var result =
                await new LoggedGenerateTokenRequest(logger,
                        new ValidatedGenerateTokenRequest(validator,
                            new GenerateTokenRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGenerateTokenSuccessResultContract success => Ok(success),
                IGenerateTokenErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdatePhoneRequestContract()
            {
                UserId = body.UserId,
                Phone = body.Phone
            };
            
            var result =
                await new LoggedUpdatePhoneRequest(logger,
                        new ValidatedUpdatePhoneRequest(validator,
                            new UpdatePhoneRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdatePhoneSuccessResultContract success => Ok(success),
                IUpdatePhoneErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdatePasswordRequestContract()
            {
                UserId = body.UserId,
                Password = body.Password
            };
            
            var result =
                await new LoggedUpdatePasswordRequest(logger,
                        new ValidatedUpdatePasswordRequest(validator,
                            new UpdatePasswordRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdatePasswordSuccessResultContract success => Ok(success),
                IUpdatePasswordErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateNameRequestContract()
            {
                UserId = body.UserId,
                Name = body.Name
            };
            
            var result =
                await new LoggedUpdateNameRequest(logger,
                        new ValidatedUpdateNameRequest(validator,
                            new UpdateNameRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateNameSuccessResultContract success => Ok(success),
                IUpdateNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateEmailRequestContract()
            {
                UserId = body.UserId,
                Email = body.Email
            };
            
            var result =
                await new LoggedUpdateEmailRequest(logger,
                        new ValidatedUpdateEmailRequest(validator,
                            new UpdateEmailRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateEmailSuccessResultContract success => Ok(success),
                IUpdateEmailErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}