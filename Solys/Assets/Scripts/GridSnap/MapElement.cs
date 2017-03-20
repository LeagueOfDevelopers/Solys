using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapElement : MonoBehaviour {

	public enum ElementsType {Wheel, Target, Wall1, Corner1, AccelBlock, AccelerationBlock, AttractionBlock, GravityBlock, TPenter, TPexit};
	public ElementsType type;


	private Vector3 prevPosition;
	
	// Update is called once per frame
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	public string Serialize()
	{
		ElementSerializeObject element = new ElementSerializeObject();
		element.position = transform.position;
		element.type = type.ToString();
		return JsonUtility.ToJson(element);
	}

	public void Update()
         {
             if (transform.position != prevPosition)
             {
				 if(type == ElementsType.Wheel && Application.isPlaying) return;
                 Snap();
                 prevPosition = transform.position;
             }
         }
     
         private void Snap()
         {
                 Vector3 t = transform.position;
                 t.x = Round( t.x );
                 t.y = Round( t.y );
                 t.z = Round( t.z );
                 transform.position = t;
         }
     
         public float Round( float input )
         {
             return GeneralLogic.SnapValueForMapElements * Mathf.Round( ( input / GeneralLogic.SnapValueForMapElements ) );
         }
}
