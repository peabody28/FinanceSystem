using entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities
{
    public class FinanceOperationEntity : IFinanceOperation
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }


        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }


        [ForeignKey("Chain")]
        public Guid ChainFk { get; set; }
        public IChain Chain { get; set; }

        IUser IFinanceOperation.User
        {
            get => User;
            set
            {
                User = value as UserEntity;
            }
        }

        IChain IFinanceOperation.Chain
        {
            get => Chain;
            set
            {
                Chain = value as ChainEntity;
            }
        }

        public bool IsApproved { get; set; }
    }
}
