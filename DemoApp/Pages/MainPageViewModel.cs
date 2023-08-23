using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;

namespace DemoApp.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        #region Fields

        private int _count;
        private string _counterBtnText;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the button click command.
        /// </summary>
        public ICommand ButtonClickCommand => new Command(HandleButtonClicked);

        /// <summary>
        ///     Gets/sets counter button text.
        /// </summary>
        public string CounterButtonText
        {
            get => _counterBtnText;
            set => SetProperty(ref _counterBtnText, value);
        }

        /// <summary>
        ///     Gets the next page command.
        /// </summary>
        public IAsyncRelayCommand NextPageCommand => new AsyncRelayCommand(NavigateToNextPageAsync);

        private async Task NavigateToNextPageAsync()
        {
            var navResult = await Navigation.PushAsync(typeof(NextPage));

            if (!navResult.Success)
            {
                await HandleFailedNavigationAsync(navResult.Exception);
            }
        }

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
        /// <param name="localizationService">The resource manager.</param>
        public MainPageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger,
            ILocalizationService localizationService) : base(navigation, dialogService, connectivityManager, logger,
            localizationService)
        {
        }

        #endregion

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

            CounterButtonText = ResourceManager.GetString("TestButtonText");
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

        #endregion
    }
}