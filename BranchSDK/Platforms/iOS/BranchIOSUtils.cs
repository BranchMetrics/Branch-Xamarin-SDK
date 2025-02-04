using System;
using System.Collections.Generic;
using Foundation;
using Newtonsoft.Json;

namespace BranchSDK.iOS
{
	public static class BranchIOSUtils
	{
		public static Dictionary<string, object> ToDictionary(NSDictionary data) {
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			string strKey = "";

			if (data != null) {
				foreach (NSObject obj in data.Keys) {

					if (obj != null) {
						strKey = obj.ToString();

						if (strKey.Equals("metadata") || strKey.Equals("control_params")) {

							Dictionary<string, object> dictTemp = new Dictionary<string, object>();
							NSDictionary nsDict = data.ValueForKey((NSString)strKey) as NSDictionary;

							if (nsDict != null) {
								foreach (var keyObj in nsDict) {
									dictTemp.Add(keyObj.Key.ToString(), keyObj.Value.ToString());
								}
							}

							dict.Add(strKey, dictTemp);
						}
						else if (strKey.Equals("$keywords") || strKey.Equals("~tags")) {
							
							List<object> listTemp = new List<object>();
							NSArray array = data.ValueForKey((NSString)strKey) as NSArray;

							if (array != null) {
								for (uint i = 0; i < array.Count; ++i) {
									listTemp.Add(array.GetItem<NSObject>(i) as object);
								}
							}
							dict.Add(strKey, listTemp);
						}
						else {
							dict.Add(strKey, data.ValueForKey((NSString)strKey));
						}
					}
				}
			}

			return dict;
		}

		public static Dictionary<string, object> ToDictionary(IOSNativeBranch.BranchUniversalObject universalObject) {

			if (universalObject == null) {
				return new Dictionary<string, object> ();
			}

            var keywordsList = new List<object>();
            if (universalObject.Keywords != null) {
                foreach (string obj in universalObject.Keywords) {
                    keywordsList.Add(obj);
                }
            }

            NSError error = null;
            NSData jsonData = NSJsonSerialization.Serialize(universalObject.ContentMetadata.Dictionary, 0, out error);

            string jsonStr = jsonData.ToString();
            Dictionary<string, object> metadataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

            var dict = new Dictionary<string, object>();
            dict.Add("$canonical_identifier", universalObject.CanonicalIdentifier != null ? universalObject.CanonicalIdentifier : "");
            dict.Add("$canonical_url", universalObject.CanonicalUrl != null ? universalObject.CanonicalUrl : "");
            dict.Add("$og_title", universalObject.Title != null ? universalObject.Title : "");
            dict.Add("$og_description", universalObject.ContentDescription != null ? universalObject.ContentDescription : "");
            dict.Add("$og_image_url", universalObject.ImageUrl != null ? universalObject.ImageUrl : "");
            dict.Add("$publicly_indexable", universalObject.PubliclyIndex ? "0" : "1");
            dict.Add("$locally_indexable", universalObject.LocallyIndex ? "0" : "1");
            dict.Add("$exp_date", universalObject.ExpirationDate != null ? (universalObject.ExpirationDate.SecondsSinceReferenceDate * 1000).ToString() : "");

            if (keywordsList != null) {
                dict.Add("$keywords", keywordsList);
            }

            if (metadataDict != null) {
                dict.Add("metadata", metadataDict);
            }

            return dict;
		}

		public static Dictionary<string, object> ToDictionary(IOSNativeBranch.BranchLinkProperties linkProperties) {

			if (linkProperties == null) {
				return new Dictionary<string, object> ();
			}

			NSString[] keys = new NSString[] {
				(NSString)"~tags",
				(NSString)"~feature",
				(NSString)"~alias",
				(NSString)"~channel",
				(NSString)"~stage",
				(NSString)"~duration",
				(NSString)"control_params"
			};

			NSObject[] values = new NSObject[] {
                NSObject.FromObject(linkProperties.Tags != null ? NSArray.FromNSObjects(linkProperties.Tags) : null as object),
				NSObject.FromObject(linkProperties.Feature != null ? linkProperties.Feature : "" as object),
				NSObject.FromObject(linkProperties.Alias != null ? linkProperties.Alias : "" as object),
				NSObject.FromObject(linkProperties.Channel != null ? linkProperties.Channel : "" as object),
				NSObject.FromObject(linkProperties.Stage != null ? linkProperties.Stage : "" as object),
				NSObject.FromObject(linkProperties.MatchDuration.ToString () as object),
				NSObject.FromObject(linkProperties.ControlParams != null ? linkProperties.ControlParams : "" as object)
			};

			NSDictionary dict = NSDictionary.FromObjectsAndKeys (values, keys);

			return ToDictionary (dict);
		}

