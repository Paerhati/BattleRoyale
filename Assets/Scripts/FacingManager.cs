using UnityEngine;

public class FacingManager : MonoBehaviour
{
    public Joystick FacingJoystick;

    private EquipmentManager equipmentManager;

    void Start()
    {
        this.equipmentManager= GetComponent<EquipmentManager>();
    }

    void Update()
    {
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        var horizontal = FacingJoystick.Horizontal;
        var vertical = FacingJoystick.Vertical;

        if (horizontal != 0 && vertical != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(new Vector3(horizontal, 0, vertical));
        }
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
        TurnTowardsPoint(this.transform, point);

        var equippedItem = this.equipmentManager.GetEquippedItem();
        if (equippedItem)
        {
            TurnTowardsPoint(equippedItem.transform, point);
        }
    }

    private void TurnTowardsPoint(Transform sourceTransform, Vector3 point)
    {
        Vector3 sourceToTarget = point - sourceTransform.position;
        sourceToTarget.y = 0;

        sourceTransform.rotation = Quaternion.LookRotation(sourceToTarget);
    }
}