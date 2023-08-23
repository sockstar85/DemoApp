using CoreM.Logging;
using CoreM.Managers;
using CoreM.Navigation;
using CoreM.Services;

namespace DemoApp.Pages
{
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
        /// <param name="resourceManager">The resource manager.</param>
        public NextPageViewModel(
            IExtendedNavigationService navigation,
            IDialogService dialogService,
            IConnectivityManager connectivityManager,
            IExtendedLogger logger,
            ILocalizationService resourceManager)
            : base(navigation, dialogService, connectivityManager, logger, resourceManager)
        {
        }

        /// <summary>
        ///     Handles the on back button pressed asynchronous.
        /// </summary>
        public override async Task HandleBackButtonOverrideAsync()
        {
            var confirm = await DialogService.DisplayAlertAsync("Are you sure?", "Are you sure you want to go back?", "Yes", "No");

            if (confirm)
            {
                await base.HandleBackButtonOverrideAsync();
            }
        }

        #endregion

        #endregion
    }
}