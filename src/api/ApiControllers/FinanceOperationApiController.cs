﻿using api.Models.FinanceOperationApi;
using core;
using entities.Interfaces;
using operations.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace api.ApiControllers
{
    public class FinanceOperationApiController : BaseApiController
    {
        private IFinanceOperationOperation FinanceOperationOperation { get; set; }

        private IUserOperation UserOperation { get; set; }


        public FinanceOperationApiController(IFinanceOperationOperation financeOperationOperation, IUserOperation userOperation)
        {
            FinanceOperationOperation = financeOperationOperation;
            UserOperation = userOperation;
        }

        [HttpGet]
        public IEnumerable<IFinanceOperation> FinanceOperations()
        {
            return FinanceOperationOperation.FinanceOperations();
        }

        [HttpPost]
        public FinanceOperationModel Create(CreateFinanceOperationModel model)
        {
            var user = UserOperation.GetObject(model.Username);

            var financeOperation = FinanceOperationOperation.Create(user, model.Amount);

            return new FinanceOperationModel { ChainId = financeOperation.Chain.Id };
        }
    }
}
