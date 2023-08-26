using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;
using DemoApp.Exceptions;
using DemoApp.Localization;

namespace DemoApp.Pages
{
    /// <summary>
    ///     The next page to navigate to.
    /// </summary>
    public class NextPageViewModel : BasePageViewModel
    {
        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NextPageViewModel" /> class.
        /// </summary>
        /// <param name="navigation">The navigation.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="connectivityManager">The connectivity manager.</param>
        /// <param name="logger">The logger.</param>
        public NextPageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger)
            : base(navigation, dialogService, connectivityManager, logger)
        {
        }

        #endregion

        /// <summary>
        ///     Handles the on back button pressed asynchronous.
        /// </summary>
        public override async Task HandleBackButtonOverrideAsync()
        {
            var confirm = await DialogService.DisplayAlertAsync(
                AppResource.BackButtonConfirmationTitle,
                AppResource.BackButtonConfirmationDescription,
                AppResource.Yes,
                AppResource.No);

            if (confirm)
            {
                await base.HandleBackButtonOverrideAsync();
            }
        }

        /// <summary>
        ///     Initializes this instance before page is placed on the navigation stack allowing for asynchronous functionality.
        ///     By default, it refreshes the network connection status.
        /// </summary>
        public override async Task InitializeAsync(INavigationParameters parameters, int lineNumber = 0,
            string methodName = "",
            string filePath = "")
        {
            await base.InitializeAsync(parameters, lineNumber, methodName, filePath);

            if (parameters.IsBackNavigation)
            {
                return;
            }

            if (!parameters.ContainsKey(NavParamKeys.RequiredDemoNavParam))
            {
                throw new MissingRequiredNavParamException($"{nameof(NavParamKeys.RequiredDemoNavParam)} was not passed in and was expected");
            }

            parameters.TryGetValue<bool>(NavParamKeys.ThrowError, out var shouldThrowError);

            if (shouldThrowError)
            {
                throw new Exception($"This is an exception that was thrown within {nameof(InitializeAsync)}()");
            }
        }

        #endregion
    }
}