using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectPackButton : MonoBehaviour {

    public GameObject Title;
    public int FirstScene;
    public int LastScene;
    public GameObject StarsLabel;
    public GameObject ScrollView;
    public GameObject Lock;
    public int PackGroup = 0;

    public bool locked = true;

    public void onClick()
    {
        if(locked)
        {
            Unlock();
        }
        else
            ScrollView.GetComponent<SelectPackMenuPackSelected>().PackSelect(Title.GetComponent<Text>().text,FirstScene, LastScene);
    }

    public void Start()
    {
        SetStarsLabel();
        SetUnlocked();
    }

    private void SetStarsLabel()
    {
        int currentAmmount = PrefsDriver.GetSumOfStarsForLevelRange(FirstScene, LastScene);
        int maxAmmount = (LastScene - FirstScene + 1)*3;
        StarsLabel.GetComponent<Text>().text = currentAmmount.ToString() + '/' + maxAmmount.ToString();
    }

    private void SetUnlocked()
    {
        if (PrefsDriver.IsPackBought(FirstScene) || !locked)
        {
            locked = false;
            Lock.SetActive(false);
        }
    }

    public void Unlock()
    {
        locked = false;
        PrefsDriver.BuyPack(FirstScene);
        Lock.GetComponent<Animator>().SetTrigger("Unlock");
    }
}
