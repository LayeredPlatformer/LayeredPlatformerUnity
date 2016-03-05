using UnityEngine;
using System.Collections;
using System;

public class Targetable : MonoBehaviour
{
    public event EventHandler DeathEventHandler;

    public float DesiredHealth = 0f;
	public GameObject[] DeadBodies;
	public float DeadBodyForce = 10f;
	public float DesiredHealAmount = 0f;

    public float Health
    {
        get { return _health; }
        private set
        {
            _health = Mathf.Max(Mathf.Min(MaxHealth, value), 0);
        }
    }

    public bool IsDead
    {
        get { return Health == 0; }
    }

    public float MaxHealth { get; private set; }
    public float HealAmount { get; private set; }

    private float _health;
    private bool _canHeal;
    private int _healTimer;
	private int _healDelay = 3;

	void Start()
	{
		InitializeHealth(DesiredHealth);
		InitializeHealAmount(DesiredHealAmount);
	}

	void Update()
	{
		Step();
	}

	public void InitializeHealAmount(float h)
	{
        HealAmount = Mathf.Max(h, 0);
	}

	public void InitializeHealth(float h)
	{
        MaxHealth = Mathf.Max(h, 0);
		Health = h;
	}

	protected virtual void Step()
	{
		if (_canHeal && HealAmount > 0 && Health < MaxHealth)
			Heal(HealAmount);
	}

	public void DealDamage (float amount, Vector3 sourcePosition, float impactForce) 
	{
		_canHeal = false;
		CancelInvoke ("ResetCanHeal");
		Invoke ("ResetCanHeal", _healDelay);

		Health -= amount;
		Debug.Log("health: " + Health);

		if (IsDead)
		{
            OnDeath();
            Vector3 dieDirection = transform.position - sourcePosition;
			Die(dieDirection, impactForce);
		}
	}

	public void Heal(float amount)
	{
		Health += amount;
        Debug.Log("Health: " + Health);
	}

	public virtual void Die(Vector3 direction, float impactForce) 
	{
		for (int i = 0; i < DeadBodies.Length; i++)
		{
			GameObject body = (GameObject) Instantiate(DeadBodies[i], transform.position, transform.localRotation);
			body.transform.localScale = transform.localScale;
			body.transform.rotation = transform.rotation;
			Rigidbody[] rbs = body.GetComponentsInChildren<Rigidbody>();
			for (int j = 0; j < rbs.Length; j++)
			{
				Vector3 randDir = new Vector3 (UnityEngine.Random.Range(-DeadBodyForce, DeadBodyForce),
                    UnityEngine.Random.Range(-DeadBodyForce, DeadBodyForce), 0);
				rbs[j].AddForce(direction * impactForce);
			}
		}

		Destroy(gameObject);
		Destroy(this);
	}

    protected virtual void OnDeath()
    {
        if (DeathEventHandler != null)
        {
            DeathEventHandler(this, new EventArgs());
        }
    }

    private void ResetCanHeal()
	{
		_canHeal = true;
	}
}
