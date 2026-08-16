using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [SerializeField] private float mouseSens = 100f;

    private float xRotation = 0f;

    private Vector2 lookInput;

    private bool lookEnabled = true;

    public void ProcessLookInput(Vector2 newLookInput)
    {
        lookInput = newLookInput;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleLook(bool toggle)
    {
        lookEnabled = toggle;
    }

    private void LateUpdate()
    {
        if (!lookEnabled) return;

        float mouseX = lookInput.x * mouseSens * 0.01f;
        float mouseY = lookInput.y * mouseSens * 0.01f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTrans.Rotate(Vector3.up * mouseX);
    }
}
