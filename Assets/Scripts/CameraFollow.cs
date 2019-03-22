using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    void Update()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(
            this.transform.position,
            desiredPosition,
            smoothSpeed);

        transform.position = new Vector3(
            smoothedPosition.x,
            transform.position.y,
            smoothedPosition.z);
    }
}