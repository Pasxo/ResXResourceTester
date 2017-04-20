namespace Pasxo.Testing.Resx
{
    using System.Collections.Generic;

    /// <summary>
    /// An API to help verify resx files are complete and in sync.
    /// </summary>
    public interface IResXResourceTester
    {
        /// <summary>
        /// Verifies that all of the supported locales are present, returning details about the test (i.e. whether or
        /// not the test passed, data to help fix the issues, and a formatted string to display in your failed tests).
        /// </summary>
        /// <param name="settings">The test settings.</param>
        /// <returns>
        /// The test details.
        /// </returns>
        TestResults<IEnumerable<string>> VerifyAllSupportedLocalesPresent(TestSettings settings);

        /// <summary>
        /// Verifies that only the supported locales are present, returning details about the test (i.e. whether or not
        /// the test passed, data to help fix the issues, and a formatted string to display in your failed tests).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The test details.
        /// </returns>
        TestResults<IEnumerable<string>> VerifyOnlySupportedLocalesPresent(TestSettings settings);

        /// <summary>
        /// Verifies that all of the string keys match across the locales, returning details about the test (i.e.
        /// whether or not the test passed, data to help fix the issues, and a formatted string to display in your
        /// failed tests).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The test details.
        /// </returns>
        TestResults<IDictionary<string, IEnumerable<string>>> VerifyAllStringKeysMatch(TestSettings settings);
    }
}