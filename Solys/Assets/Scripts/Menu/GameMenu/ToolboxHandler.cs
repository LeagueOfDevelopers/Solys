using UnityEngine;
using UnityEngine.UI;

public class ToolboxHandler : MonoBehaviour {

    private GameObject writer;
    private int fullClearCounter = 0;

	// Use this for initialization
	void Start () {
        writer = GameObject.Find("LineWriter");
        Debug.Log(writer.name);
	}
	
	

    public void valueChange(bool isWriter)
    {
        Debug.Log(isWriter);
        if (isWriter)
        {
            fullClearCounter = 0;
            writer.GetComponent<LineWriter>().ChangeTool(0);
        }
        else
            writer.GetComponent<LineWriter>().ChangeTool(1);           

    }

    public void FullResetClickCounter()
    {
        fullClearCounter++;
        if (fullClearCounter > 2)
        {
            ClearAll();
        }
    }

    private void ClearAll()
    {
        transform.Find("Writer").GetComponent<Toggle>().isOn = true;
        fullClearCounter = 0;
        GeneralLogic.ResetSimulationEvent();
    }
}
