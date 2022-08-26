using FluentValidation;
using FluentValidation.Results;
using web.Models.Login;
using validations;
using validations.Interfaces;

namespace web.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(model => model)
                .Custom(ValidateName);
        }

        private void ValidateName(LoginModel model, ValidationContext<LoginModel> context)
        {
            if (string.IsNullOrEmpty(model.Name))
                context.AddFailure(new ValidationFailure(nameof(model.Name), "USERNAME_REQUIRED"));
        }
    }
}
