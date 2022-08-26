using FluentValidation;
using web.Models.FinanceOperation;
using validations;
using validations.Interfaces;

namespace web.Validators
{
    public class CreateFinanceOperationValidation : AbstractValidator<CreateFinanceOperationModel>
    {
        private IUserValidation UserValidation { get; set; }
        public CreateFinanceOperationValidation(IUserValidation userValidation)
        {
            UserValidation = userValidation;

            RuleFor(model => model)
                .Custom((model, context) => 
                { 
                    context.AddFailures(nameof(model.UserName), UserValidation.Validate(model.UserName)); 
                });
        }
    }
}
