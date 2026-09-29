using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    private Rigidbody rb;

    [SerializeField] private float defaultMoveSpeed;
    [SerializeField] private float sprintMoveSpeed;

    private Vector2 _moveInput;
    private bool _sprintInput;

    public void ProcessMoveInput(Vector2 newMoveInput)
    {
        _moveInput = newMoveInput;
    }

    public void ProcessSprintInput(bool newSprintInput)
    {
        _sprintInput = newSprintInput;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 move = cameraTrans.right * _moveInput.x + cameraTrans.forward * _moveInput.y;

        if(_sprintInput)
        {
            move *= sprintMoveSpeed;
        }
        else
        {
            move *= defaultMoveSpeed;
        }

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
