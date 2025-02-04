using System;
using System.Collections;
using System.Collections.Generic;

namespace BranchSDK
{
	abstract public class Branch
	{
		public const string version = "10.0.0";

		#region Singleton

		protected static Branch branchInstance = null;

		protected Branch() { }

		public static Branch GetInstance()
		{
			if (branchInstance == null)
			{
				throw new BranchException("You must initialize Branch before you can use the Branch object!");
			}

			return branchInstance;
		}

		#endregion

		/// <summary>
		/// Enables Logging
		/// </summary>
		public static bool EnableLogging = false;

		[Obsolete("Debug is deprecated. Use EnableLogging to enable logging. Use Test Devices (https://help.branch.io/using-branch/docs/add-test-devices) to simulate installs.")]
		public static bool Debug = false;

		/// <summary>
		/// Gets or sets the Branch key. This is generally
		/// set by the platform specific Init methods.
		/// </summary>
		/// <value>The Branch key.</value>
		protected String branchKey { get; set; }

		protected List<Object> callbacksList = new List<object>();


		#region Session methods
		/// <summary>
		/// Initiate a Branch session.
		/// </summary>
		/// <param name="callback">The callback that is called once the request has completed.</param>
		public virtual void InitSession(IBranchSessionInterface callback) { callbacksList.Clear(); }

		/// <summary>
		/// Initiate a Branch session.
		/// </summary>
		/// <param name="callback">The callback that is called once the request has completed.</param>
		public virtual void InitSession(IBranchBUOSessionInterface callback) { callbacksList.Clear(); }

		/// <summary>
		/// Get the referring parameters from the last open.
		/// </summary>
		/// <returns>The referring parameters from the last open.</returns>
		abstract public Dictionary<String, object> GetLastReferringParams();

		/// <summary>
		/// Gets the branch universal object from last open.
		/// </summary>
		/// <returns>The last referring branch universal object.</returns>
		abstract public BranchUniversalObject GetLastReferringBranchUniversalObject();

		/// <summary>
		/// Gets link properties from last open.
		/// </summary>
		/// <returns>The last referring branch link properties.</returns>
		abstract public BranchLinkProperties GetLastReferringBranchLinkProperties();

		/// <summary>
		/// Get the referring parameters from the initial install.
		/// </summary>
		/// <returns>The referring parameters from the initial install.</returns>
		abstract public Dictionary<String, object> GetFirstReferringParams();

		/// <summary>
		/// Gets the branch universal object from initial install.
		/// </summary>
		/// <returns>The first referring branch universal object.</returns>
		abstract public BranchUniversalObject GetFirstReferringBranchUniversalObject();

		/// <summary>
		/// Gets link properties from initial install.
		/// </summary>
		/// <returns>The first referring branch link properties.</returns>
		abstract public BranchLinkProperties GetFirstReferringBranchLinkProperties();

		/// <summary>
		/// Notifies the native Branch layer to begin initializing.
		/// </summary>
		abstract public void NotifyNativeInit();

		#endregion

		#region Identity methods

		/// <summary>
		/// Reset the current session.
		/// </summary>
		abstract public void ResetUserSession();

		/// <summary>
		/// Set the user's identity to an ID used by your system, so that it is identifiable by you elsewhere.
		/// If you call setIdentity, this device will have that identity associated with this user until `logout` is called.
		/// This includes persisting through uninstalls, as we track device id.
		/// </summary>
		/// <param name="user">The ID Branch should use to identify this user.</param>
		/// <param name="callback">The callback that is called once the request has completed.</param>
		abstract public void SetIdentity(String user, IBranchIdentityInterface callback);

		/// <summary>
		/// Clear all of the current user's session items.
		/// </summary>
		/// <param name="callback">The callback that is called once the request has completed.</param>
		abstract public void Logout(IBranchIdentityInterface callback = null);

		#endregion


		#region Short Links methods

		/// <summary>
		/// Get a short url with specified universal object and link properties.
		/// </summary>
		/// <param name="callback">The callback that is called once the request has completed.</param>
		/// <param name="universalObject">Universal object.</param>
		/// <param name="linkProperties">Link properties.</param>
		abstract public void GetShortURL(IBranchUrlInterface callback,
										  BranchUniversalObject universalObject,
										  BranchLinkProperties linkProperties);

		#endregion


		#region Share Link methods

		/// <summary>
		/// Shares the link.
		/// </summary>
		/// <param name="callback">Callback.</param>
		/// <param name="universalObject">Universal object.</param>
		/// <param name="linkProperties">Link properties.</param>
		/// <param name="message">Message.</param>
		abstract public void ShareLink(IBranchLinkShareInterface callback,
										BranchUniversalObject universalObject,
										BranchLinkProperties linkProperties,
										string message);

		#endregion

		#region Send Event methods

		/// <summary>
		/// Send event.
		/// </summary>
		/// <param name="branchEvent">Event.</param>
		abstract public void SendEvent(BranchEvent branchEvent);

		#endregion

		#region Configuration methods

		/// <summary>
		/// Specify the time to wait in seconds between retries in the case of a Branch server error.
		/// </summary>
		/// <param name="retryInterval">Number of seconds to wait between retries.</param>
		abstract public void SetRetryInterval(int retryInterval);

		/// <summary>
		/// Specify the max number of times to retry in the case of a Branch server error.
		/// </summary>
		/// <param name="maxRetries">Number of retries to make.</param>
		abstract public void SetMaxRetries(int maxRetries);

		/// <summary>
		/// Specify the amount of time before a request should be considered "timed out".
		/// </summary>
		/// <param name="timeout">Number of seconds to before a request is considered timed out.</param>
		abstract public void SetNetworkTimeout(int timeout);

		/// <summary>
		/// Registers the view.
		/// </summary>
		/// <param name="universalObject">Universal object.</param>
		abstract public void RegisterView(BranchUniversalObject universalObject);

		/// <summary>
		/// Index the content on Spotlight Search.
		/// </summary>
		/// <param name="universalObject">Universal object.</param>
		abstract public void ListOnSpotlight(BranchUniversalObject universalObject);

		/// <summary>
		/// Key-value pairs to be included in the metadata on every request.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		abstract public void SetRequestMetadata(string key, string value);

		/// <summary>
		/// Disable/enable tracking of analytics for the user.
		/// </summary>
		/// <param name="value">Value.</param>
		abstract public void SetTrackingDisabled(bool value);


		/// <summary>
		/// Set DMA parameters for EEA region.
		/// </summary>
		/// <param name="eeaRegion">Whether the region is EEA.</param>
		/// <param name="adPersonalizationConsent">Ad personalization consent.</param>
		/// <param name="adUserDataUsageConsent">Ad user data usage consent.</param>
		abstract public void SetDMAParamsForEEA(bool eeaRegion, bool adPersonalizationConsent, bool adUserDataUsageConsent);



		#endregion

		#region Consumer Protection methods

		/// <summary>
		/// Set the consumer protection attribution level.
		/// </summary>
		/// <param name="level">The attribution level to set.</param>
		abstract public void setConsumerProtectionAttributionLevel(BranchAttributionLevel level);

		#endregion
	}
}

