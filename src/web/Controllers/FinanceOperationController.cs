using Microsoft.AspNetCore.Mvc;
using operations.Interfaces;
using web.Models.FinanceOperation;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Extensions;

namespace web.Controllers
{
    public class FinanceOperationController : Controller
    {
        #region [ Dependencies ]
        private IFinanceOperationOperation FinanceOperationOperation { get; set; }
        private IUserOperation UserOperation { get; set; }

        #endregion

        public FinanceOperationController(IFinanceOperationOperation finaceOperationOperation, IUserOperation userOperation)
        {
            FinanceOperationOperation = finaceOperationOperation;
            UserOperation = userOperation;
        }

        public ActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        public JsonResult Create(CreateFinanceOperationModel model)
        {
            if(!ModelState.IsValid)
            {
                var errors = new List<string>();
                ModelState.Values.ForEach(item => errors.AddRange(item.Errors.Select(error => error.ErrorMessage)));
                return new JsonResult(new { Errors = errors });
            }

            var user = UserOperation.GetObject(model.UserName);
            var financeOperationEntity = FinanceOperationOperation.Create(user, model.Amount);

            return new JsonResult(new FinanceOperationModel()
            {
                Id = financeOperationEntity.Id,
                Amount = financeOperationEntity.Amount,
                UserFk = financeOperationEntity.UserFk,
                ChainFk = financeOperationEntity.ChainFk
            });
        }
    }
}
