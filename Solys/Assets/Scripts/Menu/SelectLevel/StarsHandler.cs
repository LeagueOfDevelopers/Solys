using UnityEngine.UI;
using UnityEngine;

public class StarsHandler : MonoBehaviour {

	public Sprite star;
	public Sprite noStar;

	public void SetStars(int stars)
	{
		for (int i = 0; i < stars; i++)
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = star;

		for (int i = stars; i<3; i++)
			transform.GetChild(i).gameObject.GetComponent<Image>().sprite = noStar;
	}

	public void SetStarsAnim()
	{

	}
}
