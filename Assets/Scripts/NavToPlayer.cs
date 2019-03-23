using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class NavToPlayer : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        this.nav.SetDestination(player.position);
    }
}
