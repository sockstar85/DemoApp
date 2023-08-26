using CoreM.Helpers;
using CoreM.Navigation;

namespace DemoApp.Pages;

/// <summary>
///     The main page.
/// </summary>
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
    /// <remarks>
    ///     NOTE: This is here because normally navigation occurs
    ///     through <see cref="IExtendedNavigationService"/> which
    ///     would call <see cref="NavigatableViewModel"/> lifecycle
    ///     events. This page got set in <c>App.xaml.cs</c>.
    /// </remarks>
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