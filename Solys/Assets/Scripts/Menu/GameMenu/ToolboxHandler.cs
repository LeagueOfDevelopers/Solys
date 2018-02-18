using UnityEngine;

public class ToolboxHandler : MonoBehaviour {

    private GameObject writer;
    private int fullClearCounter = 0;
	// Use this for initialization
	void Start () {
        writer = GameObject.Find("LineWriter");
        Debug.Log(writer.name);
	}
	
	public void valueChange(int tool)
    {
        if (tool == 1)
        {
            fullClearCounter++;
            if (fullClearCounter > 2)
            {
                GeneralLogic.ResetSimulationEvent();
                fullClearCounter = 0;
            }
        }
        else
            fullClearCounter = 0;

        writer.GetComponent<LineWriter>().ChangeTool(tool);
    }
}
