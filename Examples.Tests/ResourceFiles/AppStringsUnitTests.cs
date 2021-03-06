﻿namespace Examples.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pasxo.Testing.Resx;

    [TestClass]
    public class AppStringsUnitTests
    {
        [TestMethod]
        public void AppStrings_VerifyAllSupportedLocalesPresent()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings();

            // Act
            var response = tester.VerifyAllSupportedLocalesPresent(settings);

            // Assert
            Assert.IsTrue(response.Passed, response.ErrorMessage);
        }

        [TestMethod]
        public void AppStrings_VerifyOnlySupportedLocalesPresent()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings();

            // Act
            var response = tester.VerifyOnlySupportedLocalesPresent(settings);

            // Assert
            Assert.IsTrue(response.Passed, response.ErrorMessage);
        }

        [TestMethod]
        public void AppStrings_VerifyAllStringKeysMatch()
        {
            // Arrange
            var tester = new ResXResourceTester();
            var settings = GetSettings();

            // Act
            var response = tester.VerifyAllStringKeysMatch(settings);

            // Assert
            Assert.IsTrue(response.Passed, response.ErrorMessage);
        }

        private TestSettings GetSettings()
        {
            var solutionDir = "../../../";
            var pathToResourceFiles = "Examples/ResourceFiles/";

            return new TestSettings
            {
                PathToResourceFiles = Path.Combine(solutionDir, pathToResourceFiles),
                ResourceName = "AppStrings",
                DefaultLocale = "en-US",
                SupportedLocales = new List<string> { "en-US", "es-ES" }
            };
        }
    }
}
