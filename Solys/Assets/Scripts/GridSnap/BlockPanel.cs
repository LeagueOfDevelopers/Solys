using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class BlockPanel : MonoBehaviour {
	private LineWriter writerComponent;
	private BlockPanelElement lastDragedElement;

	private GameObject map;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		map = GameObject.Find("Map");
		writerComponent = GameObject.Find("LineWriter").GetComponent<LineWriter>();
		LeanTouch.OnFingerSet += DragElementIfTouch;
		LeanTouch.OnFingerUp += ReleaseDragedObject;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		LeanTouch.OnFingerSet-= DragElementIfTouch;
		LeanTouch.OnFingerUp -=ReleaseDragedObject;
	}
	
	void DragElementIfTouch(LeanFinger finger)
	{
		if(writerComponent.PositionsAmount == 0 && lastDragedElement == null)
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);

			RaycastHit2D hit = Physics2D.Raycast(origin,Vector2.zero);

			if(hit)
			{
				var element = hit.transform.gameObject.GetComponent<BlockPanelElement>();
				Debug.Log(hit.collider.name);
				if(element != null)		
						{
							lastDragedElement = element;
							element.Drag(new Vector3(origin.x,origin.y,0));
						}
			}
		}

		if(lastDragedElement != null)
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(origin,Vector2.zero);
			bool isElementIn = false;
			foreach(RaycastHit2D hit in hits)
				if(hit.collider.gameObject.name == gameObject.name) isElementIn = true;
				
			if(isElementIn)
				lastDragedElement.Drag(new Vector3(origin.x,origin.y,0));
			else
			{
				MapSerializator.InstantiateMapObject(lastDragedElement.type,origin,Quaternion.Euler(0,0,0),map.transform,true);
				ReleaseDragedObject(finger);
				
			}
		}
		
	}
	void ReleaseDragedObject(LeanFinger finger)
	{
		if(lastDragedElement!=null)
		{
			lastDragedElement.Release();
			lastDragedElement = null;
		}
	}

}

