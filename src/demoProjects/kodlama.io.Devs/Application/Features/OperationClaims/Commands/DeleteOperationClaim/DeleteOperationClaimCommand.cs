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

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { "Admin" };
        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<DeleteOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimShouldBeExistWhenRequested(request.Id);
                await _operationClaimBusinessRules.UserOperationClaimShouldNotBeExistWhenTryingToDeleteOperationClaim(request.Id);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);
                OperationClaim deleteOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);

                DeleteOperationClaimDto deleteOperationClaimDto = _mapper.Map<DeleteOperationClaimDto>(deleteOperationClaim);

                return deleteOperationClaimDto;
            }
        }
    }
}
