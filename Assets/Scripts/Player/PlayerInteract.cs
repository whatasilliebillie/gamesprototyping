using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTrans;
    private Camera _playerCamera;

    [SerializeField] private float defaultInteractDistance;
    [SerializeField] private float focusInteractDistance;

    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private int interactLayer;
    [SerializeField] private int windowLayer;

    private InteractionMode _interactionMode = InteractionMode.Default;

    private IInteractable _hoveredInteractable;

    private bool _gamePaused = false;

    public void ProcessInteractInput()
    {
        if(_hoveredInteractable != null && !_gamePaused)
        {
            _hoveredInteractable.Interact();
        }
    }

    private void OnEnable()
    {
        PlayerUIHandler.GamePauseEvent += OnGamePause;
    }

    private void Start()
    {
        _playerCamera = playerCameraTrans.GetComponent<Camera>();
    }

    private void Update()
    {
        if(_interactionMode == InteractionMode.Focus)
        {
            RayToInteract(_playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue()), focusInteractDistance);
        }
        else
        {
            RayToInteract(new Ray(playerCameraTrans.position, playerCameraTrans.forward), defaultInteractDistance);
        }
    }

    private void RayToInteract(Ray interactRay, float interactDistance)
    {
        if (Physics.Raycast(interactRay, out RaycastHit interactHit, interactDistance, raycastLayerMask))
        {
            if ((interactLayerMask & (1 << interactHit.collider.gameObject.layer)) == 0)
            {
                RemoveHoveredInteractable();
                return;
            }

            GameObject hitObject = interactHit.transform.gameObject;

            if (hitObject.layer == windowLayer)
            {
                Vector3 raycastPos = interactHit.point + WindowHandler.Instance.WindowOffset();

                if (Physics.Raycast(raycastPos, playerCameraTrans.forward, out RaycastHit windowHit, defaultInteractDistance - interactHit.distance))
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

            if (_hoveredInteractable != null)
            {
                RemoveHoveredInteractable();
            }

            if (hitObject.TryGetComponent(out IInteractable newInteractable))
            {
                if (newInteractable.InteractionEnabled)
                {
                    _hoveredInteractable = newInteractable;

                    InteractFeedbackUI.Instance.SetIcon(_hoveredInteractable.CurHoverIcon);

                }
                else
                {
                    RemoveHoveredInteractable();
                }
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

    private void OnGamePause(bool isPaused)
    {
        _gamePaused = isPaused;
    }

    public void SetInteractionMode(InteractionMode newInteractMode)
    {
        _interactionMode = newInteractMode;
    }

    private void RemoveHoveredInteractable()
    {
        if (_hoveredInteractable != null)
        {
            InteractFeedbackUI.Instance.SetIcon(HoverIcon.Default);

            _hoveredInteractable = null;
        }
    }

    private void OnDisable()
    {
        PlayerUIHandler.GamePauseEvent -= OnGamePause;
    }
}

public enum InteractionMode
{
    Default, Focus
}