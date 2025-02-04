Branch Xamarin SDK change log

- 10.0.0
  * Update to Branch Android SDK 5.15.0
  * Update to Branch iOS SDK 3.8.0
  * Update to use .NET-7
  * Exposed new method `setConsumerProtectionAttributionLevel` to set CPP level

- 9.0.1
  * Fix iOS linker issue causing crashes on pre-iOS 14 devices
  * Update Branch iOS SDK 2.1.3 (unreleased version)

- 9.0.0
  * Initial support for MAUI and .Net6
  * Update to Branch Android SDK 5.4.0
  * Update to Branch iOS SDK 2.1.2

- 8.1.2
  * Update to Branch iOS SDK 1.45.2

- 8.1.1
  * Update to Branch iOS SDK 1.45.1

- 8.1.0
  * Update to Branch Android SDK 5.2.6
  * Update to Branch iOS SDK 1.45.0
  * Added new method for Branch Events, AddContentItems().
  * Added support for missing Branch Event types.
  
- 8.0.0
  * Update to Branch Android SDK 5.1.3 
  * Update to Branch iOS SDK 1.41.0
  * Bug fix for GAID. To enable GAID, add the "Xamarin.GooglePlayServices.Ads.Identifier" NuGet package. This is optional. The "117.0.1.5" release does not work on macOS. Use version "117.0.1.3" instead.
  * New dependency on "Xamarin.Android.Binding.InstallReferrer" NuGet package for install referrer. This is required.
  * Deprecated the "Debug" option. Use "EnableLogging = true;" for device side logs. Use Test Devices (https://help.branch.io/using-branch/docs/add-test-devices) to simulate installs.

- 7.0.7
  * Adding method UseLongerWaitForAppleSearchAds (for ios only)
  * Adding method IgnoreAppleSearchAdsTestData (for ios only)

- 7.0.6
  * Updating Branch to iOS version 0.35.0

- 7.0.3
  * Fixing InvalidCastException when adding custom data to branch event on IOS 
  * Fixing empty content items in branch event on IOS

- 7.0.2
  * Supporting 'customer_event_alias' field

- 7.0.1
  * Fixing plugin type value for Xamarin.IOS
  * Changing deprecated "iconURL" to "icon" tag in nuspec

- 7.0.0
  * Updating Branch to Android version 4.3.2 and iOS version 0.31.4
  * Adding support of Play Install Referrer API
  * Updating readme.md

- 6.0.2
  * Updating Branch to Android version 4.2.1 and iOS version 0.31.3

- 6.0.1
  * Updating Branch to iOS version 0.31.0

- 6.0.0
  * Updating Branch to Android version 4.2.0 and iOS version 0.30.1
  * Moving Xamarin SDK from PCL to .net standard 1.2

- 5.0.9
  * Updating Branch to Android version 4.1.0 and iOS version 0.29.1

- 5.0.8
  * Updating Branch to IOS version 0.28.1
  * Adding method `SetAlias(string alias)` in `BranchEvent` class
  * Deleting method `setAppleSearchAdsDebugMode()`

- 5.0.7
  * Updating Branch to Android version 4.0.0

- 5.0.6
  * Updating Branch to Android version 3.2.0 and iOS version 0.27.0
  * Fixing empty callback for Androi push notifications

- 5.0.5
  * Fixing #98 Extracting data embedded via HTTP API
  * Fixing #97 Purchase events not tracking on iOS with X.Forms

- 5.0.4
  * Updating Branch to iOS version 0.25.11

- 5.0.3
  * Updating Branch to Android version 3.0.3 and iOS version 0.25.10

- 5.0.2
  * Adding method SetAppleSearchAdsDebugMode (for ios only)
  * Adding method DelayInitToCheckForSearchAds (for ios only)

- 5.0.1
  * Updating Branch to Android version 3.0.1 and iOS version 0.25.8
  * Changing the api.branch.io endpoint to api2.branch.io
  * Android Min SDK version is now 16.
  * "+clicked_branch_link" property in BranchUniversalObject is the same now on Android and iOS ("true"/"false")

- 4.0.8
  * Updating Branch to Android version 2.19.5 and iOS version 0.25.6

- 4.0.7
  * Updating Branch to Android version 2.19.3 and iOS version 0.25.3

- 4.0.6
  * Updating Branch to Android version 2.19.0 and iOS version 0.25.2

- 4.0.5
  * Updating Branch to Android version 2.18.1
  * Adding method for disable/enable of tracking of analytics for the user
  * Updating readme file

- 4.0.4
  * Updating Branch to Android version 2.18.0 and iOS version 0.25.0

- 4.0.3
  * Updating Branch to Android version 2.17.0 and iOS version 0.24.1

- 4.0.2
  * Updating Branch to Android version 2.16.0 and iOS version 0.23.2

- 4.0.1
  * Updating Branch to Android version 2.15.0 and iOS version 0.22.5

- v4.0.0
  * Updating Branch to Android version 2.14.4 and iOS version 0.22.4
  * Updating BranchUniversalObject
  * Adding new class for metadata (BranchContentMetadata)
  * Adding API for sending branch events
  * Updating Readme

- v3.0.18
  * Updating Branch to iOS version 0.20.3
  * Adding method `SetRequestMetadata(string key, string val)`

- v3.0.17
  * Updating Branch to Android version 2.13.1

- v3.0.16
  * Updating Branch to Android version 2.12.2 and iOS version 0.20.2

- v3.0.15
  * Updating Branch to Android version 2.12.1 and iOS version 0.19.5

- v3.0.14
  * Updating Branch to Android version 2.12.0 and iOS version 0.18.8

- v3.0.13
  * Updating Branch to Android version 2.11.1 and iOS version 0.17.7

- v3.0.12
  * Updating Branch to iOS version 0.17.6

- v3.0.11
  * Updating Branch to Android version 2.10.3 and iOS version 0.17.5
  * Cleaning Examples projects

- v3.0.10
  * Updating Branch to Android version 2.10.2

- v3.0.9
  * Updating Branch to Android version 2.8.0 and iOS version 0.15.3
  * Adding canonicalUrl for BranchUniversalObject

- v3.0.8
  * Updating Branch to Android version 2.6.1 and iOS version 0.14.12

- v3.0.7
  * Updating Branch to Android version 2.6.0
  * Adding checking for dictionary in method UserCompletedAction

- v3.0.6
  * Updating Branch to Android version 2.5.9 and iOS version 0.13.5

- v3.0.5
  * Updating iOS native library to version 0.12.27
  * Updating Android native library to version 2.5.7
  * Deleting Microsoft.Net.Http dependence
  
- v3.0.4
  * Updating iOS native library to version 0.12.24
  * Updating Android native library to version 2.5.5

- v3.0.3
  * Updating iOS native library to version 0.12.19
  * Updating Android native library to version 2.5.2

- v3.0.2
  * Updating Android native library to version 2.5.1

- v3.0.1
  * Updating iOS native library to version 0.12.16
  * Updating Android native library to version 2.4.7
  
- v3.0.0
  * Updating iOS native library to version 0.12.11
  * Updating Android native library to version 2.4.5
  * Adding examples for iOS and Android applications
  * Adding ResetUserSession()
  * Adding ListOnSpotlight(BranchUniversalObject universalObject)
  * Deleting CloseSession ()
  * Deleting GetShortUrl (IBranchUrlInterface callback, Dictionary<String, dynamic> parameters = null, string channel = "", string stage = "", ICollection<String> tags = null, string feature = "", int duration = 0)
  * Deleting GetShortUrl (IBranchUrlInterface callback, int type = Constants.URL_TYPE_UNLIMITED, Dictionary<String, dynamic> parameters = null, string channel = "", string stage = "", ICollection<String> tags = null, string feature = "")

- v2.1.6
  * Updating iOS native library to version 0.12.3
  * Updating Android native library to version 1.14.3

- v2.1.5
  * Updating iOS native library to version 0.12.2
  * Updating Android native library to version 1.12.1
  * Fixing keywords and metadata in BUO
  * Fixing controls parameters in BLP

- v2.1.4
  * Updating iOS native library to version 0.12.1
  * Updating Android native library to version 1.11.3
  * Adding Init(Application app...) to Xamarin Android SDK

- v2.1.3
  * Fixing trouble with empty expired data string

- v2.1.2
  * Fixing non-string parameters

- v2.1.1
  * Fixing BranchUniversalObject expiration date

- v2.1.0
  * New version of Xamarin SDK based on native libraries. 

- v1.2.1
  * Debug field has been become static.

- v1.2.0
  * BranchAndroid.GetInstance() has been renamed BranchAndroid.getInstance().
  * Automatical session management has been added. Use AndroidBranch.SetLifeCycleHandlerCallback(Context, IBranchSessionInterface) to enable automatical sesiion management. Use Branch.AutoSessionEnabled to check what type of session management is used.
  * AndroidAppLink support added.
  * Android Push Notification support added.
  * iOS Universal Links suppor added.

- v1.1.0
  * Usage of app key is now deprecated. Use your Branch key, found in the settings page of your Dashboard if possible.
  * Adding ChangeLog.
