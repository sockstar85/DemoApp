using CoreM.Exceptions;
using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;
using DemoApp.Localization;

namespace DemoApp.Pages
{
    /// <summary>
    ///     The base view model for pages within the app.
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
        public BasePageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger)
            : base(navigation, dialogService, connectivityManager, logger)
        {
        }

        #endregion

        /// <summary>
        ///     Initializes this instance before page is placed on the navigation stack allowing for asynchronous functionality.
        ///     By default, it refreshes the network connection status.
        /// </summary>
        public override async Task InitializeAsync(INavigationParameters parameters, int lineNumber = 0,
            string methodName = "",
            string filePath = "")
        {
            await base.InitializeAsync(parameters, lineNumber, methodName, filePath);

            Console.WriteLine($"{filePath}\n{nameof(InitializeAsync)}() hit");
        }

        /// <summary>
        ///     Called when a page is navigated away from.
        /// </summary>
        public override async Task OnNavigatedFromAsync(INavigationParameters parameters, int lineNumber = 0,
            string methodName = "",
            string filePath = "")
        {
            await base.OnNavigatedFromAsync(parameters, lineNumber, methodName, filePath);

            Console.WriteLine($"{filePath}\n{nameof(OnNavigatedFromAsync)}() hit");
        }

        /// <summary>
        ///     Called when a page is navigated to just after being placed on the navigation stack.
        /// </summary>
        public override async Task OnNavigatedToAsync(INavigationParameters parameters, int lineNumber = 0,
            string methodName = "",
            string filePath = "")
        {
            await base.OnNavigatedToAsync(parameters, lineNumber, methodName, filePath);

            Console.WriteLine($"{filePath}\n{nameof(OnNavigatedToAsync)}() hit");
        }

        /// <summary>
        ///     Handles the failed navigation with a popup to alert the user.
        /// </summary>
        /// <param name="ex">The exception thrown during navigation.</param>
        protected override async Task HandleFailedNavigationAsync(Exception? ex, bool logToAppCenter = true)
        {
            if (ex is NoNetworkException)
            {
                await HandleNoNetworkConnectionAsync();
                return;
            }

            //errors on navigation already get logged

            await DialogService.DisplayAlertAsync(
                ErrorResource.GenericErrorTitle,
                ErrorResource.GenericErrorDescription,
                AppResource.Ok,
                exception: ex);
        }

        /// <summary>
        ///     Handles the no network connection with a popup to alert the user.
        /// </summary>
        protected override async Task HandleNoNetworkConnectionAsync()
        {
            await DialogService.DisplayAlertAsync(
                ErrorResource.NoNetworkTitle,
                ErrorResource.NoNetworkDescription,
                AppResource.Ok);
        }

        #endregion
    }
}