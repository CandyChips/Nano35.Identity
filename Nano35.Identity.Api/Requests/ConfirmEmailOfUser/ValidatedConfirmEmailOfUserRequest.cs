﻿using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ValidatedConfirmEmailOfUserRequestErrorResult :
        IConfirmEmailOfUserErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedConfirmEmailOfUserRequest:
        IPipelineNode<
            IConfirmEmailOfUserRequestContract, 
            IConfirmEmailOfUserResultContract>
    {
        private readonly IValidator<IConfirmEmailOfUserRequestContract> _validator;
        
        private readonly IPipelineNode<
            IConfirmEmailOfUserRequestContract, 
            IConfirmEmailOfUserResultContract> _nextNode;
        
        public ValidatedConfirmEmailOfUserRequest(
            IValidator<IConfirmEmailOfUserRequestContract> validator,
            IPipelineNode<
                IConfirmEmailOfUserRequestContract,
                IConfirmEmailOfUserResultContract> nextNode)
        {   
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IConfirmEmailOfUserResultContract> Ask(
            IConfirmEmailOfUserRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);

            if (!result.IsValid)
            {
                return new ValidatedConfirmEmailOfUserRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}