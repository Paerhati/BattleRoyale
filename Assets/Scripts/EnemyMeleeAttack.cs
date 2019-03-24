using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float Cooldown = 1f;
    public int Damage = 1;

    private GameObject player;
    private MovementManager playerMovementManager;
    private Health playerHealth;
    private bool playerInRange = false;
    private float attackTimer;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerHealth = player.GetComponent<Health>();
        this.playerMovementManager = player.GetComponent<MovementManager>();
        this.attackTimer = this.Cooldown;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            this.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player)
        {
            this.playerInRange = false;
        }
    }

    void Update()
    {
        if (this.CanAttackPlayer())
        {
            this.AttackPlayer();
        }

        this.attackTimer = Utils.CountDownTimer(this.attackTimer);
    }

    private bool CanAttackPlayer()
    {
        return this.attackTimer <= 0 && this.playerInRange;
    }

    private void AttackPlayer()
    {
        this.attackTimer = this.Cooldown;
        this.playerHealth.TakeDamage(this.Damage);
        this.KnockBackPlayer();
    }

    private void KnockBackPlayer()
    {
        Vector3 knockDirection = this.player.transform.position - this.transform.position;
        knockDirection = knockDirection.normalized;
        this.playerMovementManager.KnockBack(knockDirection);
    }
}