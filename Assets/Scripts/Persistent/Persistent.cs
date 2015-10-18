using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour
{
	public string AutoLoadLevel;

	void Start()
	{
		RecursivelyMakePersistent(transform);

		// Load next level
		if (AutoLoadLevel.Length > 0)
		{
			Application.LoadLevel(AutoLoadLevel);
		}
	}

	void RecursivelyMakePersistent(Transform tr)
	{
		if (tr.gameObject != null)
		{
			DontDestroyOnLoad(tr.gameObject);
		}

		// Make children persistent
		int numChildren = tr.childCount;
		for (int i = 0; i < numChildren; i++)
		{
			RecursivelyMakePersistent(tr.GetChild(i));
		}
	}
}
