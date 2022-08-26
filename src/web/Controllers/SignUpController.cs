using Microsoft.AspNetCore.Mvc;
using operations.Interfaces;
using web.Models.SignUp;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Extensions;

namespace web.Controllers
{
    public class SignUpController : Controller
    {
        private IUserOperation UserOperation { get; set; }

        public SignUpController(IUserOperation userOperation)
        {
            UserOperation = userOperation;
        }
        public IActionResult Index()
        {
            return View("SignUp");
        }

        [HttpPost]
        public JsonResult Create(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                ModelState.Values.ForEach(item => errors.AddRange(item.Errors.Select(error => error.ErrorMessage)));
                return new JsonResult(new { Errors = errors });
            }

            var user = UserOperation.Create(model.Name);
            return new JsonResult(new RedirectModel() { Url = "/" });
        }
    }
}
