using entities;
using entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using repositories.Interfaces;
using System;

namespace repositories.Repositories
{
    public class ChainRepository : IChainRepository
    {
        private IServiceProvider Container { get; set; }

        private Bank Bank { get; set; }
        
        public ChainRepository(Bank database, IServiceProvider serviceProvider)
        {
            Bank = database;
            Container = serviceProvider;
        }

        public IChain Create()
        {
            var entity = Container.GetRequiredService<IChain>();
            entity.Id = Guid.NewGuid();

            var chain = Bank.Chain.Add(entity as ChainEntity);
            Bank.SaveChanges();
            return chain.Entity;
        }
    }
}
