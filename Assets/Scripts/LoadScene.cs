using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string Destination;

    public void Load()
    {
        SceneManager.LoadScene(Destination);
//        Application.LoadLevel(Destination);
    }
}
