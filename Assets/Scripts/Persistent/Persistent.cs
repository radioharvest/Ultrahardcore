using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour
{
	public string AutoLoadLevel;

	void Start()
	{
		DontDestroyOnLoad(this);

		// Make children persistent
		int numChildren = transform.childCount;
		for (int i = 0; i < numChildren; i++)
		{
			Transform childTransform = transform.GetChild(i);
			GameObject childGameObject = childTransform.gameObject;
			if (childGameObject != null)
			{
				DontDestroyOnLoad(childGameObject);
			}
		}

		// Load next level
		if (AutoLoadLevel.Length > 0)
		{
			Application.LoadLevel(AutoLoadLevel);
		}
	}
}
