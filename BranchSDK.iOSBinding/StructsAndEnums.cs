using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;

namespace IOSNativeBranch
{

    [Native]
    public enum BNCErrorCode : long
    {
        InitError = 1000,
        DuplicateResourceError = 1001,
        BadRequestError = 1003,
        ServerProblemError = 1004,
        NilLogError = 1005,
        VersionError = 1006,
        NetworkServiceInterfaceError = 1007,
        InvalidNetworkPublicKeyError = 1008,
        ContentIdentifierError = 1009,
        SpotlightNotAvailableError = 1010,
        SpotlightTitleError = 1011,
        SpotlightIdentifierError = 1013,
        SpotlightPublicIndexError = 1014,
        TrackingDisabledError = 1015,
        GeneralError = 1016,
        HighestError
    }

    [Native]
    public enum BranchLinkType : ulong
    {
        UnlimitedUse = 0,
        OneTimeUse = 1
    }

    [Native]
    public enum BranchLogLevel : ulong
    {
        Verbose,
        Debug,
        Warning,
        Error
    }

    [Native]
    public enum BNCViewControllerPresentationOption : long
    {
        Show,
        Push,
        Present
    }

    [Native]
    public enum BranchContentIndexMode : long
    {
        Public,
        Private
    }

    [Native]
    public enum BranchEventAdType : long
    {
        None,
        Banner,
        Interstitial,
        RewardedVideo,
        Native
    }

    [Native]
    public enum BranchQRCodeImageFormat : long
    {
        Png,
        Jpeg
    }
}


