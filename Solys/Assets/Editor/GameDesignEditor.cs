﻿using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class GameDesignEditor : EditorWindow {

	Vector2 scrollPos = new Vector2();
	AnimBool OpenWheelDialog;
	AnimBool OpenLineDialog;
	GameObject WheelPrefab;
	GameObject LinePrefab;
	[MenuItem("Edit/GameDesign")]

	static void Init()
	{
		var window = (GameDesignEditor)EditorWindow.GetWindow( typeof( GameDesignEditor ) );
             window.maxSize = new Vector2( 200, 100 );
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	public void OnEnable()
	{
		OpenWheelDialog = new AnimBool();
		OpenWheelDialog.valueChanged.AddListener(Repaint);
		OpenLineDialog = new AnimBool();
		OpenLineDialog.valueChanged.AddListener(Repaint);
		

	}
	public void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos,false,true);
		// КОЛЕСО-------------------------------------------------------------------------------------------------------------------------------------------
		OpenWheelDialog.target = EditorGUILayout.Foldout(OpenWheelDialog.target, "Wheel");
		if(EditorGUILayout.BeginFadeGroup(OpenWheelDialog.faded))
		{
		EditorGUI.indentLevel++;
		EditorGUILayout.PrefixLabel("Object");
		WheelPrefab = (GameObject)EditorGUILayout.ObjectField(WheelPrefab, typeof(GameObject));
		if(WheelPrefab){
			GameObject Wheel = GameObject.Find(WheelPrefab.name);
			if(Wheel)
				{
				EditorGUILayout.PrefixLabel("Mass");
				Wheel.GetComponent<Rigidbody2D>().mass = EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().mass,0,5);
				EditorGUILayout.PrefixLabel("GravityScale");
				Wheel.GetComponent<Rigidbody2D>().gravityScale = EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().gravityScale,0,10);
				EditorGUILayout.PrefixLabel("Drag");
				Wheel.GetComponent<Rigidbody2D>().drag = EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().drag,0,1);
				EditorGUILayout.PrefixLabel("AngularDrag");
				Wheel.GetComponent<Rigidbody2D>().angularDrag = EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().angularDrag,0,1);

				EditorGUILayout.Space();

				PhysicsMaterial2D material = Wheel.GetComponent<CircleCollider2D>().sharedMaterial;
				EditorGUILayout.PrefixLabel("Bounciness");
				material.bounciness = EditorGUILayout.Slider(material.bounciness, 0,1);
				EditorGUILayout.PrefixLabel("Friction");
				material.friction = EditorGUILayout.Slider(material.friction, 0,1);
				if(GUILayout.Button("Save"))
					PrefabUtility.ReplacePrefab(Wheel,WheelPrefab);
			}}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		//ЛИНИЯ-------------------------------------------------------------------------------------------------------------------------------------------
		OpenLineDialog.target = EditorGUILayout.Foldout(OpenLineDialog.target, "Line");

		if(EditorGUILayout.BeginFadeGroup(OpenLineDialog.faded))
		{
		EditorGUI.indentLevel++;
		EditorGUILayout.PrefixLabel("Object");
		LinePrefab = (GameObject)EditorGUILayout.ObjectField(LinePrefab, typeof(GameObject));
		if(LinePrefab){
			GameObject Line = GameObject.Find(LinePrefab.name)? GameObject.Find(LinePrefab.name) :LinePrefab;
			if(Line)
				{
				PhysicsMaterial2D edge = Line.GetComponent<EdgeCollider2D>().sharedMaterial;
				EditorGUILayout.PrefixLabel("Bounciness");
				edge.bounciness = EditorGUILayout.Slider(edge.bounciness, 0,1);
				EditorGUILayout.PrefixLabel("Friction");
				edge.friction = EditorGUILayout.Slider(edge.friction, 0,1);
				if(GUILayout.Button("Save"))
					PrefabUtility.ReplacePrefab(Line,LinePrefab);
			}}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}


}