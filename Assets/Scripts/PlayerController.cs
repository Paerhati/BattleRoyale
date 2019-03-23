using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public int PlayerMoveSpeed;

    private CharacterController controller;
    private EquipmentManager equipmentManager;

    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.equipmentManager= GetComponent<EquipmentManager>();
    }

    void Update()
    {
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        FaceTowardsMouse();
    }

    private void FaceTowardsMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            FaceTowardsPoint(hit.point);
        }
    }

    private void FaceTowardsPoint(Vector3 point)
    {
        TurnTowardsPoint(this.transform, point, false);

        var equippedItem = this.equipmentManager.GetEquippedItem();
        if (equippedItem)
        {
            TurnTowardsPoint(equippedItem.transform, point);
        }
    }

    private void TurnTowardsPoint(Transform sourceTransform, Vector3 point, bool allowVerticalChange = true)
    {
        Vector3 sourceToTarget = point - sourceTransform.position;

        if (allowVerticalChange)
        {
            sourceToTarget.y -= 0.1f;
            sourceToTarget.y = Mathf.Max(sourceToTarget.y, 0);
        }
        else
        {
            sourceToTarget.y = 0;
        }

        sourceTransform.rotation = Quaternion.LookRotation(sourceToTarget);
    }
}
