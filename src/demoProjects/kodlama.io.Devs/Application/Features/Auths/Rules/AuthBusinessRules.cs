using Application.Features.Auths.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRulesMessages _authBusinessRulesMessages;

        public AuthBusinessRules(IUserRepository userRepository, AuthBusinessRulesMessages authBusinessRulesMessages)
        {
            _userRepository = userRepository;
            _authBusinessRulesMessages = authBusinessRulesMessages;
        }

        public async Task EmailShouldNotBeAlreadyExistWhenDeveloperRegister(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null) throw new BusinessException(_authBusinessRulesMessages.EmailAlreadyExist);
        }

        public async Task OperationClaimShouldBeExistAfterDeveloperCreated(OperationClaim operationClaim)
        {
            if (operationClaim == null) throw new BusinessException(_authBusinessRulesMessages.OperationClaimDoesNotExist);
        }
    }
}
