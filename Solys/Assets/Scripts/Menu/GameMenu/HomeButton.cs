
using UnityEngine;

public class HomeButton : MonoBehaviour {

	public void OnClick()
    {
        GameObject.Find("UI").GetComponent<UIHandlerScript>().TargetReached();
    }
}
