using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;
using DemoApp.Localization;

namespace DemoApp.Pages
{
    /// <summary>
    ///     The view model for <see cref="MainPage" />.
    /// </summary>
    public class MainPageViewModel : BasePageViewModel
    {
        #region Fields

        private int _count;
        private string? _counterBtnText;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the button click command.
        /// </summary>
        public ICommand ButtonClickCommand => new Command(HandleButtonClicked);

        /// <summary>
        ///     Gets/sets counter button text.
        /// </summary>
        public string? CounterButtonText
        {
            get => _counterBtnText;
            set => SetProperty(ref _counterBtnText, value);
        }

        /// <summary>
        ///     Gets the next page command.
        /// </summary>
        public IAsyncRelayCommand NextPageCommand => new AsyncRelayCommand(NavigateToNextPageAsync);

        /// <summary>
        ///     Gets the next page with error command.
        /// </summary>
        public IAsyncRelayCommand NextPageErrorCommand => new AsyncRelayCommand(NavigateToNextPageWithErrorAsync);

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        /// <param name="navigation">The navigation.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="connectivityManager">The connectivity manager.</param>
        /// <param name="logger">The logger.</param>
        public MainPageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger) : base(navigation, dialogService, connectivityManager, logger)
        {
        }

        #endregion

        /// <summary>
        ///     Quits the application after confirmation.
        /// </summary>
        public override async Task HandleBackButtonOverrideAsync()
        {
            var confirmQuit = await DialogService.DisplayAlertAsync(
                AppResource.BackButtonConfirmationTitle,
                AppResource.CloseAppConfirmation,
                AppResource.Yes,
                AppResource.No);

            if (confirmQuit)
            {
                Application.Current?.Quit();
            }
        }

        /// <summary>
        ///     Called when a page is navigated to just after being placed on the navigation stack.
        /// </summary>
        public override async Task OnNavigatedToAsync(
            INavigationParameters parameters,
            int lineNumber = 0,
            string methodName = "",
            string filePath = "")
        {
            await base.OnNavigatedToAsync(parameters, lineNumber, methodName, filePath);

            CounterButtonText = AppResource.TestButtonText;
        }

        /// <summary>
        ///     Handles the button clicked.
        /// </summary>
        private void HandleButtonClicked()
        {
            _count++;

            CounterButtonText = _count == 1
                ? $"Clicked {_count} time"
                : $"Clicked {_count} times";

            SemanticScreenReader.Announce(CounterButtonText);
        }

        /// <summary>
        ///     Navigates to next page asynchronous.
        /// </summary>
        private async Task NavigateToNextPageAsync()
        {
            //In NextPageViewModel.InitializeAsync() we enforce
            //NavParamKey.RequiredDemoNavParam being passed in
            //so we'll send it along so it doesn't throw an exception.
            var navParams = new NavigationParameters
            {
                { NavParamKeys.RequiredDemoNavParam, true }
            };

            var navResult = await Navigation.PushAsync(typeof(NextPage), navParams);

            if (!navResult.Success)
            {
                await HandleFailedNavigationAsync(navResult.Exception);
            }
        }

        /// <summary>
        ///     Navigates to next page with error asynchronous.
        /// </summary>
        private async Task NavigateToNextPageWithErrorAsync()
        {
            //In NextPageViewModel.InitializeAsync() we enforce
            //NavParamKey.RequiredDemoNavParam being passed in
            //so we'll send it along so it doesn't throw an exception.
            //We'll also send another parameter that can be used in
            //the next page's view model's lifecycle events.
            var navParameters = new NavigationParameters
            {
                { NavParamKeys.ThrowError, true },
                { NavParamKeys.RequiredDemoNavParam, true }
            };

            var navResult = await Navigation.PushAsync(typeof(NextPage), navParameters);

            if (!navResult.Success)
            {
                await HandleFailedNavigationAsync(navResult.Exception);
            }
        }

        #endregion
    }
}