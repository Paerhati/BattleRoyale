using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ProjectileWeapon : MonoBehaviour, IActivatable, IReloadable
{
    public AudioClip ShotAudio;
    public AudioClip ReloadAudio;

    public Transform FirePoint;
    public GameObject ImpactEffect;
    public GameObject ProjectilePrefab;
    public AmmoTypes AmmoType;

    public bool IsAutomatic = false;
    public int Damage = 1;
    public int Knockback = 5;
    public int MaxAmmunition = 10;

    public float CoolDown = .01f;
    public float ReloadTime = 1f;

    private float coolDownTimer = 0.1f;
    private float reloadTimer = 0f;

    private int ammo;

    private AudioSource audioSource;

    void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.ammo = this.MaxAmmunition;
    }

    void Update()
    {
        this.reloadTimer = Utils.CountDownTimer(this.reloadTimer);
        this.coolDownTimer = Utils.CountDownTimer(this.coolDownTimer);

        this.UpdateAmmo();
    }

    public void Reload()
    {
        if (!this.CanReload())
        {
            return;
        }

        this.audioSource.PlayOneShot(ReloadAudio);
        this.reloadTimer = this.ReloadTime;
    }

    public int GetAmmoInWeapon()
    {
        return this.ammo;
    }

    public virtual void Activate()
    {
        if (this.IsReloading())
        {
            return;
        }

        if (this.CanShoot())
        {
            Shoot();
        }
        else
        {
            if (!this.HasAmmo())
            {
                Reload();
            }
        }
    }

    public virtual void Activating()
    {
        if (!this.IsAutomatic)
        {
            return;
        }

        this.Activate();
    }

    private void UpdateAmmo()
    {
        if (!this.IsReloading())
        {
            return;
        }

        var reloadPercentage = (this.ReloadTime - this.reloadTimer) / this.ReloadTime;
        this.ammo = (int)Mathf.Round(reloadPercentage * this.MaxAmmunition);
    }

    private bool CanShoot()
    {
        if (!this.HasAmmo())
        {
            return false;
        }

        if (this.IsReloading())
        {
            return false;
        }

        if (this.coolDownTimer > 0)
        {
            return false;
        }

        return true;
    }

    private bool CanReload()
    {
        return !this.IsReloading() && this.ammo < this.MaxAmmunition;
    }

    private bool HasAmmo()
    {
        return this.ammo > 0;
    }

    private bool IsReloading()
    {
        return this.reloadTimer > 0;
    }

    private void Shoot()
    {
        this.coolDownTimer = this.CoolDown;
        this.ammo --;
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
                health.TakeDamageAndKnockBack(
                    this.Damage,
                    hitInfo.point,
                    this.Knockback);
            }

            Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
        }
    }
}
