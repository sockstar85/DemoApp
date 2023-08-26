using DemoApp.Pages;

namespace DemoApp;

/// <summary>
///     The application.
/// </summary>
public partial class App
{
    #region Methods

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="App" /> class.
    /// </summary>
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }

    #endregion

    #endregion
}