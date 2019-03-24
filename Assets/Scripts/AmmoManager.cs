using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AmmoManager : MonoBehaviour
{
    public GameObject AmmoUIContainer;

    public Slider AmmoUISlider;
    public Text AmmoUIText;

    public Dictionary<AmmoTypes, int> Ammunition = new Dictionary<AmmoTypes, int>();

    private EquipmentManager equipmentManager;

    public void SetAmmo(AmmoTypes ammoTypes, int amount)
    {
        this.Ammunition[ammoTypes] = amount;
    }

    public int GetAmmo(AmmoTypes ammoType)
    {
        if (!Ammunition.ContainsKey(ammoType))
        {
            return 0;
        }

        return this.Ammunition[ammoType];
    }

    void Awake()
    {
        this.Ammunition = new Dictionary<AmmoTypes, int>();

        this.equipmentManager = this.GetComponent<EquipmentManager>();
    }

    void Update()
    {
        UpdateAmmoUI();
    }

    private void HideAmmoUI()
    {
        this.AmmoUIContainer.SetActive(false);
    }

    private void SetAmmoUI(int weaponAmmo, int weaponMaxAmmo, int totalAmmo)
    {
        this.AmmoUIContainer.SetActive(true);

        var sliderValue = (float)weaponAmmo / (float)weaponMaxAmmo;
        this.AmmoUISlider.value = sliderValue;

        var ammoUIText = string.Format("{0} / {1}", weaponAmmo, totalAmmo);
        this.AmmoUIText.text = ammoUIText;
    }

    private void UpdateAmmoUI()
    {
        var equippedItem = this.equipmentManager.GetEquippedItem();

        if (!equippedItem)
        {
            this.HideAmmoUI();
            return;
        }

        var projectileWeapon = equippedItem.GetComponent<ProjectileWeapon>();
        if (!projectileWeapon)
        {
            this.HideAmmoUI();
            return;
        }

        var weaponAmmo = projectileWeapon.GetAmmoInWeapon();
        var weaponMaxAmmo = projectileWeapon.MaxAmmunition;
        var totalAmmo = this.GetAmmo(projectileWeapon.AmmoType);
        this.SetAmmoUI(weaponAmmo, weaponMaxAmmo, totalAmmo);
    }
}