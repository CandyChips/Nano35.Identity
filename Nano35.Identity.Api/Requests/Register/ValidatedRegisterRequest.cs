using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class ValidatedRegisterRequestErrorResult : IRegisterErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedRegisterRequest:
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IValidator<IRegisterRequestContract> _validator;
        private readonly IPipelineNode<IRegisterRequestContract, IRegisterResultContract> _nextNode;

        public ValidatedRegisterRequest(
            IValidator<IRegisterRequestContract> validator,
            IPipelineNode<IRegisterRequestContract, IRegisterResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedRegisterRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
    public class RegisterRequestValidator :
        AbstractValidator<IRegisterRequestContract>
    {

        public RegisterRequestValidator()
        {
            RuleFor(request => request.NewUserId).NotEmpty().WithMessage("userId нет");
            RuleFor(request => request.Phone).NotEmpty().WithMessage("нет номера телефона");
            RuleFor(request => request.Email).NotEmpty().WithMessage("почта проебалась");
            RuleFor(request => request.Password).NotEmpty().WithMessage("пароли для лохов");
            RuleFor(request => request.PasswordConfirm).NotEmpty().Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}