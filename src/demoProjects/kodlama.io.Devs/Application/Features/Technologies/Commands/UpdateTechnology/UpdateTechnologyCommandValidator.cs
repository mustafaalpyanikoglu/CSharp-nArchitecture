using Application.Features.Technologies.Commands.CreateTechnology;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {

            RuleFor(l => l.Id).NotEmpty();
            RuleFor(l => l.Id).NotNull();
            RuleFor(l => l.Id).GreaterThan(0);

            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.Name).NotNull();

            RuleFor(c => c.LanguageId).NotEmpty();
            RuleFor(c => c.LanguageId).NotNull();
            RuleFor(c => c.LanguageId).GreaterThan(0);
        }
    }
}
