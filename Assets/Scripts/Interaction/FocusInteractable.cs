using UnityEngine;

public class FocusInteractable : MonoBehaviour, IInteractable
{
    public Transform CameraTarget;

    [SerializeField] private Collider focusCollider;
    [SerializeField] private Collider[] internalColliders;

    public HoverIcon CurHoverIcon => HoverIcon.Eye;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public void Interact()
    {
        StartFocus();
    }

    private void StartFocus()
    {
        PlayerUIHandler.Instance.StartFocus(this);

        ToggleInternalColliders(true);

        focusCollider.enabled = false;
    }

    public void StopFocus()
    {
        ToggleInternalColliders(false);

        focusCollider.enabled = true;
    }

    private void ToggleInternalColliders(bool colliderEnabled)
    {
        for(int i = 0; i < internalColliders.Length; i++)
        {
            internalColliders[i].enabled = colliderEnabled;
        }
    }
}
