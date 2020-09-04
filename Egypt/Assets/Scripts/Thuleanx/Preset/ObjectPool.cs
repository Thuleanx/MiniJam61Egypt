using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Thuleanx.Preset {
	public class ObjectPool : MonoBehaviour
	{
		[System.Serializable]
		public class Pool {
			public string tag, prefabLocation;
			public GameObject prefab;
			public int defaultCount;
		}

		public static ObjectPool Instance;

		[SerializeField] int expansionConstant;
		[SerializeField] Pool[] pools;

		Dictionary<string, Queue<GameObject>> objectQueues = new Dictionary<string, Queue<GameObject>>();
		Dictionary<string, int> tagToPoolIndex = new Dictionary<string, int>();
		
		void Awake() {
			Instance = this;
			pools = Thuleanx.IO.JSONReader.Parse<Pool[]>("Data/ObjectPool");

			for (int i = 0; i < pools.Length; i++) {
				if (pools[i].prefabLocation.StartsWith("/"))
					pools[i].prefab = Resources.Load(pools[i].prefabLocation.Substring(1)) as GameObject; 
				else
					pools[i].prefab = Resources.Load(pools[i].prefabLocation) as GameObject; 

				tagToPoolIndex[pools[i].tag] = i;
				objectQueues[pools[i].tag] = new Queue<GameObject>();
				Expand(pools[i], pools[i].defaultCount);
			}
		}

		void Expand(Pool pool, int count) {
			while (count --> 0) {
				GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
				obj.SetActive(false);
				obj.AddComponent(typeof(ObjectReturnToPool));

				obj.transform.parent = transform;

				ObjectReturnToPool returnScript = obj.GetComponent<ObjectReturnToPool>();
				returnScript.sourceTag = pool.tag;

				objectQueues[pool.tag].Enqueue(obj);

			}
		}

		public GameObject Instantiate(string tag) {
			return Instantiate(tag, Vector3.zero, Quaternion.identity);
		}

		public GameObject Instantiate(string tag, Vector3 position, Quaternion rotation) {
			Assert.IsTrue(objectQueues.ContainsKey(tag));

			if (objectQueues[tag].Count == 0)
				Expand(pools[tagToPoolIndex[tag]], expansionConstant);

			GameObject obj = objectQueues[tag].Dequeue();
			obj.transform.position = position;
			obj.transform.rotation = rotation;

			obj.SetActive(true);

			return obj;
		}

		public void Return(string tag, GameObject obj) {
			Assert.IsTrue(objectQueues.ContainsKey(tag));

			objectQueues[tag].Enqueue(obj);
		}
	}
}