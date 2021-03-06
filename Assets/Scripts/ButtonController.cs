﻿using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	public Triggerable[] Triggerables;
	public bool PlayerActivates = false;
	public bool GearActivates = true;

	//TODO: ontriggerenter or ontriggerstay (permanent or not)

    void OnTriggerStay(Collider collider)
    {
		GearController gc = collider.GetComponent<GearController>();
		PlayerController player = collider.GetComponent<PlayerController>();
		if ((GearActivates && gc && gc.isBeingThrown()) ||
			(PlayerActivates && player))
        {
			foreach (Triggerable t in Triggerables)
				t.Trigger();
        }
    }
}
