using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour {

	public int id;
	public void OnClick()
	{
		if(SceneDataTransfer.Instance.LoadLevelData(id))
			SceneManager.LoadScene(2);
	}
}
