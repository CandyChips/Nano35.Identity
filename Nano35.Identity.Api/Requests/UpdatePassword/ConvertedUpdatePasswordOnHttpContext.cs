using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class ConvertedUpdatePasswordOnHttpContext : 
        PipeInConvert
        <UpdatePasswordHttpBody, 
            IActionResult,
            IUpdatePasswordRequestContract, 
            IUpdatePasswordResultContract>
    {
        public ConvertedUpdatePasswordOnHttpContext(
            IPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdatePasswordHttpBody input)
        {
            var converted = new UpdatePasswordRequestContract()
            {
                UserId = input.UserId,
                Password = input.Password,
            };
            return await DoNext(converted) switch
            {
                IUpdatePasswordSuccessResultContract success => new OkObjectResult(success),
                IUpdatePasswordErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}