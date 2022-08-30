using entities.Interfaces;
using MessageBroker.Kafka;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using operations.Interfaces;
using repositories.Extensions;
using repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace operations.Operations
{
    public class FinanceOperationOperation : IFinanceOperationOperation
    {
        #region [ Dependency ]

        private IFinanceOperationRepository FinanceOperationRepository { get; set; }

        private IConfiguration Configuration { get; set; }

        private IServiceProvider Container { get; set; }

        #endregion

        public FinanceOperationOperation(IFinanceOperationRepository finaceOperationRepository, IConfiguration configuration, IServiceProvider container)
        {
            FinanceOperationRepository = finaceOperationRepository;
            Configuration = configuration;
            Container = container;
        }

        public IFinanceOperation Create(IUser user, decimal amount)
        {
            var financeOperation = Container.InTransaction(() => FinanceOperationRepository.Create(user, amount));

            if(financeOperation != null)
            {
                var msgBus = new MessageBus<string>();
                var approveFinanceOperationRequestTopic = Configuration.GetSection("ApproveFinanceOperationRequestTopicName").Value;
                msgBus.SendMessage(approveFinanceOperationRequestTopic, financeOperation.Id.ToString()); 
            }

            return financeOperation;
        }

        public IEnumerable<IFinanceOperation> FinanceOperations()
        {
            return FinanceOperationRepository.FinanceOperations();
        }

        public void Approve(IFinanceOperation financeOperation)
        {
            financeOperation.IsApproved = true;
            FinanceOperationRepository.Save(financeOperation);
        }
    }
}
