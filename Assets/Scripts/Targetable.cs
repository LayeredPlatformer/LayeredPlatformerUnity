using UnityEngine;
using System.Collections;

public class Targetable : MonoBehaviour
{

	public float DesiredHealth = 0f;
	public GameObject[] DeadBodies;
	public float DeadBodyForce = 10f;
	public float DesiredHealAmount = 0f;

	float _maxHealth;
	float _health;
	float _healAmount;
	bool _canHeal;
	int _healTimer;
	int _healDelay = 3;

	void Start()
	{
		if (DesiredHealth > 0)
			InitializeHealth(DesiredHealth);
		if (DesiredHealAmount > 0)
			InitializeHealAmount(DesiredHealAmount);
	}

	void Update()
	{
		Step();
	}

	public void InitializeHealAmount(float h)
	{
		_healAmount = h;
	}

	public void InitializeHealth(float h)
	{
		_maxHealth = h;
		_health = h;
	}

	protected virtual void Step()
	{
		if (_canHeal)
			Heal(_healAmount);
	}

	public void DealDamage (float amount, Vector3 sourcePosition, float impactForce) 
	{
		_canHeal = false;
		CancelInvoke ("resetCanHeal");
		Invoke ("resetCanHeal", _healDelay);
		_health -= amount;
		if (IsDead())
		{
			Vector3 dieDirection = transform.position - sourcePosition;
			Die(dieDirection, impactForce);
		}
	}

	public void Heal(float amount)
	{
		_health += amount;
		if (_health > _maxHealth)
			_health = _maxHealth;
	}

	bool IsDead ()
	{
		return _health <= 0;
	}

	virtual public void Die (Vector3 direction, float impactForce) 
	{
		for (int i=0; i<DeadBodies.Length; i++)
		{
			GameObject body = (GameObject) Instantiate(DeadBodies[i], transform.position, transform.localRotation);
			body.transform.localScale = transform.localScale;
			body.transform.rotation = transform.rotation;
			Rigidbody[] rbs = body.GetComponentsInChildren<Rigidbody>();
			for (int j = 0; j < rbs.Length; j++)
			{
				Vector3 randDir = new Vector3 (Random.Range(-DeadBodyForce, DeadBodyForce),
					Random.Range(-DeadBodyForce, DeadBodyForce), 0);
				rbs[j].AddForce(direction * impactForce);
			}
		}

		Destroy(gameObject);
		Destroy(this);
	}

	void ResetCanHeal ()
	{
		_canHeal = true;
	}

	public float GetMaxHealth()
	{
		return _maxHealth;
	}

	public float GetHealth()
	{
		return _health;
	}
}
