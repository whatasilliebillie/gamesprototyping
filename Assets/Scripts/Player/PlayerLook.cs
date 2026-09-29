using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;

    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private float targetLerpSpeed;

    private float xRotation = 0f;
    private float yRotation = 0f;

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
        transform.position = Vector3.Lerp(transform.position, targetTrans.position, targetLerpSpeed * Time.deltaTime);

        if (!lookEnabled) return;

        float mouseX = lookInput.x * mouseSens * 0.01f;
        float mouseY = lookInput.y * mouseSens * 0.01f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
