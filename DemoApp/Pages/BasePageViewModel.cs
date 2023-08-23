using CoreM.Exceptions;
using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;

namespace DemoApp.Pages
{
    /// <summary>
    ///     The base view model for pages.
    /// </summary>
    public class BasePageViewModel : NavigatableViewModel
    {
        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePageViewModel" /> class.
        /// </summary>
        /// <param name="navigation">The navigation.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="connectivityManager">The connectivity manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="resourceManager">The resource manager.</param>
        public BasePageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger,
            ILocalizationService resourceManager)
            : base(navigation, dialogService, connectivityManager, logger, resourceManager)
        {
        }

        #endregion

        /// <summary>
        ///     Handles the failed navigation with a popup to alert the user.
        /// </summary>
        /// <param name="ex">The exception thrown during navigation.</param>
        /// <param name="logToAppCenter">Flag indicating whether to log error to AppCenter.</param>
        protected override async Task HandleFailedNavigationAsync(Exception ex, bool logToAppCenter = true)
        {
            if (ex is NoNetworkException)
            {
                await HandleNoNetworkConnectionAsync();
                return;
            }

            Logger.Log(ex, logToAppCenter);

            await DialogService.DisplayAlertAsync("Error", "An error occurred", "Ok", exception: ex);
        }

        /// <summary>
        ///     Handles the no network connection with a popup to alert the user.
        /// </summary>
        protected override async Task HandleNoNetworkConnectionAsync()
        {
            await DialogService.DisplayAlertAsync("No network connection", "Unable to perform action", "Ok");
        }

        #endregion
    }
}