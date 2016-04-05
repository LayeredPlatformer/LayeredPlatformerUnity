using UnityEngine;
using System.Collections;

public class DamageOnTouch : MonoBehaviour
{
	public float Damage = .1f;
	public float ImpactForce = 1000f;
	public float SlowAmount = .1f;
    public bool isShuriken;

	void OnTriggerStay(Collider collider)
	{
		if (collider.GetComponentInChildren<DamageOnTouch>())
			return;

		Targetable targetable = collider.gameObject.GetComponent<Targetable>();
        if (collider.gameObject.tag != "Player")
        {
            if (!isShuriken)
            {
                if (targetable)
                    targetable.DealDamage(Damage, transform.position, ImpactForce);
            }
        }
        else
        {
            if (targetable)
                targetable.DealDamage(Damage, transform.position, ImpactForce);
        }
		Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
		if (rb)
		{
			rb.velocity *= SlowAmount;
		}
	}
}
