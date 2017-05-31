using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class MapElementDrag : MonoBehaviour {



	private LineWriter writerComponent;
	private MapElement lastDragedElement;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
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
				var element = hit.transform.gameObject.GetComponent<MapElement>();
				
				if(element != null)
					if(element.isDragable)
					{
						lastDragedElement = element;
						element.isNeedToSnap = false;
						element.Drag(new Vector3(origin.x,origin.y,0));
					}
			}
		}

		if(lastDragedElement != null)
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
			lastDragedElement.Drag(new Vector3(origin.x,origin.y,0));
		}
		
	}
	void ReleaseDragedObject(LeanFinger finger)
	{
		if(lastDragedElement!=null)
		{
			lastDragedElement.isNeedToSnap = true;
			lastDragedElement = null;
		}
	}

}
