using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaimByDynmaic
{
    public class GetListOperationClaimByDynamicQuery : IRequest<OperationClaimListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class GetListOperationClaimByDynmaicQueryHandler : IRequestHandler<GetListOperationClaimByDynamicQuery, OperationClaimListModel>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetListOperationClaimByDynmaicQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListByDynamicAsync(
                    request.Dynamic, index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                await _operationClaimBusinessRules.ShouldBeSomeDataInTheOperationClaimTableWhenRequested(operationClaims);

                OperationClaimListModel mappedOperationClaimModels = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimModels;
            }
        }
    }
}
