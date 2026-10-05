using UnityEngine;

public class FocusInteractable : MonoBehaviour, IInteractable
{
    [Header("Components")]
    public Transform CameraTarget;

    [Header("Colliders")]
    [SerializeField] private Collider focusCollider;
    [SerializeField] private Collider[] internalColliders;

    [Header("Camera Manipulation")]
    [SerializeField] private Vector3 cameraOffset;

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

        if(cameraOffset != Vector3.zero)
        {
            PlayerLook.Instance.MoveCamera(cameraOffset);
        }

        focusCollider.enabled = false;
    }

    public void StopFocus()
    {
        WindowHandler.Instance.ToggleSpawnEnabled(true);

        ToggleInternalColliders(false);

        if (cameraOffset != Vector3.zero)
        {
            PlayerLook.Instance.MoveCamera(-cameraOffset);
        }

        focusCollider.enabled = true;
    }

    public void ToggleInteraction(bool interactEnabled)
    {
        _interactionEnabled = interactEnabled;
    }

    private void ToggleInternalColliders(bool colliderEnabled)
    {
        for(int i = 0; i < internalColliders.Length; i++)
        {
            internalColliders[i].enabled = colliderEnabled;
        }
    }
}
