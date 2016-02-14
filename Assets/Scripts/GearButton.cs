using UnityEngine;
using System.Collections;

public class GearButton : MonoBehaviour {

    public string Gear = "Gear";

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Gear)
        {
            //The large gear entered the box. Do something.
            Debug.Log("The gear hit the box.");
        }
    }
}
