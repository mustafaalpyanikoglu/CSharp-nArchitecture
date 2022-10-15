using Application.Features.GithubProfiles.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Rules
{
    public class GithubProfileBusinessRules
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IUserRepository _userRepository;
        private readonly GithubProfileBusinessRulesMessages _messages;

        public GithubProfileBusinessRules(IGithubRepository githubRepository, IUserRepository userRepository, GithubProfileBusinessRulesMessages messages)
        {
            _githubRepository = githubRepository;
            _userRepository = userRepository;
            _messages = messages;
        }

        public async Task DeveloperShouldBeExistWithGivenUserId(int id)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == id);
            if (user == null) throw new BusinessException(_messages.IdDoesNotExist);
        }

        public async Task GithubAddressCanNotBeDuplicatedWhenInserted(string github)
        {
            IPaginate<GithubProfile> result = await _githubRepository.GetListAsync(g => g.Equals(github));
            if (result.Items.Any()) throw new BusinessException("Github account already exist.");
        }

        public void GithubAddressShouldExistWhenRequested(GithubProfile githubProfile)
        {
            if (githubProfile == null) throw new BusinessException("Requested Github acoount does not exist.");
        }

        public async Task GithubProfileShouldBeExistWhenRequested(int id)
        {
            GithubProfile? githubProfile = await _githubRepository.GetAsync(g => g.Id == id);
            if (githubProfile == null) throw new BusinessException("User Operation Claim does not exist in the system!");
        }
    }
}
