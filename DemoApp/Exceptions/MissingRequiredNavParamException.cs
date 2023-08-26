namespace DemoApp.Exceptions
{
    /// <summary>
    ///     Exception to be thrown when required navigation parameters are missing.
    /// </summary>
    public class MissingRequiredNavParamException : Exception
    {
        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingRequiredNavParamException" /> class.
        /// </summary>
        public MissingRequiredNavParamException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingRequiredNavParamException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingRequiredNavParamException(string message) : base(message)
        {
        }

        #endregion

        #endregion
    }
}