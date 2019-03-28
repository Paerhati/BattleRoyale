using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public Joystick FacingJoystick;
    public Button ChangeWeaponButton;
    public List<GameObject> Equipment;
    public int CurrentlyEquippedIndex = 0;

    public GameObject GetEquippedItem()
    {
        return this.Equipment[this.CurrentlyEquippedIndex];
    }

    void Awake()
    {
        this.GetEquippedItem().SetActive(true);
        this.ChangeWeaponButton.onClick.AddListener(LoopThroughEquipment);
    }

    void Update()
    {
        HandleTouchInput();
        HandleKeyboardInput();
    }

    private void LoopThroughEquipment()
    {
        var nextIndex = (this.CurrentlyEquippedIndex + 1) % this.Equipment.Count;
        SwitchEquippedItem(nextIndex);
    }

    private void HandleTouchInput()
    {
        var horizontal = FacingJoystick.Horizontal;
        var vertical = FacingJoystick.Vertical;

        var equippedItem = this.GetEquippedItem();
        var activatable = equippedItem.GetComponent<IActivatable>();

        if (horizontal != 0 && vertical != 0)
        {
            if (activatable != null)
            {
                activatable.Activate();
            }
        }
    }

    private void HandleKeyboardInput()
    {
        var equippedItem = this.GetEquippedItem();
        var activatable = equippedItem.GetComponent<IActivatable>();

        if (activatable != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // activatable.Activate();
            }
            if (Input.GetButton("Fire1"))
            {
                // activatable.Activating();
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
