namespace Pasxo.Testing.Resx
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A helper class for verifying resx files.
    /// </summary>
    public class ResXResourceTester : IResXResourceTester
    {
        /// <summary>
        /// The tester settings.
        /// </summary>
        private readonly TesterSettings TesterSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTester"/> class, using the default settings.
        /// </summary>
        public ResXResourceTester()
            : this(new TesterSettings())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTester"/> class.
        /// </summary>
        /// <param name="testerSettings">The tester settings.</param>
        public ResXResourceTester(TesterSettings testerSettings)
        {
            TesterSettings = testerSettings;
        }

        /// <summary>
        /// Verifies that all of the supported locales are present, returning details about the test (i.e. whether or
        /// not the test passed, data to help fix the issues, and a formatted string to display in your failed tests).
        /// </summary>
        /// <param name="settings">The test settings.</param>
        /// <returns>
        /// The test results.
        /// </returns>
        public TestResults<IEnumerable<string>> VerifyAllSupportedLocalesPresent(TestSettings settings)
        {
            var missingSupportedLocales = GetMissingSupportedLocales(settings);
            var passed = missingSupportedLocales.Count() == 0;
            var errorMessage = passed ? null : string.Format(
                "Supported locales missing ({1}) for {0} resx files.",
                settings.ResourceName,
                string.Join(", ", missingSupportedLocales));

            return new TestResults<IEnumerable<string>>(passed, missingSupportedLocales, errorMessage);
        }

        /// <summary>
        /// Verifies that only the supported locales are present, returning details about the test (i.e. whether or not
        /// the test passed, data to help fix the issues, and a formatted string to display in your failed tests).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The test results.
        /// </returns>
        /// <exception cref="ResXResourceTesterException">
        /// The resource file was not matched with the regular expression in the settings.
        /// </exception>
        public TestResults<IEnumerable<string>> VerifyOnlySupportedLocalesPresent(TestSettings settings)
        {
            var nonSupportedLocales = GetNonSupportedLocales(settings);
            var passed = nonSupportedLocales.Count() == 0;
            var errorMessage = passed ? null : string.Format(
                "Extra locales ({1}) exist for {0} resx files.",
                settings.ResourceName,
                string.Join(", ", nonSupportedLocales));

            return new TestResults<IEnumerable<string>>(passed, nonSupportedLocales, errorMessage);
        }

        /// <summary>
        /// Verifies that all of the string keys match across the locales, returning details about the test (i.e.
        /// whether or not the test passed, data to help fix the issues, and a formatted string to display in your
        /// failed tests).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The test results.
        /// </returns>
        public TestResults<IDictionary<string, IEnumerable<string>>> VerifyAllStringKeysMatch(TestSettings settings)
        {
            var missingStringKeys = GetMissingStringKeys(settings);
            var passed = missingStringKeys.Count() == 0;
            var errorMessage = passed ? null : string.Format(
                "Some keys ({1}) are not in all of the {0} resx files.",
                settings.ResourceName,
                string.Join(", ", missingStringKeys.Keys));

            return new TestResults<IDictionary<string, IEnumerable<string>>>(passed, missingStringKeys, errorMessage);
        }

        /// <summary>
        /// Gets the missing supported locales.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The list of missing supported locales.
        /// </returns>
        private IEnumerable<string> GetMissingSupportedLocales(TestSettings settings)
        {
            var missingLocales = new List<string>();

            foreach (var locale in settings.SupportedLocales)
            {
                var pathToResource = GetPathToResourceFile(settings, locale);

                if (!File.Exists(pathToResource))
                {
                    missingLocales.Add(locale);
                }
            }

            return missingLocales;
        }

        /// <summary>
        /// Gets the non supported locales.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The list of non supported locales found.
        /// </returns>
        /// <exception cref="ResXResourceTesterException">
        /// The resource file was not matched with the regular expression in the settings.
        /// </exception>
        private IEnumerable<string> GetNonSupportedLocales(TestSettings settings)
        {
            var nonSupportedLocales = new List<string>();
            var resourceFiles = Directory.GetFiles(settings.PathToResourceFiles, settings.ResourceName + "*.resx");

            foreach (var resourceFile in resourceFiles)
            {
                var locale = GetResourceFileLocale(settings, Path.GetFileName(resourceFile));

                if (!settings.SupportedLocales.Any(l => l == locale))
                {
                    nonSupportedLocales.Add(locale);
                }
            }

            return nonSupportedLocales;
        }

        /// <summary>
        /// Gets the details around which keys are missing in which locale files.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The details around which keys are missing in which locale files.
        /// </returns>
        private IDictionary<string, IEnumerable<string>> GetMissingStringKeys(TestSettings settings)
        {
            // keep track of which locales have the resource key
            var allKeys = new Dictionary<string, List<string>>();

            foreach (var locale in settings.SupportedLocales)
            {
                var pathToResource = GetPathToResourceFile(settings, locale);

                using (var resxReader = new ResXResourceReader(pathToResource))
                {
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        var key = entry.Key as string;

                        if (!allKeys.ContainsKey(key))
                        {
                            allKeys.Add(key, new List<string>());
                        }

                        allKeys[key].Add(locale);
                    }
                }
            }

            return allKeys
                .Where(kvp => kvp.Value.Count != settings.SupportedLocales.Count())
                .ToDictionary(kvp => kvp.Key, kvp => settings.SupportedLocales.Except(kvp.Value));
        }

        /// <summary>
        /// Gets the locale associated with the resource file.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="resourceFile">The resource file.</param>
        /// <returns>
        /// The locale associated with the file.
        /// </returns>
        /// <exception cref="ResXResourceTesterException">
        /// The resource file was not matched with the regular expression in the settings.
        /// </exception>
        private string GetResourceFileLocale(TestSettings settings, string resourceFile)
        {
            var matches = Regex.Matches(resourceFile, TesterSettings.ResourceNameRegex);
            
            if (matches.Count != 1)
            {
                throw new ResXResourceTesterException(
                    ResXResourceTesterExceptionType.ResourceFileRegexPattern,
                    string.Format("The resource file ({0}) was not matched with the regular expression in the settings.", resourceFile));
            }

            var locale = matches[0].Groups["locale"].Value;

            if (string.IsNullOrEmpty(locale))
            {
                return settings.DefaultLocale;
            }

            return locale;
        }

        /// <summary>
        /// Gets the path to the resource file based on the locale.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>
        /// The path to the resource file based on the locale.
        /// </returns>
        private string GetPathToResourceFile(TestSettings settings, string locale)
        {
            var pattern = locale == settings.DefaultLocale ? TesterSettings.DefaultPattern : TesterSettings.LocalePattern;
            var resourceFileName = string.Format(pattern, settings.ResourceName, locale);
            var pathToResource = Path.Combine(settings.PathToResourceFiles, resourceFileName);

            return pathToResource;
        }
    }
}
