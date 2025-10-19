using FluentValidation;
using StudentPortal.Contracts.Requests;

namespace StudentPortal.Validations
{
    public class StudentUpdateRequestValidator : AbstractValidator<StudentUpdateRequest>
    {
        public StudentUpdateRequestValidator()
        {
            RuleFor(s => s.Name).NotEmpty().MaximumLength(50).WithMessage("이름 50글자 이내");
            RuleFor(s => s.Age).InclusiveBetween(1, 120).WithMessage("나이 1~120살 사이만가능");
        }
    }
}
