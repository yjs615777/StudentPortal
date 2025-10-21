using FluentValidation;
using StudentPortal.Contracts.Requests;

namespace StudentPortal.Validations
{
    public class StudentCreateRequestValidator : AbstractValidator<StudentCreateRequest>
    {
        public StudentCreateRequestValidator()
        {
            RuleFor(s => s.Name).NotEmpty().MaximumLength(50).WithMessage("이름은 50자 이내여야 합니다");
            RuleFor(s => s.Age).InclusiveBetween(1, 120).WithMessage("나이는 1~120살 사이만 가능합니다");
        }
    }
}
