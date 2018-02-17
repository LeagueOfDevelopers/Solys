using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMoveHandler : MonoBehaviour {

    public bool isPackMenu;
    

	public void ScrollToLastPoint()
    {
        if (isPackMenu)
            FindContentAndScroll(PrefsDriver.GetScrollPosForPackSelect());
        else
            FindContentAndScroll(PrefsDriver.GetScrollPosForLevelSelect(SceneDataTransfer.Instance.FirstLevelInPack.ToString()));
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
            StopAllCoroutines();
    }

    /// <summary>
    /// </summary>
    /// <param name="pos"></param>
    private void FindContentAndScroll(float pos)
    {
        GameObject content = transform.FindChild("Viewport").FindChild("Content").gameObject;
        //Debug.Log(content);
        StartCoroutine(SmoothMoving(content,pos));
    }

    
    IEnumerator SmoothMoving(GameObject content, float pos)
    {
        Debug.Log(content.GetComponent<RectTransform>().localPosition.x);
        yield return new WaitForEndOfFrame();
        Vector3 current = content.GetComponent<RectTransform>().localPosition;
        current.x = Mathf.Lerp(current.x, pos, Time.deltaTime);
        content.GetComponent<RectTransform>().localPosition = current;
        if (Mathf.Abs(current.x - pos) > 100)
            StartCoroutine(SmoothMoving(content, pos));
            
    }

}
