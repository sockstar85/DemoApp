using CoreM.Helpers;

namespace DemoApp.Pages;

public partial class NextPage
{
    #region Methods

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="NextPage" /> class.
    /// </summary>
    public NextPage()
    {
        InitializeComponent();
        BindingContext = this.GetBindingContext();
    }

    #endregion

    #endregion
}