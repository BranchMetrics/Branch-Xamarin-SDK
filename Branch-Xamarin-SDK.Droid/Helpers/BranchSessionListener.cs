﻿using System;
using System.Collections.Generic;
using Org.Json;
using Newtonsoft.Json;
using BranchXamarinSDK;
using BranchXamarinSDK.Droid;

namespace BranchXamarinSDK.Droid
{
	public class BranchSessionListener: Java.Lang.Object, IO.Branch.Referral.AndroidNativeBranch.IBranchReferralInitListener
	{
		private IBranchSessionInterface callback = null;

		public BranchSessionListener(IBranchSessionInterface callback) {
			this.callback = callback;
		}

		public void OnInitFinished (JSONObject data, IO.Branch.Referral.BranchError error) {
			if (callback == null) {
				return;
			}

			if (error == null) {

				callback.InitSessionComplete (BranchAndroidUtils.ToDictionary(data));
			} else {

				BranchError err = new BranchError (error.Message, error.ErrorCode);
				callback.SessionRequestError (err);
			}
		}
	}
}

