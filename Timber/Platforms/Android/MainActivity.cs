using Android.App;
using Android.Content.PM;
using Android.OS;
using BranchSDK;
using Android.Content;

namespace Timber;

[IntentFilter(new[] { "android.intent.action.VIEW" },
Categories = new[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
DataScheme = "timber",
DataHost = "open")]

[IntentFilter(new[] { "android.intent.action.VIEW" },
Categories = new[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
DataScheme = "https",
DataHost = "timber.app.link")]

/*
 * Update the Activity
 * 
 * Add a "Name" so this Activity can be located by name.
 * Set the LaunchMode, singleTop or singleTask is recommended.
 * 
 * Implement IBranchSessionInterface to get Branch Payloads
 * 
 */
[Activity(Name = "io.branch.timber.MainActivity", Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IBranchSessionInterface
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        LogMessage("BRANCHSDK .NET MAUI: MainActivity.OnCreate");
        BranchAndroid.Init(this, "key_live_nf8w3l1WBpzWdlC00KsLNdmbuEccK6Yr", this);
    }

    protected override void OnNewIntent(Intent intent)
    {
        LogMessage("BRANCHSDK .NET MAUI: MainActivity.OnNewIntent");
        base.OnNewIntent(intent);
        intent.PutExtra("branch_force_new_session", true);
        Intent = intent;
    }

    public void InitSessionComplete(Dictionary<string, object> data)
    {
        if (data == null)
        {
            LogMessage("BRANCHSDK .NET MAUI: InitSessionComplete with data: null");
        }
        else
        {
            var dataString = string.Join(", ", data.Select(kvp => $"{kvp.Key}: {kvp.Value?.ToString() ?? "null"}"));
            LogMessage($"BRANCHSDK .NET MAUI: InitSessionComplete with data: {dataString}");
        }
    }


    public void SessionRequestError(BranchError error)
    {
        LogMessage("BRANCHSDK .NET MAUI: SessionRequestError: ");
        LogMessage("BRANCHSDK .NET MAUI: Error Message: " + error.ErrorMessage);
        LogMessage("BRANCHSDK .NET MAUI: Error Code: " + error.ErrorCode);
    }

    void LogMessage(string message)
    {
        Console.WriteLine(message);
    }
}

