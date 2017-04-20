namespace Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pasxo.Testing.Resx;

    using NUnitAssert = NUnit.Framework.Assert;

    [TestClass]
    public class ResXResourceTesterUnitTests
    {
        #region VerifyAllSupportedLocalesPresent Tests
        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_MissingLocales_ReturnsNonPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingLocales");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.IsFalse(response.Passed);
        }

        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_MissingLocales_ReturnsMissingLocales()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingLocales");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(1, response.Data.Count());
            Assert.AreEqual("test2", response.Data.First());
        }

        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_MissingLocales_ReturnsErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingLocales");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual("Supported locales missing (test2) for MissingLocales resx files.", response.ErrorMessage);
        }

        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_NoMissingLocales_ReturnsPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.IsTrue(response.Passed);
        }

        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_NoMissingLocales_ReturnsNoMissingLocales()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(0, response.Data.Count());
        }
        
        [TestMethod]
        public void VerifyAllSupportedLocalesPresent_NoMissingLocales_ReturnsNullErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(null, response.ErrorMessage);
        }
        #endregion

        #region VerifyOnlySupportedLocalesPresent Tests
        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_FileNotMatchingRegex_ThrowsResXResourceTesterException()
        {
            // Arrange
            var tester = new ResXResourceTester(new TesterSettings("", "", ""));
            var settings = GetSettings("ExtraLocales");

            // Act
            var exception = NUnitAssert.Catch<ResXResourceTesterException>(() => tester.VerifyOnlySupportedLocalesPresent(settings));

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(ResXResourceTesterExceptionType.ResourceFileRegexPattern, exception.ExceptionType);
        }

        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_ExtraLocales_ReturnsNonPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("ExtraLocales");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.IsFalse(response.Passed);
        }

        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_ExtraLocales_ReturnsExtraLocales()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("ExtraLocales");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(1, response.Data.Count());
            Assert.AreEqual("test3", response.Data.First());
        }

        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_ExtraLocales_ReturnsErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("ExtraLocales");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual("Extra locales (test3) exist for ExtraLocales resx files.", response.ErrorMessage);
        }

        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_NoExtraLocales_ReturnsPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.IsTrue(response.Passed);
        }

        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_NoExtraLocales_ReturnsNoExtraLocales()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(0, response.Data.Count());
        }
        
        [TestMethod]
        public void VerifyOnlySupportedLocalesPresent_NoExtraLocales_ReturnsNullErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.AreEqual(null, response.ErrorMessage);
        }
        #endregion

        #region VerifyAllStringKeysMatch Tests
        [TestMethod]
        public void VerifyAllStringKeysMatch_MissingKeys_ReturnsNonPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingKeys");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.IsFalse(response.Passed);
        }

        [TestMethod]
        public void VerifyAllStringKeysMatch_MissingKeys_ReturnsMissingKeys()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingKeys");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            var missingKey = "String2";
            Assert.AreEqual(1, response.Data.Count);
            Assert.IsTrue(response.Data.ContainsKey(missingKey));

            var locales = response.Data[missingKey];
            Assert.AreEqual(1, locales.Count());
            Assert.AreEqual("test2", locales.First());
        }

        [TestMethod]
        public void VerifyAllStringKeysMatch_MissingKeys_ReturnsErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("MissingKeys");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.AreEqual("Some keys (String2) are not in all of the MissingKeys resx files.", response.ErrorMessage);
        }

        [TestMethod]
        public void VerifyAllStringKeysMatch_NoMissingKeys_ReturnsPassed()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.IsTrue(response.Passed);
        }

        [TestMethod]
        public void VerifyAllStringKeysMatch_NoMissingKeys_ReturnsNoMissingKeys()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.AreEqual(0, response.Data.Count());
        }
        
        [TestMethod]
        public void VerifyAllStringKeysMatch_NoMissingKeys_ReturnsNullErrorMessage()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings("AllGood");

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.AreEqual(null, response.ErrorMessage);
        }
        #endregion

        private TestSettings GetSettings(string resourceName)
        {
            var solutionDir = "../../../";
            var pathToResourceFiles = "Tests/ResourceFiles/";

            return new TestSettings
            {
                PathToResourceFiles = Path.Combine(solutionDir, pathToResourceFiles),
                ResourceName = resourceName,
                DefaultLocale = "test1",
                SupportedLocales = new List<string> { "test1", "test2" }
            };
        }
    }
}
