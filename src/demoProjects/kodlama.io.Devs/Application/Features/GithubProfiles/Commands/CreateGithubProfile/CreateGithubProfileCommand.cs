using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommand:IRequest<CreateGithubProfileDto>
    {
        public int UserId { get; set; }
        public string Github { get; set; }

        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreateGithubProfileDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public CreateGithubProfileCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<CreateGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _githubProfileBusinessRules.GithubAddressCanNotBeDuplicatedWhenInserted(request.Github);

                GithubProfile mappedGithubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile createdGithub = await _githubRepository.AddAsync(mappedGithubProfile);
                CreateGithubProfileDto createGithubProfileDto = _mapper.Map<CreateGithubProfileDto>(createdGithub);

                return createGithubProfileDto;
            }
        }
    }
}
