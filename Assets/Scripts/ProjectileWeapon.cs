using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileWeapon : MonoBehaviour, IActivatable
{
    public Transform FirePoint;
    public GameObject ImpactEffect;
    public GameObject ProjectilePrefab;
    public AudioClip ShotAudio;

    public int Ammunition = 10;
    public int Damage = 1;

    private AudioSource audioSource;

    void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public virtual void Activate()
    {
        Shoot();
    }

    public virtual void Activating()
    {
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(ShotAudio);

        var projectileObject = Instantiate(ProjectilePrefab, FirePoint.position, Quaternion.identity);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.SetSource(FirePoint.position);
        projectile.SetTarget(FirePoint.position + FirePoint.forward * 100);

        RaycastHit hitInfo;
        if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hitInfo))
        {
            projectile.SetTarget(hitInfo.point);

            var health = hitInfo.transform.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(Damage);
            }

            Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
        }
    }
}
