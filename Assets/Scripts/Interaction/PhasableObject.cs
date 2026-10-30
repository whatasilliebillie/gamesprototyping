using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhaseableObject : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GhostGlow glow = other.GetComponentInParent<GhostGlow>();

        if (glow != null) glow.EnterZone();
    }

    private void OnTriggerExit(Collider other)
    {
        GhostGlow glow = other.GetComponentInParent<GhostGlow>();

        if (glow != null) glow.ExitZone();
    }
}