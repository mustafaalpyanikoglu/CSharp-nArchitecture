using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandValidator : AbstractValidator<DeleteLanguageCommand>
    {
        public DeleteLanguageCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
