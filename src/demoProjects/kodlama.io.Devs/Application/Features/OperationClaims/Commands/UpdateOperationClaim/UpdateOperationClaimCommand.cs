﻿using Application.Features.OperationClaims.Dtos;
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

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand : IRequest<UpdateOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdateOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdateOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimShouldBeExistWhenRequested(request.Id);
                await _operationClaimBusinessRules.OperationClaimNameCanNotBeDuplicatedWhenInsertedOrUpdated(request.Name);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o=>o.Id == request.Id);
                _mapper.Map(request, operationClaim);
                OperationClaim updateOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
                UpdateOperationClaimDto updateOperationClaimDto = _mapper.Map<UpdateOperationClaimDto>(updateOperationClaim);

                return updateOperationClaimDto;
            }
        }
    }
}
