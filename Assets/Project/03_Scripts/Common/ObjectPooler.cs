using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[SerializeField] private GameObject objectPrefab = null;
	[SerializeField] private int poolSize = 10;
	[SerializeField] private bool canExpand = true;

	private List<GameObject> objectList;
	private GameObject parentObject;

	// Start is called before the first frame update
	void Start()
	{
		parentObject = new GameObject("Parent");
		Refill();
	}

	private void Refill()
	{
		objectList = new List<GameObject>();
		for (int i = 0; i < poolSize; i++)
		{
			AddGameObject();
		}
	}

	public GameObject GetObjectFromPool()
	{
		for (int i = 0; i < objectList.Count; i++)
		{
			if (!objectList[i].activeInHierarchy)
			{
				return objectList[i];
			}
		}
		if (canExpand)
		{
			AddGameObject();
			return objectList[objectList.Count - 1];
		}
		return null;
	}

	private void AddGameObject()
	{
		GameObject obj = Instantiate(objectPrefab, parentObject.transform);
		obj.SetActive(false);
		objectList.Add(obj);
	}
}
