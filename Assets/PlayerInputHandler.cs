using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerLook playerLook;
    private PlayerMovement playerMovement;

    private WorkerInput inputActions;

    private void Awake()
    {
        inputActions = new WorkerInput();

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        inputActions.OnFoot.Enable();
    }

    private void Update()
    {
        playerMovement.ProcessMoveInput(inputActions.OnFoot.Movement.ReadValue<Vector2>());
        playerLook.ProcessLookInput(inputActions.OnFoot.Look.ReadValue<Vector2>());
    }
}
