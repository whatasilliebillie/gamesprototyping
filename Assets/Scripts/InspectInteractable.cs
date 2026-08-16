using UnityEngine;

public class InspectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Inspectable inspectPrefab;

    public Inspectable InspectPrefab => inspectPrefab;

    public void Interact()
    {
        PlayerUIHandler.Instance.StartInspect(this);
    }

    public void SetHover(bool toggle)
    {

    }
}
