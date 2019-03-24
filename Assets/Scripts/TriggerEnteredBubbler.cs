using UnityEngine;

public class TriggerEnteredBubbler : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        var triggerEnteredCapturer = this.GetComponentInParent<ITriggerEnteredCapturer>();
        triggerEnteredCapturer.OnCaptureTriggerEntered(this.gameObject, collider);
    }
}