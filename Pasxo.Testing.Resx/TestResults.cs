namespace Pasxo.Testing.Resx
{
    /// <summary>
    /// Details about a test that was run.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestResults<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestResults{T}"/> class.
        /// </summary>
        /// <param name="passed">if set to <c>true</c> [passed].</param>
        /// <param name="data">The data.</param>
        /// <param name="errorMessage">The error message.</param>
        public TestResults(bool passed, T data, string errorMessage)
        {
            Passed = passed;
            Data = data;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets a value indicating whether or not the test passed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the test passed; otherwise, <c>false</c>.
        /// </value>
        public bool Passed { get; private set; }

        /// <summary>
        /// Gets the data to help fix resx file issues.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; private set; }

        /// <summary>
        /// Gets the formatted error message with details about the failed test.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; private set; }
    }
}
