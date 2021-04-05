using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.HttpContext.identity;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class ConvertedGenerateTokenOnHttpContext : 
        PipeInConvert
        <GenerateUserTokenHttpBody, 
            IActionResult,
            IGenerateTokenRequestContract, 
            IGenerateTokenResultContract>
    {
        public ConvertedGenerateTokenOnHttpContext(
            IPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GenerateUserTokenHttpBody input)
        {
            var converted = new GenerateTokenRequestContract()
            {
                Login = input
                    .With(x => x.Login)
                    .If(x => x != "")
                    .Do(Console.WriteLine),
                Password = input
                    .With(x => x.Password)
                    .If(x => x != "")
                    .Do(Console.WriteLine)
            };
            return await DoNext(converted) switch
            {
                IGenerateTokenSuccessResultContract success => new OkObjectResult(success),
                IGenerateTokenErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}