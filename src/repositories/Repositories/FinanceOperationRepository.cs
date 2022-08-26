using entities;
using entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace repositories.Repositories
{
    public class FinanceOperationRepository : IFinanceOperationRepository
    {
        private IServiceProvider Container { get; set; }

        private Bank Bank { get; set; }

        public IChainRepository ChainRepository { get; set; }

        public FinanceOperationRepository(Bank database, IChainRepository chainRep, IServiceProvider container)
        {
            Bank = database;
            ChainRepository = chainRep;
            Container = container;
        }

        public IFinanceOperation Create(IUser user, decimal amount)
        {
            var transaction = Bank.Database.BeginTransaction();
            var chain = ChainRepository.Create();

            var entity = Container.GetRequiredService<IFinanceOperation>();
            entity.Id = Guid.NewGuid();
            entity.Chain = chain;
            entity.User = user;
            entity.Amount = amount;

            var financeOperation = Bank.FinanceOperation.Add(entity as FinanceOperationEntity);

            try
            {
                Bank.SaveChanges();
                transaction.Commit();
                return financeOperation.Entity;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<IFinanceOperation> FinanceOperations()
        {
            return Bank.FinanceOperation.Cast<IFinanceOperation>();
        }

        public void Save(IFinanceOperation financeOperation)
        {
            Bank.FinanceOperation.Update(financeOperation as FinanceOperationEntity);
            Bank.SaveChanges();
        }

        public IFinanceOperation GetObject(Guid id)
        {
            return Bank.FinanceOperation.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
