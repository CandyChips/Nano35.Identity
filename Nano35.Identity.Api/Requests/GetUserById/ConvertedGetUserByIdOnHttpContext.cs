using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class ConvertedGetUserByIdOnHttpContext : 
        PipeInConvert
        <GetUserByIdHttpQuery, 
            IActionResult,
            IGetUserByIdRequestContract, 
            IGetUserByIdResultContract>
    {
        public ConvertedGetUserByIdOnHttpContext(
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetUserByIdHttpQuery input)
        {
            var converted = new GetUserByIdRequestContract()
            {
                UserId = input.Id
            };
            return await DoNext(converted) switch
            {
                IGetUserByIdSuccessResultContract success => new OkObjectResult(success),
                IGetUserByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}