using CoreM.Helpers;
using CoreM.Navigation;

namespace DemoApp.Pages;

public partial class MainPage
{
    #region Methods

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainPage" /> class.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this.GetBindingContext();
    }

    #endregion

    /// <summary>
    ///     Called when appearing.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is NavigatableViewModel viewModel)
        {
            viewModel.OnNavigatedToAsync(new NavigationParameters());
        }
    }

    #endregion
}