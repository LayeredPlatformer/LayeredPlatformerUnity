using UnityEngine;
using System.Collections;
using System;

public class Targetable : MonoBehaviour
{
    public event EventHandler DeathEventHandler;

    public float DesiredHealth = 0f;
	public float DesiredHealAmount = 0f;

    [HideInInspector]
    public bool Invulnerable = false;


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
        if (Invulnerable)
            return;

		_canHeal = false;
		CancelInvoke ("ResetCanHeal");

        // Don't deal damage and/or die if already dead
        if (!IsDead)
        {
            Health -= amount;
//            Debug.Log("health: " + Health);

            if (IsDead)
            {
                Vector3 dieDirection = transform.position - sourcePosition;
                OnDeath(dieDirection, impactForce);
            }
            else
            {
                // Reseat healing if still alive.
                Invoke("ResetCanHeal", _healDelay);
            }
        }
	}

	public void Heal(float amount)
	{
		Health += amount;
//        Debug.Log("Health: " + Health);
	}

    private void OnDeath(Vector3 direction, float impactForce)
    {
        Debug.Log("OnDeath()");
        if (DeathEventHandler != null)
        {
            DeathEventHandler(this, new DeathEventArgs { Direction = direction, ImpactForce = impactForce });
        }
    }

    private void ResetCanHeal()
	{
		_canHeal = true;
	}
}

public class DeathEventArgs : EventArgs
{
    public Vector3 Direction { get; set; }
    public float ImpactForce { get; set; }
}
