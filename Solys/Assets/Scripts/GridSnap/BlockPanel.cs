using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class BlockPanel : MonoBehaviour {
	private LineWriter writerComponent;
	private BlocksPanelElement lastDragedElement;

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
				var element = hit.transform.gameObject.GetComponent<BlocksPanelElement>();
				
				if(element != null)		
						lastDragedElement = element;
						element.Drag(new Vector3(origin.x,origin.y,0));
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
			lastDragedElement.Release();
			lastDragedElement = null;
		}
	}

	void CheckTouchPosition()
	{

	}

}

