using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandlerScript : MonoBehaviour {


	public GameObject fade;
	public GameObject slider;
	private enum direction {Menu, SelectLevel}
	private direction destination;


	public void SwitchScene()		//Called by slider
	{
		switch (destination)
		{
			case direction.Menu:
				SceneManager.LoadScene("Menu");
			break;
			case direction.SelectLevel:
				SceneManager.LoadScene("SelectLevelMenu");
			break;
		}
	}

	public void MenuPressed()
	{
		GeneralLogic.StopSimulationEvent();
		StartExitAnim();
		destination = direction.Menu;
	}

	public void TargetReached()
	{
		GeneralLogic.StopSimulationEvent();
		StartExitAnim();
		destination = direction.SelectLevel;
	}

	private void StartExitAnim()
	{
		fade.GetComponent<Animator>().SetBool("Exit", true);
        slider.GetComponent<Animator>().SetTrigger("Exit");
	}
}
