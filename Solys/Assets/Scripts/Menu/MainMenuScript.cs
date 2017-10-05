using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject Logo;
    public GameObject ButtonText;
    public GameObject Ball;

    private Coroutine Wait;

    public void StartLogoAnimEnded()
    {
         Wait = StartCoroutine(WaitToContinueAnimation());
    }

    public void OpenSelectPackMenu()
    {
        SceneManager.LoadScene("SelectPackMenu");

    }

    private void Start()
    {
        ButtonText.transform.parent.gameObject.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if(Wait!=null && (Input.touchCount>0 || Input.GetMouseButtonDown(0)))
        {
            StopCoroutine(Wait);
            ContinueAnimation();
        }
    }
    
    private IEnumerator WaitToContinueAnimation()
    {
        yield return new WaitForSeconds(5);
        ContinueAnimation();

    }

    private void ContinueAnimation()
    {
        GetComponent<Animator>().SetBool("EndLogo", true);
        Logo.GetComponent<Animator>().SetBool("EndLogo", true);
        ButtonText.GetComponent<Animator>().SetBool("EndLogo", true);
        Ball.GetComponent<Animator>().SetBool("EndLogo", true);
        ButtonText.transform.parent.gameObject.GetComponent<Button>().interactable = true;
    }
}
