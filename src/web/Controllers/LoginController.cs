using Microsoft.AspNetCore.Mvc;
using operations.Interfaces;
using web.Models.Login;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Extensions;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        public IUserOperation UserOperation { get; set; }

        public LoginController(IUserOperation userOperations)
        {
            UserOperation = userOperations;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public JsonResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                ModelState.Values.ForEach(item => errors.AddRange(item.Errors.Select(error => error.ErrorMessage)));
                return new JsonResult(new { Errors = errors });
            }


            var user = UserOperation.GetObject(model.Name);
            return new JsonResult(new { Status = user != null });
        }
    }
}
