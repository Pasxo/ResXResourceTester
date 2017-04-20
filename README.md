# ResXResourceTester
This is a helper library for testing .resx files. There are 3 methods to use to help verify that your .resx files
are all there and not missing any string keys from locale to locale.

## Install

The library is released on NuGet as `Pasxo.Testing.Resx`.

## Example Use

The best use of the library is probably as a set of unit tests for the resource file in question. An example use
in this capacity is below. If you look at the source code on GitHub, you can see the example project with the
resource files and tests in action.

```cs
namespace Examples.Tests
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
```
