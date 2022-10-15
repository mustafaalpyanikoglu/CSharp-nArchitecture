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

namespace Application.Features.GithubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommand:IRequest<DeleteGithubProfileDto>
    {
        public int Id { get; set; }

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand, DeleteGithubProfileDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public DeleteGithubProfileCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<DeleteGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _githubProfileBusinessRules.GithubProfileShouldBeExistWhenRequested(request.Id);

                GithubProfile mappedGithubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile deleteGithubProfile = await _githubRepository.DeleteAsync(mappedGithubProfile);
                DeleteGithubProfileDto deleteGithubProfileDto = _mapper.Map<DeleteGithubProfileDto>(deleteGithubProfile);

                return deleteGithubProfileDto;
            }
        }
    }
}
