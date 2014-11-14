using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Authentication.PasswordHashing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Template.Authentication.PasswordHashing.Tests
{
    [TestClass()]
    public class BCryptHashImplTests
    {
        private IPasswordHashing _hashingEngine;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInit()
        {
            _hashingEngine = new BCryptHashImpl();
        }



        [TestMethod()]
        public void GetSaltDoesNotThrowException()
        {
            _hashingEngine.GetSalt();
        }
    }
}
