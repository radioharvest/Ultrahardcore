using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour
{
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
	}
}
