using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyTarget : MonoBehaviour, IHasHealth
{
    public int Health = 10;

    private WaitForSeconds damageDuration = new WaitForSeconds(0.1f);
    private Renderer rend;

    public int GetHealth()
    {
        return this.Health;
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
        {
            Destroy(this.gameObject);
        }

        StartCoroutine(DamageEffect());
    }

    void Awake()
    {
        this.rend = GetComponent<Renderer>();
    }

    private IEnumerator DamageEffect()
    {
        Color previousColor = rend.material.color;

        rend.material.color = Color.red;
        yield return damageDuration;
        rend.material.color = previousColor;
    }
}
