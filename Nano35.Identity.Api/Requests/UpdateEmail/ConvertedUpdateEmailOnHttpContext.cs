using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class ConvertedUpdateEmailOnHttpContext : 
        PipeInConvert
        <UpdateEmailHttpBody, 
            IActionResult,
            IUpdateEmailRequestContract, 
            IUpdateEmailResultContract>
    {
        public ConvertedUpdateEmailOnHttpContext(
            IPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateEmailHttpBody input)
        {
            var converted = new UpdateEmailRequestContract()
            {
                UserId = input.UserId,
                Email = input.Email
            };
            return await DoNext(converted) switch
            {
                IUpdateEmailSuccessResultContract success => new OkObjectResult(success),
                IUpdateEmailErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}