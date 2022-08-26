using entities.Interfaces;
using System.Collections.Generic;

namespace operations.Interfaces
{
    public interface IFinanceOperationOperation
    {
        IFinanceOperation Create(IUser user, decimal amount);

        IEnumerable<IFinanceOperation> FinanceOperations();

        void Approve(IFinanceOperation financeOperation);
    }
}
