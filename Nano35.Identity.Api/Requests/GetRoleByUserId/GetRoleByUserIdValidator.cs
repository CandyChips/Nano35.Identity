﻿using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleByUserId
{
    public class ValidatedGetRoleByUserIdRequestErrorResult : IGetRoleByUserIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetRoleByUserIdValidator:
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        private readonly IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> _nextNode;

        public GetRoleByUserIdValidator(
            IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(
            IGetRoleByUserIdRequestContract input)
        {
            if (false)
            {
                return new ValidatedGetRoleByUserIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}