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
            Debug.Log(interactHit.collider.gameObject.name);

            if ((interactLayerMask & (1 << interactHit.collider.gameObject.layer)) == 0) return;

            GameObject hitObject = interactHit.transform.gameObject;

            if(hitObject.layer == windowLayer)
            {
                Vector3 raycastPos = interactHit.point + WindowHandler.Instance.WindowOffset();

                if (Physics.Raycast(raycastPos, playerCameraTrans.forward, out RaycastHit windowHit, interactionDistance - interactHit.distance))
                {
                    if ((interactLayerMask & (1 << windowHit.collider.gameObject.layer)) == 0) return;

                    hitObject = windowHit.transform.gameObject;
                }
                else
                {
                    if (_hoveredInteractable != null)
                    {
                        _hoveredInteractable.SetHover(false);

                        _hoveredInteractable = null;
                    }

                    return;
                }
            }

            if(_hoveredInteractable != null)
            {
                //stuff
            }

            if(hitObject.TryGetComponent(out IInteractable newInteractable))
            {
                _hoveredInteractable = newInteractable;

                _hoveredInteractable.SetHover(true);
            }
            else
            {
                Debug.LogWarning($"Object '{hitObject.name}' in the Interactable (8) layer does not contain an interactable component!");
            }
        }
        else
        {
            if(_hoveredInteractable != null)
            {
                _hoveredInteractable.SetHover(false);

                _hoveredInteractable = null;
            }
        }
    }
}
