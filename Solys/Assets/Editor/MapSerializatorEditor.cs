﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(MapSerializator))]
[CanEditMultipleObjects]
public class MapSerializatorEditor : Editor {

	string deserializeText = "ДАННЫЕ ДЛЯ ДЕСЕРИЛИЗАЦИИ";
	SerializedProperty dict;
	void OnEnable()
	{
		dict = serializedObject.FindProperty("types");
		
	}


	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		for(int i = 0; i<dict.arraySize; i++)
		{
			EditorGUILayout.PrefixLabel(((MapElement.ElementsType)i).ToString());
			EditorGUILayout.ObjectField(dict.GetArrayElementAtIndex(i));
		}
		serializedObject.ApplyModifiedProperties();

		deserializeText = EditorGUILayout.TextArea(deserializeText);
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
