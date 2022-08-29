using MessageBroker.Kafka;
using Microsoft.Extensions.Configuration;
using operations.Interfaces;
using repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace background.task.tasks
{
    public class ApprovingFinanceOperationTask
    {
        private static MessageBus<string> msgBus;

        private IFinanceOperationOperation FinanceOperationOperation { get; set; }

        private IFinanceOperationRepository FinanceOperationRepository { get; set; }

        private IConfiguration Configuration { get; set; }

        public ApprovingFinanceOperationTask(IFinanceOperationRepository financeOperationRepository, IFinanceOperationOperation financeOperationOperation, IConfiguration configuration)
        {
            FinanceOperationOperation = financeOperationOperation;
            Configuration = configuration;
            FinanceOperationRepository = financeOperationRepository;
        }

        public Task Create()
        {
            var approveFinanceOperationRequestTopic = Configuration.GetSection("ApproveFinanceOperationRequestTopicName").Value;

            Console.WriteLine($"Start listening topic: {approveFinanceOperationRequestTopic}");
            return new Task(() =>
            {
                using (msgBus = new MessageBus<string>())
                {
                    try
                    {
                        msgBus.SubscribeOnTopic<string>(approveFinanceOperationRequestTopic, (msg) => ApproveFinanceOperation(msg), CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                }
            });
        }

        private bool ApproveFinanceOperation(string guid)
        {
            Console.WriteLine("financeOperation " + guid + " approved");
            var financeOperation = FinanceOperationRepository.GetObject(Guid.Parse(guid));
            FinanceOperationOperation.Approve(financeOperation);
            return true;
        }
    }
}