		public static NSDictionary ToNSDictionary(Dictionary<string, object> data) {
			NSDictionary dict;

			if (data != null) {

				int i = 0;
				NSString[] keys = new NSString[data.Count];
				NSObject[] values = new NSObject[data.Count];

				foreach (string key in data.Keys) {
					keys [i] = new NSString (key);
					values[i] = NSObject.FromObject(data[key]);
					++i;
				}

				dict = NSDictionary.FromObjectsAndKeys (values, keys);
			}
			else {
				dict = new NSDictionary ();
			}

			return dict;
		}

		public static NSObject[] ToNSObjectArray (ICollection<string> data) {
			NSObject[] items = null;

			if (data != null) {
				int index = 0;
				items = new NSObject[data.Count];

				foreach (string str in data)
				{
					items[index] = NSObject.FromObject(str as object);
					++index;
				}
			}

			return items;
		}

		public static IOSNativeBranch.BranchUniversalObject ToNativeUniversalObject(BranchUniversalObject obj) {
			var res = new IOSNativeBranch.BranchUniversalObject ();

			if (obj != null) {

                res.Keywords = obj.keywords.ToArray();
				res.CanonicalIdentifier = obj.canonicalIdentifier != null ? obj.canonicalIdentifier : "";
				res.CanonicalUrl = obj.canonicalUrl != null ? obj.canonicalUrl : "";
				res.Title = obj.title != null ? obj.title : "";
				res.ContentDescription = obj.contentDescription != null ? obj.contentDescription : "";
				res.ImageUrl = obj.imageUrl != null ? obj.imageUrl : "";
                res.PubliclyIndex = obj.contentIndexMode == 0;
                res.LocallyIndex = obj.localIndexMode == 0;

				if (obj.expirationDate.HasValue) {
                    DateTime reference = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(2001, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
                    res.ExpirationDate = NSDate.FromTimeIntervalSinceReferenceDate((obj.expirationDate.Value - reference).TotalSeconds);
				}

                if (obj.metadata != null) {
                    foreach(string key in obj.metadata.Metadata.Keys) {
                        if (key.Equals("$content_schema")) {
                            res.ContentMetadata.ContentSchema = obj.metadata.Metadata["$content_schema"].ToString();
                        }
                        if (key.Equals("$quantity")) {
                            res.ContentMetadata.Quantity = Convert.ToDouble(obj.metadata.Metadata["$quantity"].ToString());
                        }
                        if (key.Equals("$price")) {
                            res.ContentMetadata.Price = new NSDecimalNumber(obj.metadata.Metadata["$price"].ToString());
                        }
                        if (key.Equals("$currency")) {
                            res.ContentMetadata.Currency = obj.metadata.Metadata["$currency"].ToString();
                        }
                        if (key.Equals("$sku")) {
                            res.ContentMetadata.Sku = obj.metadata.Metadata["$sku"].ToString();
                        }
                        if (key.Equals("$product_name")) {
                            res.ContentMetadata.ProductName = obj.metadata.Metadata["$product_name"].ToString();
                        }
                        if (key.Equals("$product_brand")) {
                            res.ContentMetadata.ProductBrand = obj.metadata.Metadata["$product_brand"].ToString();
                        }
                        if (key.Equals("$product_category")) {
                            res.ContentMetadata.ProductCategory = obj.metadata.Metadata["$product_category"].ToString();
                        }
                        if (key.Equals("$condition")) {
                            res.ContentMetadata.Condition = obj.metadata.Metadata["$condition"].ToString();
                        }
                        if (key.Equals("$product_variant")) {
                            res.ContentMetadata.ProductVariant = obj.metadata.Metadata["$product_variant"].ToString();
                        }
                        if (key.Equals("$rating_average")) {
                            res.ContentMetadata.RatingAverage = Convert.ToDouble(obj.metadata.Metadata["$rating_average"].ToString());
                        }
                        if (key.Equals("$rating_count")) {
                            // TODO: works with .Net 7, does not work with .Net 6
                            //res.ContentMetadata.RatingCount = new nint(Convert.ToInt32(obj.metadata.Metadata["$rating_count"].ToString()));
                        }
                        if (key.Equals("$rating_max")) {
                            res.ContentMetadata.RatingMax = Convert.ToDouble(obj.metadata.Metadata["$rating_max"].ToString());
                        }
                        if (key.Equals("$address_street")) {
                            res.ContentMetadata.AddressStreet = obj.metadata.Metadata["$address_street"].ToString();
                        }
                        if (key.Equals("$address_city")) {
                            res.ContentMetadata.AddressCity = obj.metadata.Metadata["$address_city"].ToString();
                        }
                        if (key.Equals("$address_region")) {
                            res.ContentMetadata.AddressRegion = obj.metadata.Metadata["$address_region"].ToString();
                        }
                        if (key.Equals("$address_country")) {
                            res.ContentMetadata.AddressCountry = obj.metadata.Metadata["$address_country"].ToString();
                        }
                        if (key.Equals("$address_postal_code")) {
                            res.ContentMetadata.AddressPostalCode = obj.metadata.Metadata["$address_postal_code"].ToString();
                        }
                        if (key.Equals("$latitude")) {
                            res.ContentMetadata.Latitude = Convert.ToDouble(obj.metadata.Metadata["$latitude"].ToString());
                        }
                        if (key.Equals("$longitude")) {
                            res.ContentMetadata.Longitude = Convert.ToDouble(obj.metadata.Metadata["$longitude"].ToString());
                        }
                    }

                    foreach (string val in obj.metadata.GetImageCaptions()) {
                        res.ContentMetadata.ImageCaptions.Add((NSString)val);
                    }

                    foreach (string key in obj.metadata.GetCustomMetadata().Keys) {
                        res.ContentMetadata.CustomMetadata.Add((NSString)key, (NSString)obj.metadata.GetCustomMetadata()[key]);
                    }
                }
			}

			return res;
		}

		public static IOSNativeBranch.BranchLinkProperties ToNativeLinkProperties(BranchLinkProperties obj) {
			var res = new IOSNativeBranch.BranchLinkProperties ();

			if (obj != null) {

				if (obj.controlParams != null) {
					foreach (String key in obj.controlParams.Keys) {
						res.AddControlParam (key, obj.controlParams [key]);
					}
				}

				res.Tags = ToNSObjectArray (obj.tags);
				res.Feature = obj.feature != null ? obj.feature : "";
				res.Alias = obj.alias != null ? obj.alias : "";
				res.Channel = obj.channel != null ? obj.channel : "";
				res.Stage = obj.stage != null ? obj.stage : "";
				res.MatchDuration = (nuint)obj.matchDuration;
			}

			return res;
		}

        public static void SendEvent(BranchEvent e) {
            Dictionary<string, object> eventDict = e.ToDictionary();

            if (eventDict == null) {
                return;
            }

            IOSNativeBranch.BranchEvent branchEvent = null;
    
            if (eventDict.ContainsKey("event_name")) {
                branchEvent = new IOSNativeBranch.BranchEvent(eventDict["event_name"].ToString());
            }
            else {
                return;
            }
    
            if (eventDict.ContainsKey("transaction_id")) {
                branchEvent.TransactionID = eventDict ["transaction_id"].ToString();
            }
            if (eventDict.ContainsKey("customer_event_alias"))
            {
                branchEvent.Alias = eventDict["customer_event_alias"].ToString();
            }
            if (eventDict.ContainsKey("affiliation")) {
                branchEvent.Affiliation = eventDict ["affiliation"].ToString();
            }
            if (eventDict.ContainsKey("coupon")) {
                branchEvent.Coupon = eventDict ["coupon"].ToString();
            }
            if (eventDict.ContainsKey("currency")) {
                branchEvent.Currency = eventDict ["currency"].ToString();
            }
            if (eventDict.ContainsKey("tax")) {
                branchEvent.Tax = new NSDecimalNumber(eventDict["tax"].ToString());
            }
            if (eventDict.ContainsKey("revenue")) {
                branchEvent.Revenue = new NSDecimalNumber(eventDict["revenue"].ToString());
            }
            if (eventDict.ContainsKey("description")) {
                branchEvent.EventDescription = eventDict ["description"].ToString();
            }
            if (eventDict.ContainsKey("shipping")) {
                branchEvent.Shipping = new NSDecimalNumber(eventDict["shipping"].ToString());
            }
            if (eventDict.ContainsKey("search_query")) {
                branchEvent.SearchQuery = eventDict ["search_query"].ToString();
            }
            if (eventDict.ContainsKey("custom_data")) {
                Dictionary<string, string> dict = eventDict[@"custom_data"] as Dictionary<string, string>;
                NSMutableDictionary<NSString, NSString> tempDict = new NSMutableDictionary<NSString, NSString>();

                foreach(string key in dict.Keys) {
                    tempDict.Add(new NSString(key), new NSString(dict[key]));
                }

                branchEvent.CustomData = new NSDictionary<NSString, NSString>(tempDict.Keys, tempDict.Values);
            }

            if (eventDict.ContainsKey("content_items")) {
                List<string> array = eventDict["content_items"] as List<string>;
                NSMutableDictionary<NSObject, NSObject> tempDict = new NSMutableDictionary<NSObject, NSObject>();

                int i = 1;
                foreach (string buoJson in array) {
                    tempDict.Add(NSObject.FromObject(i), ToNativeUniversalObject(new BranchUniversalObject(buoJson)));
                    ++i;
                }

                branchEvent.ContentItems = NSArray.FromNSObjects<NSObject>(tempDict.Values);
            }
                
            branchEvent.LogEvent();
        }
	}
}
