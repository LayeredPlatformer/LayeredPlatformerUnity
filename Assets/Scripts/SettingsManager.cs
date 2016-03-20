using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
    public GameObject[] ToDisable;
    public static string ReturnDestination;
    public bool RightHandMode;
    public Text ModeText;
    public Text ModeButtonText;

	// Use this for initialization
	void Start ()
	{
        foreach (GameObject o in ToDisable)
            o.SetActive(false);
	}

    public void ToggleControlMode()
    {
        if(RightHandMode)
        {
            RightHandMode = false;
            ModeText.text = "Current mode: Left Handed";
            ModeButtonText.text = "Switch to Right Handed";
        }
        else
        {
            RightHandMode = true;
            ModeText.text = "Current mode: Right Handed";
            ModeButtonText.text = "Switch to Left Handed";
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(ReturnDestination);
    }
}
