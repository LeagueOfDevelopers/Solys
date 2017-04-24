using UnityEngine;
using System.Collections;

public class BlockPanelElement : MonoBehaviour {
	public MapElement.ElementsType type;
	Vector3 startPos;
	void Start()
	{
		startPos = transform.position;
		Debug.Log(transform.position);
	}
	public void Drag(Vector3 newPos)
	{
		transform.position = newPos;
	}
	public void Release()
	{
		transform.position = new Vector3(transform.position.x,transform.position.y,startPos.z);
		StartCoroutine(ReleaseMoveCoroutine());
	}

	IEnumerator ReleaseMoveCoroutine()
	{
		Vector3 path = startPos - transform.position;
		transform.position += path*0.05f;
		yield return new WaitForEndOfFrame();
		if(path.magnitude >0.05)
			StartCoroutine(ReleaseMoveCoroutine());
	}
}
