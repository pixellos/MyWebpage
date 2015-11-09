using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebpage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Entity.Tests
{
    class UserRepositoryInheirter : UserRepository
    {
        public UserRepositoryInheirter(string connectString) : base(connectString)
        {}

        public void DeleteWholeDatabase()
        {
            _context.Users.RemoveRange(_context.Users.ToList());
            _context.SaveChanges();
        }
    }


    [TestClass()]
    public class UserRepositoryTests
    {
        private const string TestConnectionString = "AccTEst";
        private UserRepositoryInheirter inheirter = new UserRepositoryInheirter(TestConnectionString);
        [TestMethod()]
        public void UserRepositoryTest()
        {
            inheirter.DeleteWholeDatabase();
            inheirter.RegisterNewUser("Test0", "TestPass0", "Not");
        }

        [TestMethod()]
        public void IsPasswordOfUserVaildTest()
        {
            Assert.IsFalse(inheirter.IsPasswordOfUserVaild(null,null));
            Assert.IsFalse(inheirter.IsPasswordOfUserVaild("ssdsd", "dsdsds"));
            Assert.IsFalse(inheirter.IsPasswordOfUserVaild("Test0", "badPasswordBitch"));

            Assert.IsTrue(
                inheirter.IsPasswordOfUserVaild("Test0","TestPass0")
                );
        }

        [TestMethod()]
        public void TryToChangePasswordTest()
        {
            Assert.IsFalse(inheirter.TryToChangePassword(null,null,null));
            Assert.IsFalse(inheirter.TryToChangePassword("DoesntExist","Doesnt","Doesnt"));
            Assert.IsFalse(inheirter.TryToChangePassword("Test0","BadPassWord",null));
            Assert.IsFalse(inheirter.TryToChangePassword("Test0", "BadPassWord", "password"));

            Assert.IsFalse(inheirter.IsPasswordOfUserVaild("Test0", "Test1"));
            Assert.IsTrue(inheirter.TryToChangePassword("Test0", "TestPass0", "Test1"));
            Assert.IsTrue(inheirter.IsPasswordOfUserVaild("Test0","Test1"));
        }

        [TestMethod()]
        public void RegisterNewUserTest()
        {
            Assert.IsTrue(
             inheirter.RegisterNewUser("Test1", "Test1", "Test1")
            );

            inheirter.RegisterNewUser(null, null, null);
        }

        [TestMethod()]
        public void GeAccessLevelTest()
        {
            try
            {
                inheirter.GeAccessLevel(null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(AccessLevel.Error,inheirter.GeAccessLevel(""));

            Assert.AreEqual(AccessLevel.User,
                inheirter.GeAccessLevel("Test1")
                );
        }
    }
}