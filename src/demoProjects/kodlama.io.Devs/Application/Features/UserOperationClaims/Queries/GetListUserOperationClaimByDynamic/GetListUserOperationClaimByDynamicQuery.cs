﻿using Application.Features.UserOperationClaims.Dtos;
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
    public partial class GetListUserOperationClaimByDynamicQuery : IRequest<UserOperationClaimListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class GetListUserOperationClaimByDynamicQueryHandler : IRequestHandler<GetListUserOperationClaimByDynamicQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetListUserOperationClaimByDynamicQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim>? userOperationClaims = await _userOperationClaimRepository
                                                                    .GetListByDynamicAsync(request.Dynamic,
                                                                                           include: x => x.Include(g => g.User).Include(g => g.OperationClaim),
                                                                                           index: request.PageRequest.Page,
                                                                                           size: request.PageRequest.PageSize,
                                                                                           enableTracking: false);

                await _userOperationClaimBusinessRules.ShouldBeSomeDataInTheUserOperationClaimTableWhenRequested(userOperationClaims);

                UserOperationClaimListModel mappedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return mappedUserOperationClaimListModel;
            }
        }
    }
}
