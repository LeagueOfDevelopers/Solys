using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections.Generic;

public class GameDesignEditor : EditorWindow {

	Vector2 scrollPos = new Vector2();
	AnimBool OpenWheelDialog;
	AnimBool OpenLineDialog;
	AnimBool OpenWallDialog;
	AnimBool OpenGravityBlockDialog;
	AnimBool OpenAccelBlockDialog;
	GameObject WheelPrefab;
	GameObject LinePrefab;
	GameObject WallPrefab;
	GameObject GravityBlockPrefab;
	GameObject AccelBlockPrefab;

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
		OpenWallDialog = new AnimBool();
		OpenWallDialog.valueChanged.AddListener(Repaint);
		OpenGravityBlockDialog = new AnimBool();
		OpenGravityBlockDialog.valueChanged.AddListener(Repaint);
		OpenAccelBlockDialog = new AnimBool();
		OpenAccelBlockDialog.valueChanged.AddListener(Repaint);
		

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
				Wheel.GetComponent<Rigidbody2D>().mass =
						EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().mass,0,5);
				EditorGUILayout.PrefixLabel("GravityScale");
				Wheel.GetComponent<Rigidbody2D>().gravityScale = 
						EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().gravityScale,0,10);
				EditorGUILayout.PrefixLabel("Drag");
				Wheel.GetComponent<Rigidbody2D>().drag = 
						EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().drag,0,1);
				EditorGUILayout.PrefixLabel("AngularDrag");
				Wheel.GetComponent<Rigidbody2D>().angularDrag = 
						EditorGUILayout.Slider(Wheel.GetComponent<Rigidbody2D>().angularDrag,0,1);

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
			
				PhysicsMaterial2D edge = LinePrefab.GetComponent<EdgeCollider2D>().sharedMaterial;
				EditorGUILayout.PrefixLabel("Bounciness");
				edge.bounciness = EditorGUILayout.Slider(edge.bounciness, 0,1);
				EditorGUILayout.PrefixLabel("Friction");
				edge.friction = EditorGUILayout.Slider(edge.friction, 0,1);
				
			}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		//Стена-------------------------------------------------------------------------------------------------------------------------------------------
		OpenWallDialog.target = EditorGUILayout.Foldout(OpenWallDialog.target, "Wall");

		if(EditorGUILayout.BeginFadeGroup(OpenWallDialog.faded))
		{
		EditorGUI.indentLevel++;
		EditorGUILayout.PrefixLabel("Object");
		WallPrefab = (GameObject)EditorGUILayout.ObjectField(WallPrefab, typeof(GameObject));
		if(WallPrefab){
			
				PhysicsMaterial2D edge;
				if(WallPrefab.GetComponent<PolygonCollider2D>())
					edge = WallPrefab.GetComponent<PolygonCollider2D>().sharedMaterial;
				else edge = WallPrefab.GetComponent<BoxCollider2D>().sharedMaterial;
				EditorGUILayout.PrefixLabel("Bounciness");
				edge.bounciness = EditorGUILayout.Slider(edge.bounciness, 0,1);
				EditorGUILayout.PrefixLabel("Friction");
				edge.friction = EditorGUILayout.Slider(edge.friction, 0,1);
				
			}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		//Блок гравитации-------------------------------------------------------------------------------------------------------------------------------------------
		OpenGravityBlockDialog.target = EditorGUILayout.Foldout(OpenGravityBlockDialog.target, "Gravity Block");

		if(EditorGUILayout.BeginFadeGroup(OpenGravityBlockDialog.faded))
		{
		EditorGUI.indentLevel++;
		EditorGUILayout.PrefixLabel("Object");
		GravityBlockPrefab = (GameObject)EditorGUILayout.ObjectField(GravityBlockPrefab, typeof(GameObject));
		if(GravityBlockPrefab)
		{
			GameObject[] BlocksList = GameObject.FindGameObjectsWithTag("GravityBlock");
			if(BlocksList.Length == 0)
				BlocksList = new GameObject[]{GravityBlockPrefab};
			
			
			
			EditorGUILayout.PrefixLabel("Force");
			GravityBlockPrefab.GetComponent<GravityBlock>().Force = 
					EditorGUILayout.Slider(GravityBlockPrefab.GetComponent<GravityBlock>().Force,0,20);	

			foreach(GameObject block in BlocksList)
				block.GetComponent<GravityBlock>().Force = GravityBlockPrefab.GetComponent<GravityBlock>().Force;
				
		}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		//Блок Ускорения-------------------------------------------------------------------------------------------------------------------------------------------
		OpenAccelBlockDialog.target = EditorGUILayout.Foldout(OpenAccelBlockDialog.target, "Accel Block");

		if(EditorGUILayout.BeginFadeGroup(OpenAccelBlockDialog.faded))
		{
		EditorGUI.indentLevel++;
		EditorGUILayout.PrefixLabel("Object");
		AccelBlockPrefab = (GameObject)EditorGUILayout.ObjectField(AccelBlockPrefab, typeof(GameObject));
		if(AccelBlockPrefab)
		{
			GameObject[] BlocksList = GameObject.FindGameObjectsWithTag("AccelBlock");
			if(BlocksList.Length == 0)
				BlocksList = new GameObject[]{AccelBlockPrefab};
			
			
			
			EditorGUILayout.PrefixLabel("Force");
			AccelBlockPrefab.GetComponent<AccelBlock>().AccelForce = 
					EditorGUILayout.Slider(AccelBlockPrefab.GetComponent<AccelBlock>().AccelForce,0,10000);	

			foreach(GameObject block in BlocksList)
				block.GetComponent<AccelBlock>().AccelForce = AccelBlockPrefab.GetComponent<AccelBlock>().AccelForce;
				
		}
		EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}


}
