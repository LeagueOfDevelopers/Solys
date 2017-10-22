using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LastLevelRating : MonoBehaviour {

    public Sprite FullStar;
    public Sprite NoStar;
    public float delay = 5;

    private void Start()
    {
        
        if(SceneDataTransfer.Instance.NeedToUpdateStarsForLevel != 0)
        {
            int LastLevelStars = SceneDataTransfer.Instance.LastLevelRating;
            SceneDataTransfer.Instance.NeedToUpdateStarsForLevel = 0;
            SetStars(LastLevelStars);
            StartAnim();
            StartCoroutine(WaitToExit());

        }
    }

    private void SetStars(int stars)
    {
        Debug.Log(stars);
        Image[] elements = transform.GetComponentsInChildren<Image>();
        foreach(Image elem in elements)
        {
            if (stars > 0)
            {
                elem.sprite = FullStar;
                stars--;
            }
            else
                elem.sprite = NoStar;
        }
           
    }

    private void StartAnim()
    {
        GetComponent<Animator>().SetTrigger("Enter");

    }

    IEnumerator WaitToExit()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().SetTrigger("Exit");
    }
}
