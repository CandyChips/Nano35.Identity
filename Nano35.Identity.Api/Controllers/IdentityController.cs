using System;
using System.Net;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;
using Nano35.Identity.Api.Requests;
using Nano35.Identity.Api.Requests.GenerateToken;
using Nano35.Identity.Api.Requests.GetAllRoles;
using Nano35.Identity.Api.Requests.GetAllUsers;
using Nano35.Identity.Api.Requests.GetRoleById;
using Nano35.Identity.Api.Requests.GetUserById;
using Nano35.Identity.Api.Requests.GetUserByToken;
using Nano35.Identity.Api.Requests.Register;

namespace Nano35.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public IdentityController(IServiceProvider  services)
        {
            _services = services;
        }
    
        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>

        public class GetAllUsersHttpContext :
            IGetAllUsersRequestContract
        {
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(
            [FromHeader] GetAllUsersHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetAllUsersRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUsersRequest>));
            
            var result =
                await new LoggedGetAllUsersRequest(logger,
                        new ValidatedGetAllUsersRequest(
                            new GetAllUsersRequest(bus)
                            )
                        ).Ask(message);

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
            [FromHeader] GetAllRolesHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetAllRolesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllRolesRequest>));
            
            var result =
                await new LoggedGetAllRolesRequest(logger,
                        new ValidatedGetAllRolesRequest(
                            new GetAllRolesRequest(bus)
                            )
                        ).Ask(message);

            return result switch
            {
                IGetAllRolesSuccessResultContract => Ok(result),
                IGetAllRolesErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

        public class GetUserByIdHttpContext : IGetUserByIdRequestContract
        {
            public Guid UserId { get; set; }
        }
        
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(
            [FromHeader]GetUserByIdHttpContext message)
        {
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetUserByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByIdRequest>));
            var requestLogger = (ILogger<GetUserByIdRequest>) _services.GetService(typeof(ILogger<GetUserByIdRequest>));
            
            var result =
                await new LoggedGetUserByIdRequest(logger,
                        new ValidatedGetUserByIdRequest(
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

        public class GetUserFromTokenHttpContext :
            IGetUserByIdRequestContract
        {
            public Guid UserId { get; set; }
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
                    new ValidatedGetUserByTokenRequest(
                        new GetUserByTokenRequest(bus, auth)
                    )
                ).Ask(new GetUserFromTokenHttpContext());

            return result switch
            {
                IGetUserByIdSuccessResultContract => Ok(result),
                IGetUserByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
        
        public class GetRoleByIdHttpContext : IGetRoleByIdRequestContract
        {
            public Guid RoleId { get; set; }
        }
        
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(
            [FromHeader]GetRoleByIdHttpContext message)
        {
            
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGetRoleByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetRoleByIdRequest>));
            
            
            var result =
                await new LoggedGetRoleByIdRequest(logger,
                        new ValidatedGetRoleByIdRequest(
                             new GetRoleByIdRequest(bus)
                             )
                        ).Ask(message);

            return result switch
            {
                IGetRoleByIdSuccessResultContract => Ok(result),
                IGetRoleByIdErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }

       
           
            public class RegisterHttpContext : IRegisterRequestContract
            {
                public Guid NewUserId { get; set; }
                public string Phone { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string PasswordConfirm { get; set; }
                
                public RegisterHttpContext(RegisterHttpContextHeader head, RegisterHttpContextBody body)
                {
                    NewUserId = head.NewUserId;
                    Phone = body.Phone;
                    Email = body.Email;
                    Password = body.Password;
                    PasswordConfirm = body.PasswordConfirm;
                }
            }
            
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
            
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody]RegisterHttpContextBody body,
            [FromHeader] RegisterHttpContextHeader head)
        {
            RegisterHttpContext message = new RegisterHttpContext(head, body);
            
            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedRegisterRequest>) _services.GetService(typeof(ILogger<LoggedRegisterRequest>));
            
            var result =
                await new LoggedRegisterRequest(logger,
                        new ValidatedRegisterRequest(
                            new RegisterRequest(bus)
                            )
                        ).Ask(message);
            return result switch
            {
                IRegisterSuccessResultContract => Ok(result),
                IRegisterErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
        }
        
        public class GenerateUserTokenHttpContext : IGenerateTokenRequestContract
        {
            public string Login { get; set; }
            public string Password { get; set; }
            
            public GenerateUserTokenHttpContext(GenerateUserTokenBody body)
            {
                Login = body.Login;
                Password = body.Password;
            }
        }
        
        public class GenerateUserTokenBody
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> GenerateUserToken(
            [FromBody] GenerateUserTokenBody body)
        {
            GenerateUserTokenHttpContext message = new GenerateUserTokenHttpContext(body);

            var bus = (IBus) _services.GetService((typeof(IBus)));
            var logger = (ILogger<LoggedGenerateTokenRequest>) _services.GetService(typeof(ILogger<LoggedGenerateTokenRequest>));
            
            var result =
                await new LoggedGenerateTokenRequest(logger,
                        new ValidatedGenerateTokenRequest(
                            new GenerateTokenRequest(bus)
                            )
                        )
                    .Ask(message);
            return result switch
            {
                IGenerateTokenSuccessResultContract => Ok(result),
                IGenerateTokenErrorResultContract => BadRequest(result),
                _ => BadRequest()
            };
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