using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogin.Tests
{
    [TestClass()]
    public class LoginCheckerTests
    {
        [TestMethod()]
        public void Cheak_8Symbols_ReturnsTrue()
        {
            string login = "админ888";
            bool expected = true;

            bool actual = LoginChecker.ValidateLogin(login);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Cheak_4Symbols_ReturnsFalse()
        {
            string login = "Aq12";

            bool actual = LoginChecker.ValidateLogin(login);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void Cheak_LoginWithLowerSymbols_ReturnsTrue()
        {
            string login = "1111";
            bool expected = true;

            bool actual = LoginChecker.ValidateLogin(login);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Cheak_LoginWithLowerSymbols_ReturnsFalse()
        {
            string login = "оао999999/";
            bool expected = false;

            bool actual = LoginChecker.ValidateLogin(login);

            Assert.AreEqual(expected, actual);
        }
    }
}