using UnityEngine;
using UnityEngine.Events;

public class EventBridge : MonoBehaviour
{
    public UnityEvent BridgingEvent;

    public void InvokeBridgingEvent()
    {
        BridgingEvent.Invoke();
    }
}
