using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerLook playerLook;
    private PlayerMovement playerMovement;
    private PlayerInteract playerInteract;
    private PlayerUIHandler playerUIHandler;

    private WorkerInput inputActions;

    private void Awake()
    {
        inputActions = new WorkerInput();

        playerMovement = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
        playerUIHandler = GetComponent<PlayerUIHandler>();
    }

    private void OnEnable()
    {
        inputActions.OnFoot.Enable();

        inputActions.OnFoot.Interact.performed += ctx => playerInteract.ProcessInteractInput();
        inputActions.OnFoot.ExitUI.performed += ctx => playerUIHandler.CloseInspect();

        inputActions.OnFoot.OpenWindow.performed += ctx => WindowHandler.Instance.ProcessOpenWindowInput();
    }

    private void Update()
    {
        playerMovement.ProcessMoveInput(inputActions.OnFoot.Movement.ReadValue<Vector2>());
        playerLook.ProcessLookInput(inputActions.OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.OnFoot.Interact.performed -= ctx => playerInteract.ProcessInteractInput();
        inputActions.OnFoot.ExitUI.performed -= ctx => playerUIHandler.CloseInspect();

        inputActions.OnFoot.OpenWindow.performed -= ctx => WindowHandler.Instance.ProcessOpenWindowInput();

        inputActions.OnFoot.Disable();
    }
}
