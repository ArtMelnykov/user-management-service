using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            // Email    
            RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(255);

            // UserName
            RuleFor(x => x.UserName)
            .NotEmpty()
            .Length(3, 50);

            // Password
            RuleFor(x => x.Password)
            .MinimumLength(8)
            .When(x => !string.IsNullOrEmpty(x.Password));

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