using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery:IRequest<GetByIdOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<GetByIdOperationClaimDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimShouldBeExistWhenRequested(request.Id);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);
                GetByIdOperationClaimDto getByIdOperationClaimDto = _mapper.Map<GetByIdOperationClaimDto>(operationClaim);

                return getByIdOperationClaimDto;
            }
        }
    }
}
