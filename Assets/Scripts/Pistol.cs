using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour, IActivatable
{
    public GameObject ImpactEffect;
    public GameObject BulletEffect;
    public Transform FirePoint;
    public int Ammunition = 10;
    public int Damage = 1;

    public void Activate()
    {
        Shoot();
    }

    private void Shoot()
    {
        var bullet = Instantiate(BulletEffect, FirePoint.position, Quaternion.identity);
        var bulletEffect = bullet.GetComponent<BulletEffect>();
        bulletEffect.SetStart(FirePoint.position);
        bulletEffect.SetEnd(FirePoint.position + FirePoint.forward * 100);

        RaycastHit hitInfo;
        if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hitInfo))
        {
            bulletEffect.SetEnd(hitInfo.point);

            var hasHealth = hitInfo.transform.gameObject.GetComponent<IHasHealth>();
            if (hasHealth != null)
            {
                hasHealth.TakeDamage(Damage);
            }

            Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
        }
    }
}
