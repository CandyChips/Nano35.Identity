using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class ConvertedGetAllUsersOnHttpContext : 
        PipeInConvert
        <GetAllUsersHttpQuery, 
            IActionResult,
            IGetAllUsersRequestContract, 
            IGetAllUsersResultContract>
    {
        public ConvertedGetAllUsersOnHttpContext(
            IPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllUsersHttpQuery input)
        {
            var converted = new GetAllUsersRequestContract();
            return await DoNext(converted) switch
            {
                IGetAllUsersSuccessResultContract success => new OkObjectResult(success),
                IGetAllUsersErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}