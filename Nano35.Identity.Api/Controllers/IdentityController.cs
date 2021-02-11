using System;
using System.Net;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Requests;
using Nano35.Identity.Api.Requests.GenerateToken;
using Nano35.Identity.Api.Requests.GetAllRoles;
using Nano35.Identity.Api.Requests.GetAllUsers;
using Nano35.Identity.Api.Requests.GetRoleById;
using Nano35.Identity.Api.Requests.GetUserById;
using Nano35.Identity.Api.Requests.Register;

namespace Nano35.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IMediator _mediator;
        private readonly IServiceProvider  _services;

        public IdentityController(
            ILogger<IdentityController> logger,
            IMediator mediator, 
            IServiceProvider services)
        {
            _logger = logger;
            _mediator = mediator;
            _services = services;
        }

        public class GetAllusersHttpContext :
            IGetAllUsersRequestContract
        {
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(
            [FromBody] GetAllusersHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<GetAllUsersLogger>) _services.GetService(typeof(ILogger<GetAllUsersLogger>));
            var requestLogger = (ILogger<GetAllUsersRequest>) _services.GetService(typeof(ILogger<GetAllUsersRequest>));
            
            var result =
                await new GetAllUsersLogger(logger,
                        new GetAllUsersValidator(
                            new GetAllUsersRequest(bus, requestLogger)))
                    .Ask(message);

            return result switch
            {
                IGetAllUsersSuccessResultContract => Ok(result),
                IGetAllUsersErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        public class GetAllRolesHttpContext :
            IGetAllRolesRequestContract
        {
        }
        
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(
            [FromBody] GetAllRolesHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<GetAllRolesLogger>) _services.GetService(typeof(ILogger<GetAllRolesLogger>));
            var requestLogger = (ILogger<GetAllRolesRequest>) _services.GetService(typeof(ILogger<GetAllRolesRequest>));
            
            var result =
                await new GetAllRolesLogger(logger,
                        new GetAllRolesValidator(
                            new GetAllRolesRequest(bus, requestLogger)))
                    .Ask(message);

            return result switch
            {
                IGetAllRolesSuccessResultContract => Ok(result),
                IGetAllRolesErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        public class GetUserByIdHttpContext :
            IGetUserByIdRequestContract
        {
            [FromHeader]
            public Guid UserId { get; set; }
        }
        
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(
            GetUserByIdHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<GetUserByIdLogger>) _services.GetService(typeof(ILogger<GetUserByIdLogger>));
            var requestLogger = (ILogger<GetUserByIdRequest>) _services.GetService(typeof(ILogger<GetUserByIdRequest>));
            
            var result =
                await new GetUserByIdLogger(logger,
                        new GetUserByIdValidator(
                            new GetUserByIdRequest(bus, requestLogger)))
                    .Ask(message);

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        public class GetUserFromTokenHttpContext
        {
            [FromHeader]
            public string Token { get; set; }
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
        
        public class GetRoleByIdHttpContext :
            IGetRoleByIdRequestContract
        {
            [FromHeader]
            public Guid RoleId { get; set; }
        }
        
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(
            GetRoleByIdHttpContext message)
        {
            
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var requestLogger = (ILogger<GetRoleByIdRequest>) _services.GetService(typeof(ILogger<GetRoleByIdRequest>));
            var logger = (ILogger<GetRoleByIdLogger>) _services.GetService(typeof(ILogger<GetRoleByIdLogger>));
            
            
            var result =
                await new GetRoleByIdLogger(logger,
                        new GetRoleByIdValidator(
                             new GetRoleByIdRequest(bus, requestLogger)))
                    .Ask(message);

            return result switch
            {
                IGetRoleByIdSuccessResultContract => Ok(result),
                IGetRoleByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        public class RegisterHttpContext
        {
            public class RegisterHttpContextHeader
            {
                public Guid NewUserId { get; set; }
            }
            
            public class RegisterHttpContextBody
            {
                public string Phone { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string PasswordConfirm { get; set; }
            }
            [FromHeader]
            public RegisterHttpContextHeader Head { get; set; }
            [FromBody]
            public RegisterHttpContextBody Bod { get; set; }
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            RegisterHttpContext message)
        {

            
            //var bus = (IBus) _services.GetService((typeof(IBus)));
            //var requestLogger = (ILogger<RegisterRequest>) _services.GetService(typeof(ILogger<RegisterRequest>));
            //var logger = (ILogger<RegisterLogger>) _services.GetService(typeof(ILogger<RegisterLogger>));
            //
            //var result =
            //    await new RegisterLogger(logger,
            //            new RegisterValidator(
            //                new RegisterRequest(bus, requestLogger)))
            //        .Ask(message);
            //return result switch
            //{
            //    IRegisterSuccessResultContract => Ok(result),
            //    IRegisterErrorResultContract => BadRequest(result),
            //    _ => BadRequest()
            //};
            return Ok();
        }

        public class GenerateUserTokenHttpContext
        {
            public class GenerateUserTokenHttpContextHeader
            {
                public string Login { get; set; } 
            }

            public class GenerateUserTokenHttpContextBody
            {
                public string Login { get; set; } 
                public string Password { get; set; }
            }
            
            [FromHeader]
            public GenerateUserTokenHttpContextHeader Head { get; set; }
            [FromBody]
            public GenerateUserTokenHttpContextBody Bod { get; set; }
        }

        //public class GenerateUserTokenHttpContext: IGenerateTokenRequestContract
        //{
        //    public string Login { get; set; }
        //    public string Password { get; set; }
        //}
        
            
        
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> GenerateUserToken(
            GenerateUserTokenHttpContext message)
        {
           // var bus = (IBus) _services.GetService((typeof(IBus)));
           // var requestLogger = (ILogger<GenerateTokenRequest>) _services.GetService(typeof(ILogger<GenerateTokenRequest>));
           // var logger = (ILogger<GenerateTokenLogger>) _services.GetService(typeof(ILogger<GenerateTokenLogger>));
           // 
           // var result =
           //     await new GenerateTokenLogger(logger,
           //             new GenerateTokenValidator(
           //                 new GenerateTokenRequest(bus, requestLogger)))
           //         .Ask(message);
//
           // return result switch
           // {
           //     IGenerateTokenSuccessResultContract => Ok(result),
           //     IGenerateTokenErrorResultContract => BadRequest(result),
           //     _ => BadRequest()
           // };
            return Ok();
        }

        //[HttpPut]
        //[Route("UpdatePhone")]
        //public async Task<IActionResult> UpdatePhone(
        //    [FromBody] UpdatePhoneQuery message)
        //{
        //    var result = await this._mediator.Send(message);
        //    
        //    return result switch
        //    {
        //        IUpdatePhoneSuccessResultContract => Ok(result),
        //        IUpdatePhoneErrorResultContract => BadRequest(result),
        //        _ => BadRequest()
        //    };
        //}
//
        //[HttpPut]
        //[Route("UpdatePassword")]
        //public async Task<IActionResult> UpdatePassword(
        //    [FromBody] UpdatePasswordQuery message)
        //{
        //    var result = await this._mediator.Send(message);
        //    
        //    return result switch
        //    {
        //        IUpdatePasswordSuccessResultContract => Ok(result),
        //        IUpdatePasswordErrorResultContract => BadRequest(result),
        //        _ => BadRequest()
        //    };
        //}
    }
}