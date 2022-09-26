using Application.Features.Languages.Commands.CreateLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.Name).NotNull();

            RuleFor(c => c.LanguageId).NotEmpty();
            RuleFor(c => c.LanguageId).NotNull();
            RuleFor(c => c.LanguageId).GreaterThan(0);
        }
    }
}
