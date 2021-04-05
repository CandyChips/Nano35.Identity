using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.Register
{
    public class ConvertedRegisterOnHttpContext : 
        PipeInConvert
        <RegisterHttpBody, 
            IActionResult,
            IRegisterRequestContract, 
            IRegisterResultContract>
    {
        public ConvertedRegisterOnHttpContext(
            IPipeNode<IRegisterRequestContract, IRegisterResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(RegisterHttpBody input)
        {
            var converted = new RegisterRequestContract()
            {
                NewUserId = input.NewId,
                Phone = input.Phone,
                Email = input.Email,
                Password = input.Password,
                PasswordConfirm = input.PasswordConfirm
            };
            return await DoNext(converted) switch
            {
                IRegisterSuccessResultContract success => new OkObjectResult(success),
                IRegisterErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}