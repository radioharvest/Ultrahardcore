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

	// Preferences accessor
	static Preferences m_Preferences;
	static bool m_PreferencesNotFound = false;
	public static Preferences PersistentPreferences
	{
		get
		{
			if (m_Preferences == null && !m_PreferencesNotFound)
			{
				m_Preferences = FindGlobalComponentByName<Preferences>("Preferences");
				m_PreferencesNotFound = (m_Preferences == null);
			}

			return m_Preferences;
		}
	}

	// Game data accessor
	static GameData m_GameData;
	static bool m_GameDataNotFound = false;
	public static GameData PersistentData
	{
		get
		{
			if (m_GameData == null && !m_GameDataNotFound)
			{
				m_GameData = FindGlobalComponentByName<GameData>("Data");
				m_GameDataNotFound = (m_GameData == null);
			}

			return m_GameData;
		}
	}

	static T FindGlobalComponentByName<T>(string name)
	{
		GameObject go = GameObject.Find(name);
		if (go != null)
		{
			return go.GetComponent<T>();
		}

		return default(T);
	}
}
