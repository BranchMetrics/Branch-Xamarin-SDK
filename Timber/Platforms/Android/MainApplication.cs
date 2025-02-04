using Android.App;
using Android.Runtime;
using BranchSDK;

namespace Timber;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        Branch.EnableLogging = true;
        BranchAndroid.GetAutoInstance(ApplicationContext);
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

