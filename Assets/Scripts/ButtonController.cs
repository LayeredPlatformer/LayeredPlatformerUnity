using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	public Triggerable[] Triggerables;
	public bool PlayerTriggers = false;
	public bool GearTriggers = true;

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
		GearController gc = collider.GetComponent<GearController>();
		PlayerController player = collider.GetComponent<PlayerController>();
		if ((GearTriggers && gc && gc.isBeingThrown()) ||
			(PlayerTriggers && player))
        {
			foreach (Triggerable t in Triggerables)
				t.Trigger();
        }
    }
}
