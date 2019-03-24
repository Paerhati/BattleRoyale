using UnityEngine;

public interface ITriggerEnteredCapturer
{
    void OnCaptureTriggerEntered(GameObject source, Collider collider);
}