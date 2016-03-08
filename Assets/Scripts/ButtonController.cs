using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	public Triggerable[] Triggerables;

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
			foreach (Triggerable t in Triggerables)
				t.Trigger();
        }
    }
}
