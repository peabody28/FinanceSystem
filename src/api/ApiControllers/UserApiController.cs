using api.Models.UserApi;
using core;
using entities.Interfaces;
using operations.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace api.ApiControllers
{
    public class UserApiController : BaseApiController
    {
        private IUserOperation UserOperation { get; set; }

        public UserApiController(IUserOperation userOperation)
        {
            UserOperation = userOperation;
        }

        [HttpGet]
        public IEnumerable<IUser> Users()
        {
            return UserOperation.Read();
        }

        [HttpPost]
        public IUser User(UserModel model)
        {
            return UserOperation.GetObject(model.Name);
        }
    }
}
