using Android.App;
using Android.Runtime;

namespace DemoApp;

[Application]
public class MainApplication : MauiApplication
{
    #region Methods

    #region Constructors

    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    #endregion

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    #endregion
}