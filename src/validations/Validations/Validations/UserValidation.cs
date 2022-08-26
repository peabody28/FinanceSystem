using FluentValidation.Results;
using operations.Interfaces;
using System.Collections.Generic;
using validations.Interfaces;

namespace validations.Validations
{
    public class UserValidation : IUserValidation
    {
        private IUserOperation UserOperation { get; set; }

        public UserValidation(IUserOperation userOperation)
        {
            UserOperation = userOperation;
        }

        public bool IsExist(string name)
        {
            var user = UserOperation.GetObject(name);
            return user != null;
        }

        public List<ValidationFailure> Validate(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<ValidationFailure>() { new ValidationFailure("UserName", "USERNAME_REQUIRED") };
            
           if(!IsExist(name))
                return new List<ValidationFailure>() { new ValidationFailure("UserName", "USERNAME_INVALID") };

            return new List<ValidationFailure>();
        }
    }
}
