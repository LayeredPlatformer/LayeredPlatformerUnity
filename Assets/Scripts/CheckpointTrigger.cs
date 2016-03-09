using UnityEngine;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour
{
	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint encountered! "+ other.name);
    }
}
