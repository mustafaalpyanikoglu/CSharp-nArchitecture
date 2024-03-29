﻿using Application.Features.Languages.Dtos;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand:IRequest<CreateTechnologyDto>
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository? technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreateTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeRepeated(request.Name);
                await _technologyBusinessRules.TheLanguageYouWantToAddTechnologyToMustExist(request.LanguageId);

                Technology mappedTechnology = _mapper.Map<Technology>(request);
                Technology createTechnology = await _technologyRepository.AddAsync(mappedTechnology);
                CreateTechnologyDto createTechnologyDto = _mapper.Map<CreateTechnologyDto>(createTechnology);

                return createTechnologyDto;
            }
        }
    }
}
