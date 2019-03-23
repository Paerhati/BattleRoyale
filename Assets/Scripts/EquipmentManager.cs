using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public List<GameObject> Equipment;
    private GameObject equippedItem;

    public GameObject GetEquippedItem()
    {
        return this.equippedItem;
    }

    void Awake()
    {
        this.equippedItem = Equipment[0];
        this.equippedItem.SetActive(true);
    }

    void Start()
    {
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
    }

    private void SwitchEquippedItem(int index)
    {
        if (index < 0 || index >= this.Equipment.Count)
        {
            return;
        }

        var nextItem = this.Equipment[index];
        if (this.equippedItem == nextItem)
        {
            return;
        }

        this.equippedItem.SetActive(false);
        this.equippedItem = nextItem;
        nextItem.SetActive(true);
    }
}
