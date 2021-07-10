using FluentValidation;
using System.Linq;

namespace TestingWASM.Shared.DTOs.Validation
{
    public class FormValidator : AbstractValidator<FormDTO>
    {
        public FormValidator()
        {
            // We can add some data integrity checks here that apply regardless of status.
            //RuleFor(p => p.AOOfficerName).MaximumLength(50);
            //RuleFor(p => p.AOOfficerDPSST).MaximumLength(50);
            //RuleFor(p => p.AOAgency).MaximumLength(50);
            //RuleFor(p => p.AOPrecinct).MaximumLength(50);
            //RuleFor(p => p.TransitOfficer).MaximumLength(50);
            //RuleFor(p => p.TransitOfficerDPSST).MaximumLength(50);
            //RuleFor(p => p.BookingDeputy).MaximumLength(50);
            //RuleFor(p => p.BookingDeputyDPSST).MaximumLength(50);
            //RuleFor(p => p.Status).MaximumLength(50);
           
            //    RuleForEach(a => a.Questions).SetValidator(new FormQuestionValidator());
            
        }
    }
}