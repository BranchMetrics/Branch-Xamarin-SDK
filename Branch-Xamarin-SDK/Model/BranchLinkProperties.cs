﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BranchLinkProperties {

	public List<String> tags;
	public string feature;
	public string alias;
	public string channel;
	public string stage;
	public int matchDuration;
	public Dictionary<String, String> controlParams;


	public BranchLinkProperties() {
		tags =  new List<String>();
		feature = "";
		alias = "";
		channel = "";
		stage = "";
		matchDuration = 0;
		controlParams = new Dictionary<String, String>();
	}

	public BranchLinkProperties(string json) {
		tags =  new List<String>();
		feature = "";
		alias = "";
		channel = "";
		stage = "";
		matchDuration = 0;
		controlParams = new Dictionary<String, String>();

		loadFromJson(json);
	}

	public BranchLinkProperties(Dictionary<string, object> data) {
		tags =  new List<String>();
		feature = "";
		alias = "";
		channel = "";
		stage = "";
		matchDuration = 0;
		controlParams = new Dictionary<String, String>();
		
		loadFromDictionary(data);
	}

	public void loadFromJson(string json) {
		if (string.IsNullOrEmpty(json))
			return;

		var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
		loadFromDictionary(data);
	}

	public void loadFromDictionary(Dictionary<string, object> data) {
		if (data == null)
			return;

		if (data.ContainsKey("~tags")) {
			tags = data["~tags"] as List<String>;
		}
		if (data.ContainsKey("~feature")) {
			feature = data["~feature"].ToString();
		}
		if (data.ContainsKey("~alias")) {
			alias = data["~alias"].ToString();
		}
		if (data.ContainsKey("~channel")) {
			channel = data["~channel"].ToString();
		}
		if (data.ContainsKey("~stage")) {
			stage = data["~stage"].ToString();
		}
		if (data.ContainsKey("~duration")) {
			if (!string.IsNullOrEmpty(data["~duration"].ToString())) {
				matchDuration = Convert.ToInt32(data["~duration"].ToString());
			}
		}
		if (data.ContainsKey("control_params")) {
			if (data["control_params"] != null) {
				IDictionary<string, object> paramsTemp = data["control_params"] as IDictionary<string, object>;

				if (paramsTemp != null) {
					foreach(string key in paramsTemp.Keys) {
						controlParams.Add(key, paramsTemp[key].ToString());
					}
				}
			}
		}
	}

	public Dictionary<string, object> ToDictionary() {
		var data = new Dictionary<string, object>();

		data.Add("~tags", tags);
		data.Add("~feature", feature);
		data.Add("~alias", alias);
		data.Add("~channel", channel);
		data.Add("~stage", stage);
		data.Add("~duration", matchDuration.ToString());
		data.Add("control_params", controlParams);

		return data;
	}

	public string ToJsonString() {
		var data = ToDictionary ();
		return JsonConvert.SerializeObject (data);
	}
}
