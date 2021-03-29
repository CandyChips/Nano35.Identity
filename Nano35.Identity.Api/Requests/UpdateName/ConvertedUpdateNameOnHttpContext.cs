using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class ConvertedUpdateNameOnHttpContext : 
        PipeInConvert
        <UpdateNameHttpBody, 
            IActionResult,
            IUpdateNameRequestContract, 
            IUpdateNameResultContract>
    {
        public ConvertedUpdateNameOnHttpContext(IPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateNameHttpBody input)
        {
            var converted = new UpdateNameRequestContract()
            {
                UserId = input.UserId,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}