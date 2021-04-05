using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class ConvertedGetUserByTokenOnHttpContext : 
        PipeInConvert
        <GetUserByIdRequestContract, 
            IActionResult,
            IGetUserByIdRequestContract, 
            IGetUserByIdResultContract>
    {
        public ConvertedGetUserByTokenOnHttpContext(
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetUserByIdRequestContract input)
        {
            return await DoNext(input) switch
            {
                IGetUserByIdSuccessResultContract success => new OkObjectResult(success),
                IGetUserByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}