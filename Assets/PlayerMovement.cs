using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController charController;

    [SerializeField] private float moveSpeed;

    private Vector2 moveInput;

    public void ProcessMoveInput(Vector2 newMoveInput)
    {
        moveInput = newMoveInput;
    }

    private void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        charController.Move(move * moveSpeed * Time.deltaTime);
    }
}
