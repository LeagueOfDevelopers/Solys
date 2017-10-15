using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectPackButton : MonoBehaviour {

    public GameObject Title;
    public int FirstScene;
    public int LastScene;
    public GameObject StarsLabel;
    public GameObject ScrollView;

    public void onClick()
    {
        ScrollView.GetComponent<SelectPackMenuPackSelected>().PackSelect(Title.GetComponent<Text>().text,FirstScene, LastScene);
    }

    public void Start()
    {
        SetStarsLabel();
    }

    private void SetStarsLabel()
    {
        int currentAmmount = PrefsDriver.GetSumOfStarsForLevelRange(FirstScene, LastScene);
        int maxAmmount = (LastScene - FirstScene + 1)*3;
        StarsLabel.GetComponent<Text>().text = currentAmmount.ToString() + '/' + maxAmmount.ToString();
    }
}
