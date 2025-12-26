using FluentValidation;

namespace UserManagement.Application.Extensions
{
    public static class ValidatorExtensions
    {
        public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, T instance)
        {
            var validationResult = await validator.ValidateAsync(instance);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}