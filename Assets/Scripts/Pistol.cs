using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour, IActivatable
{
    public Transform FirePoint;
    public GameObject ImpactEffect;
    public GameObject BulletEffect;

    public int Ammunition = 10;
    public int Damage = 1;

    private AudioSource ShootingSound;

    void Awake()
    {
        this.ShootingSound = this.GetComponent<AudioSource>();
    }

    public void Activate()
    {
        Shoot();
    }

    private void Shoot()
    {
        ShootingSound.Play();
        var bullet = Instantiate(BulletEffect, FirePoint.position, Quaternion.identity);
        var bulletEffect = bullet.GetComponent<BulletEffect>();
        bulletEffect.SetStart(FirePoint.position);
        bulletEffect.SetEnd(FirePoint.position + FirePoint.forward * 100);

        RaycastHit hitInfo;
        if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hitInfo))
        {
            bulletEffect.SetEnd(hitInfo.point);

            var health = hitInfo.transform.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(Damage);
            }

            Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
        }
    }
}
