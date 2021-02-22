﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdatePassword
{
    public class UpdatePasswordRequest :
        IPipelineNode<
            IUpdatePasswordRequestContract,
            IUpdatePasswordResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePasswordRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class UpdatePasswordSuccessResultContract : 
            IUpdatePasswordSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdatePasswordErrorResultContract
        {
            public string Error { get; set; }
        }

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}