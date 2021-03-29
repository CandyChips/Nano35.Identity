using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class ConvertedUpdatePhoneOnHttpContext : 
        PipeInConvert
        <UpdatePhoneHttpBody, 
            IActionResult,
            IUpdatePhoneRequestContract, 
            IUpdatePhoneResultContract>
    {
        public ConvertedUpdatePhoneOnHttpContext(IPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdatePhoneHttpBody input)
        {
            var converted = new UpdatePhoneRequestContract()
            {
                UserId = input.UserId,
                Phone = input.Phone
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdatePhoneSuccessResultContract success => new OkObjectResult(success),
                IUpdatePhoneErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}