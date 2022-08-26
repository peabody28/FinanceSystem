using FluentValidation.Results;
using System.Collections.Generic;

namespace validations.Interfaces
{
    public interface IUserValidation
    {
        bool IsExist(string name);

        List<ValidationFailure> Validate(string name);
    }
}
