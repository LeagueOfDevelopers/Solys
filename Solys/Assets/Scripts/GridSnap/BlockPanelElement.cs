using UnityEngine;
using System.Collections;

public class BlocksPanelElement : MonoBehaviour {

	Vector3 startPos;
	void Start()
	{
		startPos = transform.position;
	}
	public void Drag(Vector3 newPos)
	{
		transform.position = newPos;
	}
	public void Release()
	{
		StartCoroutine(ReleaseMoveCoroutine());
	}

	IEnumerator ReleaseMoveCoroutine()
	{
		Vector3 path = startPos - transform.position;
		transform.position += path*0.05f;
		yield return new WaitForEndOfFrame();
		if(path.magnitude >0.1)
			StartCoroutine(ReleaseMoveCoroutine());
	}
}
