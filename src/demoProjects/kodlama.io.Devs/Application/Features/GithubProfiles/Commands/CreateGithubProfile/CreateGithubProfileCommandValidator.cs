using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommandValidator:AbstractValidator<CreateGithubProfileCommand>
    {
        public CreateGithubProfileCommandValidator()
        {
            RuleFor(x=> x.UserId).NotEmpty();
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.UserId).NotNull();

            RuleFor(x => x.Github).NotEmpty();
            RuleFor(x => x.Github).MinimumLength(5);
            RuleFor(x => x.Github).NotNull();
        }
    }
}
