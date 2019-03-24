using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public int MaxHealth = 10;
    public int CurrentHealth;

    private Renderer[] renderers;

    public virtual void Awake()
    {
        this.CurrentHealth = this.MaxHealth;
        this.renderers = this.GetComponentsInChildren<Renderer>();
    }

    public void TakeDamageAndKnockBack(int damage, Vector3 knockBackPoint, int knockBackForce = 5)
    {
        var knockBackManager = this.GetComponent<KnockBackManager>();
        if (knockBackManager)
        {
            knockBackManager.KnockBack(knockBackPoint, knockBackForce);
        }

        this.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        this.CurrentHealth -= damage;
        this.OnDamage();
    }

    protected virtual void OnDamage()
    {
        if (this.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(Effects.SimpleDamageEffect(this.renderers));
        }
    }
}