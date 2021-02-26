using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.Identity.Api.Helpers;
using Nano35.Identity.Api.Requests.GenerateToken;
using Nano35.Identity.Api.Requests.GetAllRoles;
using Nano35.Identity.Api.Requests.GetAllUsers;
using Nano35.Identity.Api.Requests.GetRoleById;
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
        public async Task<IActionResult> GetUserById(
            [FromQuery]GetUserByIdHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetUserByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByIdRequest>));
            var validator =
                (IValidator<IGetUserByIdRequestContract>) _services.GetService(
                    typeof(IValidator<IGetUserByIdRequestContract>));
            
            var result =
                await new LoggedGetUserByIdRequest(logger,
                        new ValidatedGetUserByIdRequest(validator,
                            new GetUserByIdRequest(bus)
                            )
                        ).Ask(message);

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetUserFromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetUserByTokenRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByTokenRequest>));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));

            var result =
                await new LoggedGetUserByTokenRequest(logger,
                    new GetUserByTokenRequest(bus, auth)
                ).Ask(new GetUserFromTokenHttpContext());

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody]RegisterHttpContextBody body,
            [FromHeader] RegisterHttpContextHeader head)
        {
            RegisterHttpContext message = new RegisterHttpContext(head, body);
            
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedRegisterRequest>) _services.GetService(typeof(ILogger<LoggedRegisterRequest>));
            var validator = (IValidator<IRegisterRequestContract>) _services.GetService(typeof(IValidator<IRegisterRequestContract>));
            
            var result =
                await new LoggedRegisterRequest(logger,
                        new ValidatedRegisterRequest(validator,
                            new RegisterRequest(bus))
                        ).Ask(message);
            
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
            [FromBody] GenerateUserTokenBody body)
        {
            var message = new GenerateUserTokenHttpContext(body);
            
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGenerateTokenRequest>) _services.GetService(typeof(ILogger<LoggedGenerateTokenRequest>));
            var validator = (IValidator<IGenerateTokenRequestContract>) _services.GetService(typeof(IValidator<IGenerateTokenRequestContract>));
            
            var result =
                await new LoggedGenerateTokenRequest(logger,
                        new ValidatedGenerateTokenRequest(validator,
                            new GenerateTokenRequest(bus)))
                    .Ask(message);
            return result switch
            {
                IGenerateTokenSuccessResultContract => Ok(result),
                IGenerateTokenErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdatePhone")]
        public async Task<IActionResult> UpdatePhone(
            [FromBody] UpdatePhoneHttpContext query)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedUpdatePhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdatePhoneRequest>));
            var validator = (IValidator<IUpdatePhoneRequestContract>) _services.GetService(typeof(IValidator<IUpdatePhoneRequestContract>));

            var result =
                await new LoggedUpdatePhoneRequest(logger,
                        new ValidatedUpdatePhoneRequest(validator,
                            new UpdatePhoneRequest(bus)))
                    .Ask(query);
            
            return result switch
            {
                IUpdatePhoneSuccessResultContract => Ok(result),
                IUpdatePhoneErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordHttpContext query)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedUpdatePasswordRequest>) _services.GetService(typeof(ILogger<LoggedUpdatePasswordRequest>));
            var validator = (IValidator<IUpdatePasswordRequestContract>) _services.GetService(typeof(IValidator<IUpdatePasswordRequestContract>));

            var result =
                await new LoggedUpdatePasswordRequest(logger,
                        new ValidatedUpdatePasswordRequest(validator,
                            new UpdatePasswordRequest(bus)))
                    .Ask(query);
            
            return result switch
            {
                IUpdatePasswordSuccessResultContract => Ok(result),
                IUpdatePasswordErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateName(
            [FromBody] UpdateNameHttpContext query)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedUpdateNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateNameRequest>));
            var validator = (IValidator<IUpdateNameRequestContract>) _services.GetService(typeof(IValidator<IUpdateNameRequestContract>));

            var result =
                await new LoggedUpdateNameRequest(logger,
                        new ValidatedUpdateNameRequest(validator,
                            new UpdateNameRequest(bus)))
                    .Ask(query);
            
            return result switch
            {
                IUpdateNameSuccessResultContract => Ok(result),
                IUpdateNameErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(
            [FromBody] UpdateEmailHttpContext query)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedUpdateEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateEmailRequest>));
            var validator = (IValidator<IUpdateEmailRequestContract>) _services.GetService(typeof(IValidator<IUpdateEmailRequestContract>));

            var result =
                await new LoggedUpdateEmailRequest(logger,
                        new ValidatedUpdateEmailRequest(validator,
                            new UpdateEmailRequest(bus)))
                    .Ask(query);
            
            return result switch
            {
                IUpdateEmailSuccessResultContract => Ok(result),
                IUpdateEmailErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
        
        //[HttpGet]
        //[Route("GetAllRoles")]
        //public async Task<IActionResult> GetAllRoles()
        //{
        //    var bus = (IBus) _services.GetService((typeof(IBus)));
        //    var logger = (ILogger<LoggedGetAllRolesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllRolesRequest>));
        //    
        //    var result =
        //        await new LoggedGetAllRolesRequest(logger,
        //                new ValidatedGetAllRolesRequest(
        //                    new GetAllRolesRequest(bus)
        //                    )
        //                ).Ask(new GetAllRolesHttpContext());
        //
        //    return result switch
        //    {
        //        IGetAllRolesSuccessResultContract => Ok(result),
        //        IGetAllRolesErrorResultContract => BadRequest(result),
        //        _ => BadRequest()
        //    };
        //}

        //[HttpGet]
        //[Route("GetRoleById")]
        //public async Task<IActionResult> GetRoleById(
        //    [FromQuery]GetRoleByIdHttpContext message)
        //{
        //    
        //    var bus = (IBus) _services.GetService((typeof(IBus)));
        //    var logger = (ILogger<LoggedGetRoleByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetRoleByIdRequest>));
        //    var validator =
        //        (IValidator<IGetRoleByIdRequestContract>) _services.GetService(
        //            typeof(IValidator<IGetRoleByIdRequestContract>));
        //    
        //    var result =
        //        await new LoggedGetRoleByIdRequest(logger,
        //                new ValidatedGetRoleByIdRequest(validator,
        //                     new GetRoleByIdRequest(bus)
        //                     )
        //                ).Ask(message);
        //
        //    return result switch
        //    {
        //        IGetRoleByIdSuccessResultContract => Ok(result),
        //        IGetRoleByIdErrorResultContract => BadRequest(result),
        //        _ => BadRequest()
        //    };
        //}
        //
    
        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>

        //[HttpGet]
        //[Route("GetAllUsers")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var bus = (IBus) _services.GetService((typeof(IBus)));
        //    var logger = (ILogger<LoggedGetAllUsersRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUsersRequest>));
        //    
        //    var result =
        //        await new LoggedGetAllUsersRequest(logger,
        //                new ValidatedGetAllUsersRequest(
        //                    new GetAllUsersRequest(bus)
        //                    )
        //                ).Ask(new GetAllUsersHttpContext());
        //
        //    return result switch
        //    {
        //        IGetAllUsersSuccessResultContract => Ok(result),
        //        IGetAllUsersErrorResultContract => BadRequest(result),
        //        _ => BadRequest()
        //    };
        //}
    }
}