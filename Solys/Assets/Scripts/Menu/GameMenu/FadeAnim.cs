using UnityEngine;

public class FadeAnim : MonoBehaviour {

	public GameObject ui;
	public void ExitAnimEnded()
	{
		ui.GetComponent<UIHandlerScript>().SwitchScene();
	}
}
