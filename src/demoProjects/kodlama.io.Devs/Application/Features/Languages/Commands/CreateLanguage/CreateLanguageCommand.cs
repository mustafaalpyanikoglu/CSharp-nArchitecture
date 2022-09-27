using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand:IRequest<CreateLanguageDto>,ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreateLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<CreateLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageNameCanNotBeRepeated(request.Name);

                Language mappedLanguage = _mapper.Map<Language>(request);
                Language createLanguage = await _languageRepository.AddAsync(mappedLanguage);
                CreateLanguageDto createLanguageDto = _mapper.Map<CreateLanguageDto>(createLanguage);

                return createLanguageDto;
            }
        }
    }
}
