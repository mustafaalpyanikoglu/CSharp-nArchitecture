using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
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

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand:IRequest<UpdateTechnologyDto>
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdateTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdateTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                //await _technologyBusinessRules.TechnologyNameCanNotBeRepeated(request.Name);
                //await _technologyBusinessRules.TechnologyIdShouldBeExist(request.Id);

                Technology mapperTechnology = _mapper.Map<Technology>(request);
                Technology updateTechnology = await _technologyRepository.UpdateAsync(mapperTechnology);
                UpdateTechnologyDto updateTechnologyDto = _mapper.Map<UpdateTechnologyDto>(updateTechnology);

                return updateTechnologyDto;
            }
        }
    }
}
