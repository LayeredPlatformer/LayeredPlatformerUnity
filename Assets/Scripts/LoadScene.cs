using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string Destination;

    public void Load()
    {
        Application.LoadLevel(Destination);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
