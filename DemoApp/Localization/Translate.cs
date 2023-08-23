using CoreM.Core.Helpers;
using CoreM.Services;
using CoreM.Startup;

namespace DemoApp.Localization
{
    /// <summary>
    ///     Translate extension that utilizes the <see cref="ILocalizationService" /> to retrieve a
    ///     localized string from <see cref="AppResource" /> and/or <see cref="ErrorResource" />.
    /// </summary>
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
       

        #region Properties

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text { get; set; } = string.Empty;

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the object created from the markup extension.
        /// </summary>
        /// <param name="serviceProvider">The service that provides the value.</param>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var localizedText = string.IsNullOrEmpty(Text)
                    ? ""
                    : AppResource.ResourceManager.GetString(Text);

                localizedText ??= ErrorResource.ResourceManager.GetString(Text);

                if (string.IsNullOrWhiteSpace(localizedText))
                {
                    throw new MissingFieldException($"\"{Text}\" is not a key in string resources or came back empty");
                }

                return localizedText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}