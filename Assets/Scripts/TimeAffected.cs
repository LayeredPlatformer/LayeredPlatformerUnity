using UnityEngine;
using System.Collections;

public class TimeAffected : LayeredController
{
	public bool isParent = true;

	protected bool canUpdatePast = false;
	protected TimeAffected shadow;

	int updateDelay = 1;
	int counter = 0;
	SpriteRenderer rend;
	Component[] components = new Component[0];
	Vector3[] previousPositions;

	// Use this for initialization
	public void initialize ()
	{
		base.Initialize();
		rend = GetComponent<SpriteRenderer>();
		previousPositions = new Vector3[60*updateDelay];

		if (isParent)
		{
			Invoke("toggleCanUpdatePast", updateDelay);
			GameObject otherGO = (GameObject) Instantiate(gameObject, transform.position, transform.localRotation);
			shadow = otherGO.GetComponent<TimeAffected>();
			shadow.isParent = false;
			shadow.initialize();
			shadow.toggleReality();
		}
	}

	protected void step()
	{
		base.Step();
		previousPositions[counter % (previousPositions.Length)] = transform.position;
		counter++;

		if (canUpdatePast)
			shadow.transform.position = previousPositions[(counter + previousPositions.Length) % previousPositions.Length];
	}
	
	void toggleCanUpdatePast ()
	{
		canUpdatePast = !canUpdatePast;
	}

	public void toggleReality()
	{
		if (components.Length == 0)
		{
			components = GetComponents<Component>();
			for (int i=0; i<components.Length; i++)
			{
				if (!(components[i] is Renderer || components[i] is TimeAffected)
					|| components[i] is Transform || components[i] is PlatformCharacter3D)
				{
					Destroy(components[i]);
//					Debug.Log("removed component: " + i);
				}
			}
		}
		toggleOpacity();
	}

	void toggleOpacity()
	{
		if (rend.color.a == .5f)
			rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1f);
		else
			rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, .5f);
	}
}
