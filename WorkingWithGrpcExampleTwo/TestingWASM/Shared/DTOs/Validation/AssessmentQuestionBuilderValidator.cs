using FluentValidation;
using System.Data;
using System.Linq;

namespace TestingWASM.Shared.DTOs.Validation
{
    public class FormQuestionBuilderValidator : AbstractValidator<FormQuestionBuilderDTO>
    {
        public FormQuestionBuilderValidator()
        {
            RuleFor(q => q.Question).NotEmpty().WithMessage("Question is required.");
            RuleFor(q => q.NullableQuestionResponseTypeId).NotNull().WithMessage("Response Type is required.");
            RuleFor(q => q.NullableQuestionResponseTypeId).GreaterThan("0").WithMessage("Response Type is required.");
            RuleFor(q => q.IsRequiredResponse).NotEmpty().WithMessage("Please specify if this question is required.");
            RuleFor(q => q.FormSection).NotEmpty().WithMessage("Form Section is required.");
        }
    }
}