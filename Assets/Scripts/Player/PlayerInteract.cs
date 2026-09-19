using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTrans;

    [SerializeField] private float interactionDistance;

    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private int interactLayer;
    [SerializeField] private int windowLayer;

    private IInteractable _hoveredInteractable;

    public void ProcessInteractInput()
    {
        if(_hoveredInteractable != null)
        {
            _hoveredInteractable.Interact();
        }
    }

    private void Update()
    {
        if(Physics.Raycast(playerCameraTrans.position, playerCameraTrans.forward, out RaycastHit interactHit, interactionDistance))
        {
            if ((interactLayerMask & (1 << interactHit.collider.gameObject.layer)) == 0)
            {
                RemoveHoveredInteractable();
                return;
            }

            GameObject hitObject = interactHit.transform.gameObject;

            if(hitObject.layer == windowLayer)
            {
                Vector3 raycastPos = interactHit.point + WindowHandler.Instance.WindowOffset();

                if (Physics.Raycast(raycastPos, playerCameraTrans.forward, out RaycastHit windowHit, interactionDistance - interactHit.distance))
                {
                    if ((interactLayerMask & (1 << windowHit.collider.gameObject.layer)) == 0)
                    {
                        RemoveHoveredInteractable();
                        return;
                    }

                    hitObject = windowHit.transform.gameObject;
                }
                else
                {
                    RemoveHoveredInteractable();
                    return;
                }
            }

            if(_hoveredInteractable != null)
            {
                RemoveHoveredInteractable();
            }

            if(hitObject.TryGetComponent(out IInteractable newInteractable))
            {
                _hoveredInteractable = newInteractable;

                _hoveredInteractable.SetHover(true);

                InteractFeedbackUI.Instance.SetIcon(_hoveredInteractable.hoverIcon);
            }
            else
            {
                Debug.LogWarning($"Object '{hitObject.name}' in the Interactable (8) layer does not contain an interactable component!");
            }
        }
        else
        {
            RemoveHoveredInteractable();
        }
    }

    private void RemoveHoveredInteractable()
    {
        if (_hoveredInteractable != null)
        {
            InteractFeedbackUI.Instance.SetIcon(HoverIcon.Default);

            _hoveredInteractable.SetHover(false);

            _hoveredInteractable = null;
        }
    }
}
