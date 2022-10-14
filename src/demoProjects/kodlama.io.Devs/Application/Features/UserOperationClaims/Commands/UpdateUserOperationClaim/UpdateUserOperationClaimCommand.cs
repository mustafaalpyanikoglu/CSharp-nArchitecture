using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
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

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdateUserOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UpdateUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimShouldBeExistWhenRequested(request.Id);
                await _userOperationClaimBusinessRules.OperationClaimShouldBeExistWhenRequested(request.OperationClaimId);
                await _userOperationClaimBusinessRules.UserShouldBeExistWhenRequested(request.UserId);
                await _userOperationClaimBusinessRules.UserOperationClaimCanNotBeDuplicatedWhenInsertedOrUpdated(request.UserId, request.OperationClaimId);

                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(u => u.Id == request.Id);
                _mapper.Map(request, userOperationClaim);
                UserOperationClaim updateUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UpdateUserOperationClaimDto updateUserOperationClaimDto = _mapper.Map<UpdateUserOperationClaimDto>(updateUserOperationClaim);

                return updateUserOperationClaimDto;
            }
        }
    }
}
