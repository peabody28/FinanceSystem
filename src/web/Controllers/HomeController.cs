using Microsoft.AspNetCore.Mvc;
using operations.Interfaces;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        #region [Dependencies]

        private IUserOperation UserOperation { get; set; }
        
        #endregion

        public HomeController(IUserOperation userOperations)
        {
            UserOperation = userOperations;
        }

        public IActionResult Index()
        {
            var users = UserOperation.Read();
            return View(users);
        }
    }
}
