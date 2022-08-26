using System;

namespace entities.Interfaces
{
    public interface IFinanceOperation
    {
        Guid Id { get; set; }

        decimal Amount { get; set; }

        Guid UserFk { get; set; }
        IUser User { get; set; }

        Guid ChainFk { get; set; }
        IChain Chain { get; set; }

        bool IsApproved { get; set; }
    }
}
