using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            this._technologyRepository = technologyRepository;
        }

        public async Task TechnologyNameCanNotBeRepeated(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology Exists");
        }

        public async Task TechnologyIdShouldBeExist(int id)
        {
            var result = await _technologyRepository.GetListAsync(p => p.Id == id);
            if (!result.Items.Any()) throw new BusinessException("Technology Not Found.");
        }

        public async Task TechnologyShouldExistWhenRequested(Language language)
        {
            if (language == null) throw new BusinessException("Technology does not exist");
        }
    }
}
