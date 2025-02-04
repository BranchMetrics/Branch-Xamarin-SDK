using System;
using Android.OS;
using Android.App;

namespace BranchSDK
{
	public class BranchAndroidLifeCycleHandler : Java.Lang.Object, Android.App.Application.IActivityLifecycleCallbacks
	{
		private static int activeCounter;
		public IBranchSessionInterface callback { get; set; }
		public IBranchBUOSessionInterface callbackBUO { get; set; }

		public BranchAndroidLifeCycleHandler(IBranchSessionInterface callback = null)
		{
			this.callback = callback;
			this.callbackBUO = null;
		}

		public BranchAndroidLifeCycleHandler(IBranchBUOSessionInterface callback = null)
		{
			this.callback = null;
			this.callbackBUO = callback;
		}

		public void OnActivityCreated(Activity activity, Bundle savedInstanceState) { }
		public void OnActivityDestroyed(Activity activity) { }
		public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }
		public void OnActivityResumed(Activity activity) { }
		public void OnActivityPaused(Activity activity) { }

		public void OnActivityStarted(Activity activity)
		{
			Console.WriteLine("BranchSDK .NET MAUI: OnActivityStarted: " + activity.LocalClassName);
			BranchAndroid.getInstance().CurrActivity = activity;
			IncreaseActivityCounter(activity);
		}

		public void OnActivityStopped(Activity activity)
		{
			Console.WriteLine("BranchSDK .NET MAUI: OnActivityStopped: " + activity.LocalClassName);
			DecreaseActivityCounter();
		}

		private void IncreaseActivityCounter(Activity activity)
		{
			if (activeCounter == 0)
			{
				if (callback != null)
				{
					BranchAndroid.getInstance().InitSession(callback);
				}
				else if (callbackBUO != null)
				{
					BranchAndroid.getInstance().InitSession(callbackBUO);
				}
			}

			activeCounter++;
		}

		private void DecreaseActivityCounter()
		{
			if (activeCounter > 0)
			{
				activeCounter--;
			}
		}
	}
}

