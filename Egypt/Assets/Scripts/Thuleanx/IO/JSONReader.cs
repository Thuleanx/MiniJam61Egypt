using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Thuleanx.IO {
	public class JSONReader
	{
		public static Dictionary<Key, Value> Parse<Key, Value>(string filenpath)
		{
			string jsonAsText = (Resources.Load(filenpath) as TextAsset).text;
			return JsonConvert.DeserializeObject<Dictionary<Key, Value>>(jsonAsText);
		}

		public static Value Parse<Value>(string filenpath)
		{
			TextAsset asset = Resources.Load<TextAsset>(filenpath);
			Debug.Log(filenpath + " " + asset);
			string jsonAsText = (asset).text;
			return JsonConvert.DeserializeObject<Value>(jsonAsText);
		}
	}
}