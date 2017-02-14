using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class EditorExtension
{
	private static readonly int ERROR_INDEX = -1;

	public static string StringPopup(this SerializedProperty arraySerializedProperty, string relativeName, string currentSelected, string label)
	{
		if (arraySerializedProperty == null || string.IsNullOrEmpty (relativeName)) { // || string.IsNullOrEmpty(currentSelected)) {
			return null;
		}

		if (!arraySerializedProperty.isArray) {
			return null;
		}

		relativeName = relativeName.Trim ();
		if (!string.IsNullOrEmpty (currentSelected)) {
			currentSelected = currentSelected.Trim ();
		}

		string[] items = GetStringArray (arraySerializedProperty, relativeName);

		return DoStringsPopup (label, items, currentSelected);
	}

	private static string[] GetStringArray (SerializedProperty serializedProperty, string relativeName)
	{
		if (serializedProperty == null) {
			return null;
		}

		List<string> stringsList = new List<string> ();

		// If relative name is specified means we need to dig deeper in property to create array.
		if (!string.IsNullOrEmpty (relativeName)) {
			relativeName = relativeName.Trim ();
			for (int i = 0; i < serializedProperty.arraySize; i++) {
				var element = serializedProperty.GetArrayElementAtIndex (i);
				if (element != null) {
					stringsList.Add (element.FindPropertyRelative (relativeName).stringValue);
				}
			}
		}
		// Just generate array from the string elements from property directly.
		else {
			for (int i = 0; i < serializedProperty.arraySize; i++) {
				stringsList.Add (serializedProperty.GetArrayElementAtIndex(i).stringValue);
			}
		}

		return stringsList.ToArray ();
	}

	private static string DoStringsPopup(string label, string[] array, string currentSelected)
	{
		if (array == null) {
			return null;
		}

		int selectedIndex = EditorGUILayout.Popup (label, GetIndexOfString (array, currentSelected), array, GUILayout.ExpandWidth (true));

		return GetStringAtIndex (array, selectedIndex);
	}

	private static string GetStringAtIndex(string[] array, int index)
	{
		if (array == null || index < 0) {
			return null;
		}
		if (index >= array.Length) {
			return null;
		}

		return array[index];
	}

	private static int GetIndexOfString(string[] array, string str)
	{
		if (array == null || string.IsNullOrEmpty (str)) {
			return ERROR_INDEX;
		}
		int index = ERROR_INDEX;

		for (int i = 0; i < array.Length; i++) {
			if (array [i] == str) {
				index = i;
				break;
			}
		}

		return index;
	}
}
