using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public GameObject playerGameObject;
    private CharacterController controller;
    private EquipmentManager equipmentManager;
    private Player player;

    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.equipmentManager= GetComponent<EquipmentManager>();
        this.player = playerGameObject.GetComponent<Player>();
    }

    void Update()
    {
        UpdateFacing();
        UpdateMovement();
        HandleInput();
    }

    private void UpdateFacing()
    {
        FaceTowardsMouse();
    }

    private void FaceTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 playerPosition = player.gameObject.transform.position;

        var newPlayerRotation = GetFacingDirection(playerPosition, mousePosition);
        playerGameObject.transform.rotation = newPlayerRotation;

        var equippedItem = this.equipmentManager.GetEquippedItem();
        if (equippedItem)
        {
            var weaponPosition = equippedItem.transform.position;
            var newWeaponRotation = GetFacingDirection(weaponPosition, mousePosition);
            equippedItem.transform.rotation = newWeaponRotation;
        }
    }

    private Quaternion GetFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        Vector3 sourceToTarget = targetPosition - sourcePosition;
        sourceToTarget.y = 0;
        return Quaternion.LookRotation(sourceToTarget);
    }

    private void UpdateMovement()
    {
        var moveDirection = new Vector3(
            Input.GetAxis("Horizontal"),
            0.0f,
            Input.GetAxis("Vertical"));

        moveDirection = this.transform.TransformDirection(moveDirection);
        moveDirection *= this.player.MoveSpeed;

        float gravity = 20f;
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        this.controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var equippedItem = this.equipmentManager.GetEquippedItem();
            var activatable = equippedItem.GetComponent<IActivatable>();

            if (activatable != null)
            {
                activatable.Activate();
            }
        }
    }
}
