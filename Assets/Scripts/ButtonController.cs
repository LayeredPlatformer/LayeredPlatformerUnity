using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	public Triggerable Triggerable;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay(Collider collider)
    {
		if (collider.gameObject.tag.Equals("Gear"))
        {
			Triggerable.Trigger();
        }
    }
}
