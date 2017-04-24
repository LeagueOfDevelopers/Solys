using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(MapSerializator))]
[CanEditMultipleObjects]
public class MapSerializatorEditor : Editor {
    
	string deserializeText = "ДАННЫЕ ДЛЯ ДЕСЕРИЛИЗАЦИИ";
	SerializedProperty dict;
	GUIStyle style;


	public override void OnInspectorGUI()
	{
		dict = serializedObject.FindProperty("types");
		EditorStyles.textArea.wordWrap = true;
		EditorGUILayout.PrefixLabel("Deserialize on scene load?");
		((MapSerializator)target).DeserializeOnLoad = EditorGUILayout.Toggle(((MapSerializator)target).DeserializeOnLoad);
		serializedObject.Update();
		for(int i = 0; i<dict.arraySize; i++)
		{
			EditorGUILayout.PrefixLabel(((MapElement.ElementsType)i).ToString());
			EditorGUILayout.ObjectField(dict.GetArrayElementAtIndex(i));
		}
		serializedObject.ApplyModifiedProperties();
		deserializeText = EditorGUILayout.TextArea(deserializeText, GUILayout.Height(300));
		if(GUILayout.Button("Serialize"))
			{
				MapSerializator obj = (MapSerializator)target;
				Debug.Log(obj.Serialize());
			}
		if(GUILayout.Button("Deserialize"))
			{
				Debug.Log(deserializeText);
				MapSerializator obj = (MapSerializator)target;
				obj.Deserialize(deserializeText);
			}
			
		
	}
}
