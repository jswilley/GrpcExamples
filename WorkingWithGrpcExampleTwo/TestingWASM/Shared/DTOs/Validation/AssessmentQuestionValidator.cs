using FluentValidation;
using System.Data;
using System.Linq;

namespace TestingWASM.Shared.DTOs.Validation
{
    public class FormQuestionValidator : AbstractValidator<FormQuestionDTO>
    {
        public FormQuestionValidator()
        {
            RuleFor(q => q.ResponseValue).NotEmpty().WithMessage("A response is required.  Please enter a response.")
                .When(q => q.Required);

            // Grab the follow-up questions for the response that matches the value the user selected.
            RuleForEach(f => f.ResponseOptions)
                .Where(f => f.Selected)
                .ChildRules(c =>
                {
                    c.RuleForEach(n => n.FollowUpQuestions)
                        .ChildRules(q =>
                        {
                            q.RuleFor(x => x.ResponseValue).NotEmpty().WithMessage("A response is required.  Please enter a response.")
                                .When(n => n.Required);
                        });
                });
        }
    }
}