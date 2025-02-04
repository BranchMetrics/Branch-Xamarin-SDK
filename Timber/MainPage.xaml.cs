using BranchSDK;

namespace Timber;

public partial class MainPage : ContentPage
{
    private bool isTrackingDisabled = false;

    public MainPage()
    {
        InitializeComponent();
        Branch.EnableLogging = true;
    }

    private void OnGetLastReferringParamsClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetLastReferringParams();
        DisplayAlert("GetLastReferringParams", FormatDictionary(result), "OK");
    }

    private void OnGetLastReferringBranchUniversalObjectClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetLastReferringBranchUniversalObject();
        DisplayAlert("GetLastReferringBranchUniversalObject", result.ToJsonString(), "OK");
    }

    private void OnGetLastReferringBranchLinkPropertiesClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetLastReferringBranchLinkProperties();
        DisplayAlert("GetLastReferringBranchLinkProperties", result.ToJsonString(), "OK");
    }

    private void OnGetFirstReferringParamsClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetFirstReferringParams();
        DisplayAlert("GetFirstReferringParams", FormatDictionary(result), "OK");
    }

    private void OnGetFirstReferringBranchUniversalObjectClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetFirstReferringBranchUniversalObject();
        DisplayAlert("GetFirstReferringBranchUniversalObject", result.ToJsonString(), "OK");
    }

    private void OnGetFirstReferringBranchLinkPropertiesClicked(object sender, EventArgs e)
    {
        var result = Branch.GetInstance().GetFirstReferringBranchLinkProperties();
        DisplayAlert("GetFirstReferringBranchLinkProperties", result.ToJsonString(), "OK");
    }

    private void OnResetUserSessionClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().ResetUserSession();
        DisplayAlert("ResetUserSession", "User session reset", "OK");
    }

    private void OnSetIdentityClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetIdentity("test_user", new BranchIdentityCallback(this));
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().Logout(new BranchIdentityCallback(this));
    }

    private void OnGetShortURLClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().GetShortURL(new BranchUrlCallback(this), new BranchUniversalObject(), new BranchLinkProperties());
    }

    private void OnShareLinkClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().ShareLink(new BranchLinkShareCallback(this), new BranchUniversalObject(), new BranchLinkProperties(), "Check this out!");
    }


    private void OnSendEventClicked(object sender, EventArgs e)
    {
        SendEvent();
    }

    private void OnSetRetryIntervalClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetRetryInterval(5);
        DisplayAlert("SetRetryInterval", "Retry interval set", "OK");
    }

    private void OnSetMaxRetriesClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetMaxRetries(3);
        DisplayAlert("SetMaxRetries", "Max retries set", "OK");
    }

    private void OnSetNetworkTimeoutClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetNetworkTimeout(10);
        DisplayAlert("SetNetworkTimeout", "Network timeout set", "OK");
    }

    private void OnRegisterViewClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().RegisterView(new BranchUniversalObject());
        DisplayAlert("RegisterView", "View registered", "OK");
    }

    private void OnListOnSpotlightClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().ListOnSpotlight(new BranchUniversalObject());
        DisplayAlert("ListOnSpotlight", "Listed on Spotlight", "OK");
    }

    private void OnSetRequestMetadataClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetRequestMetadata("key", "value");
        DisplayAlert("SetRequestMetadata", "Request metadata set", "OK");
    }

    private void OnSetTrackingDisabledClicked(object sender, EventArgs e)
    {
        isTrackingDisabled = !isTrackingDisabled; // Toggle the tracking state
        Branch.GetInstance().SetTrackingDisabled(isTrackingDisabled);

        string status = isTrackingDisabled ? "disabled" : "enabled";
        DisplayAlert("SetTrackingDisabled", $"Tracking {status}", "OK");
    }

    private void OnSetDMAParamsForEEAClicked(object sender, EventArgs e)
    {
        Branch.GetInstance().SetDMAParamsForEEA(true, false, true);
        DisplayAlert("SetDMAParamsForEEA", "DMA Params set for EEA", "OK");
    }

    void SendEvent()
    {
        BranchEvent branchEvent = new BranchEvent(BranchEventType.PURCHASE);
        branchEvent.SetAlias("new_user_purchase");
        branchEvent.SetRevenue((float)1.5);
        branchEvent.SetShipping((float)10.5);
        branchEvent.SetCurrency(BranchCurrencyType.USD);
        branchEvent.SetTax((float)12.3);

        BranchUniversalObject buo1 = new BranchUniversalObject
        {
            canonicalIdentifier = "id12345",
            title = "id12345 title",
            contentDescription = "ITEM 1"
        };
        BranchUniversalObject buo2 = new BranchUniversalObject
        {
            canonicalIdentifier = "id12345",
            title = "id12345 title",
            contentDescription = "ITEM 2"
        };

        BranchContentMetadata metadata = new BranchContentMetadata();
        metadata.price = (float)99;
        metadata.AddCustomMetadata("key1", "value1");
        metadata.quantity = 999;

        buo1.metadata = metadata;
        buo2.metadata.productCategory = BranchProductCategory.BABY_AND_TODDLER;

        List<BranchUniversalObject> list = new List<BranchUniversalObject> { buo1, buo2 };
        branchEvent.AddContentItems(list);

        Branch.GetInstance().SendEvent(branchEvent);
        DisplayAlert("SendEvent", "Event sent", "OK");
    }


    // Callbacks
    public class BranchIdentityCallback : IBranchIdentityInterface
    {
        private readonly MainPage _mainPage;

        public BranchIdentityCallback(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        public void IdentitySet(Dictionary<string, object> data)
        {
            _mainPage.DisplayAlert("SetIdentity", "Identity set successfully", "OK");
        }

        public void LogoutComplete()
        {
            _mainPage.DisplayAlert("Logout", "Successfully logged out", "OK");
        }

        public void IdentityRequestError(BranchError error)
        {
            // Handle error
            _mainPage.DisplayAlert("SetIdentity", $"Error setting identity: {error.ErrorMessage}", "OK");
        }
    }

    public class BranchUrlCallback : IBranchUrlInterface
    {
        private readonly MainPage _mainPage;

        public BranchUrlCallback(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        public void ReceivedUrl(string uri)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _mainPage.DisplayAlert("GetShortURL", $"Short URL: {uri}", "OK");
            });
        }

        public void UrlRequestError(BranchError error)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _mainPage.DisplayAlert("GetShortURL", $"Error: {error.ErrorMessage}", "OK");
            });
        }
    }

    public class BranchLinkShareCallback : IBranchLinkShareInterface
    {
        private readonly MainPage _mainPage;

        public BranchLinkShareCallback(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        public void ChannelSelected(string channel)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _mainPage.DisplayAlert("ShareLink", $"Channel Selected: {channel}", "OK");
            });
        }

        public void LinkShareResponse(string sharedLink, string sharedChannel)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _mainPage.DisplayAlert("ShareLink", $"Shared Link: {sharedLink}\nChannel: {sharedChannel}", "OK");
            });
        }

        public void LinkShareRequestError(BranchError error)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _mainPage.DisplayAlert("ShareLink", $"Error: {error.ErrorMessage}", "OK");
            });
        }
    }

    //Helper Methods
    private string FormatDictionary(Dictionary<string, object> dict)
    {
        return string.Join("\n", dict.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
    }
}
