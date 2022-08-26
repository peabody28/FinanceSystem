using api.Models.UserApi;
using FluentValidation;
using FluentValidation.Results;

namespace api.Validators.UserApi
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(model => model)
                .Custom(ValidateName);
        }

        private void ValidateName(UserModel model, ValidationContext<UserModel> context)
        {
            if (string.IsNullOrEmpty(model.Name))
                context.AddFailure(new ValidationFailure(nameof(model.Name), "USERNAME_REQUIRED"));
        }
    }
}
