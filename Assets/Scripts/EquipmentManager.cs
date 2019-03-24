using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public List<GameObject> Equipment;
    public int CurrentlyEquippedIndex = 0;

    public GameObject GetEquippedItem()
    {
        return this.Equipment[this.CurrentlyEquippedIndex];
    }

    void Awake()
    {
        this.GetEquippedItem().SetActive(true);
    }

    void Update()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        var equippedItem = this.GetEquippedItem();
        var activatable = equippedItem.GetComponent<IActivatable>();

        if (activatable != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                activatable.Activate();
            }
            if (Input.GetButton("Fire1"))
            {
                activatable.Activating();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwitchEquippedItem(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SwitchEquippedItem(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SwitchEquippedItem(2);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            var reloadable = equippedItem.GetComponent<IReloadable>();
            if (reloadable != null)
            {
                reloadable.Reload();
            }
        }
    }

    private void SwitchEquippedItem(int index)
    {
        if (index < 0 ||
            index >= this.Equipment.Count ||
            index == this.CurrentlyEquippedIndex)
        {
            return;
        }

        this.GetEquippedItem().SetActive(false);
        this.CurrentlyEquippedIndex = index;
        this.GetEquippedItem().SetActive(true);
    }
}
