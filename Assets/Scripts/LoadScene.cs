using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string Destination;

    public void Load()
    {
        string returnDest=SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Destination);
        if(Destination=="SettingsScreen")
        {
            SettingsManager.ReturnDestination = returnDest;
        }
    }
}
