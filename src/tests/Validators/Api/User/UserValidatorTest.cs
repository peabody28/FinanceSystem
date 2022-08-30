using api.Validators.UserApi;
using NUnit.Framework;

namespace tests
{
    public class UserValidatorTest
    {

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        [TestCase("")]
        [TestCase("max")]
        public void GetUser(string name)
        {
            var validator = new UserValidator();
            var model = new api.Models.UserApi.UserModel { Name = name };

            var result = validator.Validate(model);
            
            if(string.IsNullOrEmpty(name))
            {
                Assert.IsFalse(result.IsValid, "model.Name is empty but result is valid");
                Assert.IsNotEmpty(result.Errors, "errors is not empty");
            }
        }
    }
}