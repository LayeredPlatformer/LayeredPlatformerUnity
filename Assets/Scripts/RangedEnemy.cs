using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour 
{
	// TODO: update to follow c# conventions
    public float Speed = .01f;
    public float MinRange = 5f;
    public float MaxRange = 10f;
    public float XOffset = 1.5f;
    public float YOffset = .5f;

    private Transform _player;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody _rb;

	// Use this for initialization
	void Start () 
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 tdir = _player.position - transform.position;
        bool sameLayers = GetComponent<LayeredController>().Layer.Equals(_player.GetComponent<LayeredController>().Layer);
        tdir.y = 0;

		//TODO: update dispenser.Dispensing depending on distance to player
        if (sameLayers)
        {
        	_spriteRenderer.flipX = tdir.x <= 0;

            if (tdir.magnitude > MinRange && tdir.magnitude < MaxRange)
			{
				if (_spriteRenderer.flipX)
					_rb.AddForce(-transform.right*Speed);
				else
					_rb.AddForce(transform.right*Speed);
			}

			Transform child = transform.GetChild(0);
            if (child.tag == "ShooterAIDispenser")
            {
                if (_spriteRenderer.flipX == true)
                    XOffset = XOffset * -1;

                Vector3 dispenserPos = new Vector3(transform.position.x + XOffset,
					transform.position.y + YOffset, transform.position.z);
                child.position = dispenserPos;
            }
        }
	}
}
