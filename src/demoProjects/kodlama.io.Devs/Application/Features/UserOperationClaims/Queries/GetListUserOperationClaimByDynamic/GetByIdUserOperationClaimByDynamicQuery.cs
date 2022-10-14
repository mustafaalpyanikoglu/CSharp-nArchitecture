using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaimByDynamic
{
    public class GetByIdUserOperationClaimByDynamicQuery : IRequest<UserOperationClaimListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class GetByIdUserOperationClaimByDynamicQueryHandler : IRequestHandler<GetByIdUserOperationClaimByDynamicQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetByIdUserOperationClaimByDynamicQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UserOperationClaimListModel> Handle(GetByIdUserOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim>? userOperationClaims = await _userOperationClaimRepository.
                    GetListByDynamicAsync(request.Dynamic,
                                          include: x => x.Include(o => o.User).Include(u => u.OperationClaim),
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize,
                                          enableTracking: false);
                await _userOperationClaimBusinessRules.ShouldBeSomeDataInTheUserOperationClaimTableWhenRequested(userOperationClaims);

                UserOperationClaimListModel mapppedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);

                return mapppedUserOperationClaimListModel;
            }
        }
    }
}
