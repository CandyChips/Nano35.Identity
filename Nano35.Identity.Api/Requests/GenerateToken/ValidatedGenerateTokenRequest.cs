using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class ValidatedGenerateTokenRequestErrorResult : IGenerateTokenErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGenerateTokenRequest:
        IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IValidator<IGenerateTokenRequestContract> _validator;
        private readonly IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> _nextNode;
        
        public ValidatedGenerateTokenRequest(
            IValidator<IGenerateTokenRequestContract> validator,
            IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> nextNode)
        {   
            _validator = validator;
            _nextNode = nextNode;
            
        }

        public class GenerateTokenContext :
            IGenerateTokenRequestContract
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);

            if (!result.IsValid)
            {
                return new ValidatedGenerateTokenRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
    public class GenerateTokenRequestValidator :
        AbstractValidator<IGenerateTokenRequestContract> 
    {
        public GenerateTokenRequestValidator()
        {
            RuleFor(token => token.Login).NotEmpty().WithMessage("лОГИН ПУСТОЙ БЛИН");
            RuleFor(token => token.Password).NotEmpty().WithMessage("ПАРОЛЬ ПУСТОЙ БЛЯДЬ");
        }
    } 
}