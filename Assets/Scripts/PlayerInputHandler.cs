using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerLook playerLook;
    private PlayerMovement playerMovement;
    private PlayerInteract playerInteract;

    private WorkerInput inputActions;

    private void Awake()
    {
        inputActions = new WorkerInput();

        playerMovement = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
    }

    private void OnEnable()
    {
        inputActions.OnFoot.Enable();

        inputActions.OnFoot.Interact.performed += ctx => playerInteract.ProcessInteractInput();
        inputActions.OnFoot.OpenWindow.performed += ctx => WindowHandler.Instance.ProcessOpenWindowInput();
    }

    private void Update()
    {
        playerMovement.ProcessMoveInput(inputActions.OnFoot.Movement.ReadValue<Vector2>());
        playerLook.ProcessLookInput(inputActions.OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.OnFoot.Disable();

        inputActions.OnFoot.Interact.performed -= ctx => playerInteract.ProcessInteractInput();
        inputActions.OnFoot.OpenWindow.performed -= ctx => WindowHandler.Instance.ProcessOpenWindowInput();
    }
}
