using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPassword.Tests
{
    [TestClass()]
    public class PasswordCheckerTests
    {
        [TestMethod()]
        public void Cheak_8Symbols_ReturnsTrue()
        {
            string password = "ггггd///";
            bool expected = true;

            bool actual = PasswordChecker.ValidatePassword(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Cheak_4Symbols_ReturnsFalse()
        {
            string password = "Aq1$";

            bool actual = PasswordChecker.ValidatePassword(password);

            Assert.IsFalse(actual);
        }

        [TestMethod()]  
        public void Cheak_PasswordWithLowerSymbols_ReturnsTrue()
        {
            string password = "1111";
            bool expected = true;

            bool actual = PasswordChecker.ValidatePassword(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Cheak_PasswordWithLowerSymbols_ReturnsFalse()
        {
            string password = "1111/";
            bool expected = false;

            bool actual = PasswordChecker.ValidatePassword(password);

            Assert.AreEqual (expected, actual);
        }
    }
}