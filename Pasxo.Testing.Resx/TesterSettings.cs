namespace Pasxo.Testing.Resx
{
    using System.Collections.Generic;

    /// <summary>
    /// Settings used to set up the configuration for a test.
    /// </summary>
    public class TesterSettings
    {
        public TesterSettings()
        {
            DefaultPattern = "{0}.resx";
            LocalePattern = "{0}.{1}.resx";

            // Pattern is resource_name.locale.resx
            ResourceNameRegex = "(?'resource_name'[^\\.]+)(\\.(?'locale'.+))?\\.resx";
        }

        public TesterSettings(string defaultPattern, string localePattern, string resourceNameRegex)
        {
            DefaultPattern = defaultPattern;
            LocalePattern = localePattern;
            ResourceNameRegex = resourceNameRegex;
        }

        /// <summary>
        /// Gets the default pattern.
        /// </summary>
        /// <value>
        /// The default pattern.
        /// </value>
        public string DefaultPattern { get; private set; }

        /// <summary>
        /// Gets the locale pattern.
        /// </summary>
        /// <value>
        /// The locale pattern.
        /// </value>
        public string LocalePattern { get; private set; }

        /// <summary>
        /// Gets the resource name regex.
        /// </summary>
        /// <value>
        /// The resource name regex.
        /// </value>
        public string ResourceNameRegex { get; private set; }
    }
}
