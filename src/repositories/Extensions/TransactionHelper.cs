using Microsoft.Extensions.DependencyInjection;
using System;

namespace repositories.Extensions
{
    public static class TransactionHelper
    {

        public static TResult InTransaction<TResult>(this IServiceProvider container, Func<TResult> function)
        {
            var bank = container.GetRequiredService<Bank>();
            if (bank.Database.CurrentTransaction != null)
                return function();

            using (var transaction = bank.Database.BeginTransaction())
            {
                try
                {
                    var result = function();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void InTransaction(this IServiceProvider container, Action function)
        {
            var bank = container.GetRequiredService<Bank>();
            if (bank.Database.CurrentTransaction != null)
            {
                function();
                return;
            }

            using (var transaction = bank.Database.BeginTransaction())
            {
                try
                {
                    function();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
