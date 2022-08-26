using entities.Interfaces;
using System;
using System.Collections.Generic;

namespace repositories.Interfaces
{
    public interface IFinanceOperationRepository
    {
        IFinanceOperation GetObject(Guid id);

        IFinanceOperation Create(IUser user, decimal amount);

        IEnumerable<IFinanceOperation> FinanceOperations();

        void Save(IFinanceOperation financeOperation);
    }
}
