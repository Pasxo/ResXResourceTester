namespace Pasxo.Testing.Resx
{
    using System.Collections.Generic;

    /// <summary>
    /// Settings used to set up the configuration for a test.
    /// </summary>
    public class TestSettings
    {
        /// <summary>
        /// Gets or sets the name of the resource (i.e. AppStrings.resx files would be "AppStrings").
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the path to resource files.
        /// </summary>
        /// <value>
        /// The path to resource files.
        /// </value>
        public string PathToResourceFiles { get; set; }

        /// <summary>
        /// Gets or sets the default locale.
        /// </summary>
        /// <value>
        /// The default locale.
        /// </value>
        public string DefaultLocale { get; set; }

        /// <summary>
        /// Gets or sets the supported locales.
        /// </summary>
        /// <value>
        /// The supported locales.
        /// </value>
        public IEnumerable<string> SupportedLocales { get; set; }
    }
}
