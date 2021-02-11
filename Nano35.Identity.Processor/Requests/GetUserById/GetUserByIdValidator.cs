﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetUserById
{
    public class GetUserByIdValidatorErrorResult : IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetUserByIdValidator:
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> _nextNode;

        public GetUserByIdValidator(
            IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUserByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}