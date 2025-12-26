using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            // Email
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

            // UserName
            RuleFor(x => x.UserName)
            .NotEmpty()
            .Length(3, 50);

            // Password
            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

            // FirstName
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

            // LastName
            RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
        }
    }
}