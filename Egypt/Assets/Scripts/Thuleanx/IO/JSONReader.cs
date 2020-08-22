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
			string jsonAsText = File.ReadAllText(Application.dataPath + filenpath);
			return JsonConvert.DeserializeObject<Dictionary<Key, Value>>(jsonAsText);
		}

		public static Value Parse<Value>(string filenpath)
		{
			string jsonAsText = File.ReadAllText(Application.dataPath + filenpath);
			return JsonConvert.DeserializeObject<Value>(jsonAsText);
		}
	}
}