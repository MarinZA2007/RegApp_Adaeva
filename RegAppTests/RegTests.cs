using RegApp;

namespace RegAppTests
{
    [TestClass]
    public class RegTests
    {
        [TestMethod]
        public void PasswordValidLength7Letters()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwert");
            string expected = "������ ����������";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordValidLength10Letters()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwertyui");
            string expected = "������ ����������";
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void PasswordIsNotValidLength6Letters()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwer");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordIsNotValidLength11Letters()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwertyuio");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordIsEmpty()
        {
            User password = new User();
            string actual = User.TestPassword("");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }
        
        [TestMethod]
        public void PasswordContainOnlyNumbers()
        {
            User password = new User();
            string actual = User.TestPassword("1234567");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordContainOnlyLetters()
        {
            User password = new User();
            string actual = User.TestPassword("Qwertyu");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordContainOnlySpecSymbols ()
        {
            User password = new User();
            string actual = User.TestPassword("!@#$%^&*");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithoutUpperLetters()
        {
            User password = new User();
            string actual = User.TestPassword("!1qwerty");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithoutLowerLetter()
        {
            User password = new User();
            string actual = User.TestPassword("!1QWERTY");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithoutSpecSymbols()
        {
            User password = new User();
            string actual = User.TestPassword("1Qwerty");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithSpaceAtTheBeginning()
        {
            User password = new User();
            string actual = User.TestPassword(" !1Qwert");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithSpaceInTheMiddle()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qw ert");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithSpaceAtTheEnd()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwert ");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithDot()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwert.");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void PasswordWithATsign()
        {
            User password = new User();
            string actual = User.TestPassword("!1Qwert@");
            string expected = "������ ����������";
            Assert.AreNotEqual(actual, expected);
        }

    }
}
