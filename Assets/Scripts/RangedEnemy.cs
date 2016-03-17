using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour 
{
	// TODO: update to follow c# conventions
    public float speed = .01f;
    public float Range = 5f;
    public float maxRange = 10f;
    public float xOffset = 1.5f;
    public float yOffset = .5f;
    private Transform Player;
    private SpriteRenderer SpriteRenderer;
    private Rigidbody rb;

	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 tdir = Player.position - transform.position;
        bool sameLayers = GetComponent<LayeredController>().Layer.Equals(Player.GetComponent<LayeredController>().Layer);
        tdir.y = 0;

		//TODO: update dispenser.Dispensing depending on distance to player
        if (sameLayers)
        {
        	SpriteRenderer.flipX = tdir.x <= 0;
            if (tdir.magnitude > Range && tdir.magnitude < maxRange)
            {
                tdir = tdir.normalized * speed;
                transform.position += tdir;
            }

			Transform child = transform.GetChild(0);
            if (child.tag == "ShooterAIDispenser")
            {
                if (SpriteRenderer.flipX == true)
                    xOffset = xOffset * -1;

                Vector3 dispenserPos = new Vector3(transform.position.x + xOffset,
					transform.position.y + yOffset, transform.position.z);
                child.position = dispenserPos;
            }
        }
	}
}
