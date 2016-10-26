using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplicationAuth.Controllers;
using UnitTest.Base;
using IdentityCore;
using WebApplicationAuth.Models;
using System.Web.Http;

namespace UnitTest
{
    [TestClass]
    public class AccountUnitTest
    {
        [TestMethod]
        public void TestRegisterByEmail()
        {
            var manager = new ApplicationUserManager(new TestUserStore());
            var controller = new AccountController(manager);
            var user = new RegisterBindingModel()
            {
                UserName = "a@a.a",
                Password ="abcdef",
                Type = UserNameType.Email
            };
            var result = controller.Register(user).Result as IHttpActionResult;
            Assert.IsNotNull(result);
            var addedUser = manager.FindByNameAsync("a@a.a").Result;
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("a@a.a", addedUser.UserName);
        }

        [TestMethod]
        public void TestRegisterByPhone()
        {
            var manager = new ApplicationUserManager(new TestUserStore());
            var controller = new AccountController(manager);
            var user = new RegisterBindingModel()
            {
                UserName = "13000000000",
                Password = "abcdef",
                Type = UserNameType.Phone
            };
            var result = controller.Register(user).Result as IHttpActionResult;
            Assert.IsNotNull(result);
            var addedUser = manager.FindByNameAsync("13000000000").Result;
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("13000000000", addedUser.UserName);
        }

        [TestMethod]
        public void TestRegisterByUserName()
        {
            var manager = new ApplicationUserManager(new TestUserStore());
            var controller = new AccountController(manager);
            var user = new RegisterBindingModel()
            {
                UserName = "user",
                Password = "abcdef",
                Type = UserNameType.Phone
            };
            var result = controller.Register(user).Result as IHttpActionResult;
            Assert.IsNotNull(result);
            var addedUser = manager.FindByNameAsync("user").Result;
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("user", addedUser.UserName);
        }
    }
}
