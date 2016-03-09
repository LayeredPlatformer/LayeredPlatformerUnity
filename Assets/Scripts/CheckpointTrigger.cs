using UnityEngine;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour
{
	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SaveCheckpoint();
        }
    }
}
