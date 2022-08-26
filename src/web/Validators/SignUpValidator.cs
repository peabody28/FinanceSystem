using FluentValidation;
using FluentValidation.Results;
using web.Models.SignUp;
using validations.Interfaces;

namespace web.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpModel>
    {
        private IUserValidation UserValidation { get; set; }
        public SignUpValidator(IUserValidation userValidation)
        {
            UserValidation = userValidation;

            RuleFor(model => model)
                .Custom(ValidateName);
        }

        private void ValidateName(SignUpModel model, ValidationContext<SignUpModel> context)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                context.AddFailure(new ValidationFailure(nameof(model.Name), "USERNAME_REQUIRED"));
                return;
            }

            if(UserValidation.IsExist(model.Name))
                context.AddFailure(new ValidationFailure(nameof(model.Name), "USER_ALREADY_EXIST"));
        }
    }
}
