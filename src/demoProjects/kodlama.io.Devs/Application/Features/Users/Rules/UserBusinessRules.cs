﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ThereShouldBeSomeDataInUserListAsRequired(IPaginate<User> users)
        {
            if (!users.Items.Any()) throw new BusinessException("There is no any data in list!");
        }
    }
}
