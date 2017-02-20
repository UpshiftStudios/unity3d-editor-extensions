# unity3d-editor-extensions

It contains following UI elements-

**String Popup:**

Creates a popup field in EditorWindow or Inspector for a SerializedProperty.

**Description:**

Call the extension method StringPopup(...) on your required property.

`StringPopup(this SerializedProperty arraySerializedProperty, string relativeName, string currentSelected, string label)`

Parameters-

`arraySerializedProperty:` is the serialized property which is to be used to create the popup (null not allowed).
`relativeName:` the relative name of the element of type string from the arraySerializedProperty (null or empty string allowed).
`currentSelected:` the currently selected string (null or empty strings are allowed).
`label:` label for the field (null or empty string allowed).

Returns-

`string:` selected string.

**Usage:**

Case 1:

Say you have a serialized class MyStrings:

`[System.Serializable]
public class MyStrings 
{
  public string[] strings;
}`

On the SerializedProperty of the strings variable (let's say named as say stringsProperty) call the method as:

`selectedString = stringsProperty.StringPopup ("", selectedString, "My String:");`

Case 2:

Say you have a serialized class NestedStrings:
`[System.Serializable]
public class NestedStrings 
{
  public MyString[] strings;
}

[System.Serializable]
public class MyString 
{
  public string str;
}`

On the SerializedProperty of the strings variable (let's say named as say stringsProperty) call the method as:

`selectedString = stringsProperty.StringPopup ("str", selectedString, "My String:");`

**NOTE:**

Internally it uses the generic popup method: `EditorGUILayout.Popup(...)`.
