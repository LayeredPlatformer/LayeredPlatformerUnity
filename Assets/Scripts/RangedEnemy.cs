using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour 
{

    public float speed = .01f;
    public float Range = 5f;
    public float maxRange = 10f;
    public float xOffset = 1.5f;
    public float yOffset = .5f;
    private Transform Player;
    private SpriteRenderer SpriteRenderer;
    private Rigidbody rb;
    public Transform ShooterTransform;

	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        ShooterTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 tpos = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        Vector3 tdir = tpos - transform.position;
        Debug.Log(tdir);
        bool sameLayers = GetComponent<LayeredController>().Layer.Equals(Player.GetComponent<LayeredController>().Layer);
        tdir.y = 0;
        if (tdir.x > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else
        {
            SpriteRenderer.flipX = true;
        }
        if (sameLayers)
        {
            if (tdir.magnitude > Range && tdir.magnitude < maxRange)
            {
                tdir = tdir.normalized * speed;
                ShooterTransform.position += tdir;
            }
            Transform child = ShooterTransform.GetChild(0);
            if (child.tag == "ShooterAIDispenser")
            {
                if (SpriteRenderer.flipX == true)
                {

                    xOffset = xOffset * -1;
                }
                Vector3 dispenserPos = new Vector3(ShooterTransform.position.x + xOffset, ShooterTransform.position.y + yOffset, ShooterTransform.position.z);
                child.position = dispenserPos;
            }
        }
	}
}
