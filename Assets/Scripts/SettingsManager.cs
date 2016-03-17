using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
	//TODO: update to follow c# conventions (capitals, etc)
    public GameObject[] toDisable;
    public GameObject returnDestination;
    public bool rightHandMode;
    public Text modeText;
    public Text modeButtonText;

	// Use this for initialization
	void Start ()
	{
        foreach (GameObject o in toDisable)
            o.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    public void ToggleControlMode()
    {
        if(rightHandMode)
        {
            rightHandMode = false;
            modeText.text = "Current mode: Left Handed";
            modeButtonText.text = "Switch to Right Handed";
        }
        else
        {
            rightHandMode = true;
            modeText.text = "Current mode: Right Handed";
            modeButtonText.text = "Switch to Left Handed";
        }
    }
}
