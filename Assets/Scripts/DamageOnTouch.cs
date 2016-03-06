using UnityEngine;
using System.Collections;

public class DamageOnTouch : MonoBehaviour
{
	public float _damage = .1f;
	public float _impactForce = 1000f;

	void OnTriggerStay(Collider collider)
	{
		Targetable targetable = collider.gameObject.GetComponent<Targetable>();
		if (targetable != null)
			targetable.DealDamage(_damage, transform.position, _impactForce);
	}
}
