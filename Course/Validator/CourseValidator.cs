using Course.Core.Dto;
using FluentValidation;
namespace Course.Validator
{
    public class CourseValidator:AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(n => n.CourseName).MaximumLength(50)
                .NotEmpty()
                .NotNull();
        }
    }
}