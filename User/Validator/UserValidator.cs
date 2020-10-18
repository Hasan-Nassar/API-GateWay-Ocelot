using FluentValidation;
using User.Core.Dto;

namespace User.Validator
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {

            RuleFor(n => n.UserName).Length(1, 50)
                .WithMessage("please specify a fullname")
                .NotNull();

            RuleFor(e => e.Email)
                .EmailAddress()
                .NotEmpty()
                .Length(1, 50);

            RuleFor(s => s.PasswordHash).MinimumLength(8)
                .MaximumLength(16)
                .NotNull();
            //.Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$");


        }
    }
}