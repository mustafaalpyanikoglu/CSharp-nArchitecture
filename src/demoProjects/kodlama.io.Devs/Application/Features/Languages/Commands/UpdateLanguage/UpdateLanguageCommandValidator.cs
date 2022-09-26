using Application.Features.Languages.Commands.DeleteLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator: AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(l => l.Id).NotEmpty();
            RuleFor(l => l.Id).NotNull();
            RuleFor(l => l.Id).GreaterThan(0);

            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.Name).NotNull();
        }
    }
}
