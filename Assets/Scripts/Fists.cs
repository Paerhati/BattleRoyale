using UnityEngine;
using System.Collections.Generic;

public class Fists : MonoBehaviour, ITriggerEnteredCapturer
{
    public int KnockBack = 5;
    public int Damage = 1;
    private Animator animator;

    private bool isAttacking = false;
    private HashSet<int> damagedEnemies = new HashSet<int>();

    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            this.Activate();
        }
    }

    public void IsAttacking()
    {
        this.isAttacking = true;
    }

    public void IsNotAttacking()
    {
        this.damagedEnemies.Clear();
        this.isAttacking = false;
    }

    public void OnCaptureTriggerEntered(GameObject source, Collider collider)
    {
        if (!this.isAttacking)
        {
            return;
        }

        if (collider.tag != "Enemy")
        {
            return;
        }

        var id = collider.gameObject.GetInstanceID();
        if (this.damagedEnemies.Contains(id))
        {
            // Don't damage an enemy twice in the same swing
            return;
        }
        this.damagedEnemies.Add(id);

        var enemyHealth = collider.GetComponent<Health>();
        enemyHealth.TakeDamageAndKnockBack(
            this.Damage,
            this.transform.position,
            this.KnockBack);
    }

    private void Activate()
    {
        this.animator.SetTrigger("Activate");
    }
}