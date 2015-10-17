using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class Preferences : MonoBehaviour
{
	// Increasing this number will erase all existing preference keys.
	// Use this when making incompatible changes or deleting any existing properties.
	const int PREFERENCES_VERSION = 1;
	const string PREFERENCES_VERSION_KEY_NAME = "PrefVersion";

	// This is test value, delete it after adding any real preferences.
	public int TestValue;

	void Start()
	{
		int version = -1;

		if (PlayerPrefs.HasKey(PREFERENCES_VERSION_KEY_NAME))
		{
			version = PlayerPrefs.GetInt(PREFERENCES_VERSION_KEY_NAME);
		}

		// Check version compatibility
		if (version != PREFERENCES_VERSION)
		{
			PlayerPrefs.DeleteAll();
		}

		// Make sure correct version is saved
		PlayerPrefs.SetInt(PREFERENCES_VERSION_KEY_NAME, PREFERENCES_VERSION);
		PlayerPrefs.Save();

		Type myType = GetType();

		// Read preferences from registry
		foreach (FieldInfo fieldInfo in myType.GetFields())
		{
			// Make sure that preference key exists
			if (PlayerPrefs.HasKey(fieldInfo.Name))
			{
				if (fieldInfo.FieldType == typeof(int))
				{
					fieldInfo.SetValue(this, PlayerPrefs.GetInt(fieldInfo.Name));
				}
				else if (fieldInfo.FieldType == typeof(float))
				{
					fieldInfo.SetValue(this, PlayerPrefs.GetFloat(fieldInfo.Name));
				}
				else if (fieldInfo.FieldType == typeof(string))
				{
					fieldInfo.SetValue(this, PlayerPrefs.GetString(fieldInfo.Name));
				}
				else
				{
					// Tell them which types are supported for preferences
					Debug.LogWarning("Public field '" + fieldInfo.Name + "' has unsupported type: " + fieldInfo.GetType().ToString()
						+ ". Supported preference types are: int, float, string. Please fix your code, Mr. Babushkin.");
				}
			}
		}
	}

	void OnDestroy()
	{
		Type myType = GetType();

		// Save preferences to registry
		foreach (FieldInfo fieldInfo in myType.GetFields())
		{
			if (fieldInfo.FieldType == typeof(int))
			{
				PlayerPrefs.SetInt(fieldInfo.Name, (int)fieldInfo.GetValue(this));
			}
			else if (fieldInfo.FieldType == typeof(float))
			{
				PlayerPrefs.SetFloat(fieldInfo.Name, (float)fieldInfo.GetValue(this));
			}
			else if (fieldInfo.FieldType == typeof(string))
			{
				PlayerPrefs.SetString(fieldInfo.Name, (string)fieldInfo.GetValue(this));
			}
		}

		PlayerPrefs.Save();
	}
}
