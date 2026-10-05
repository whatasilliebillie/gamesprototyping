using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook Instance;

    [SerializeField] private Transform playerTargetTrans;
    private Transform _targetTrans;

    private bool _followingTarget;

    [SerializeField] private float playerTargetLerpSpeed;
    [SerializeField] private float targetLerpSpeed;

    private Vector3 followOffset;

    [SerializeField] private float mouseSens = 100f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector2 lookInput;

    private bool lookEnabled = true;

    public void ProcessLookInput(Vector2 newLookInput)
    {
        lookInput = newLookInput;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of PlayerLook in scene!");
            return;
        }

        Instance = this;
    }

    private void LateUpdate()
    {
        Transform lerpingTrans = playerTargetTrans;
        float lerpSpeed = playerTargetLerpSpeed;

        if(_followingTarget)
        {
            lerpingTrans = _targetTrans;
            lerpSpeed = targetLerpSpeed;

            transform.rotation = Quaternion.Lerp(transform.rotation, lerpingTrans.rotation, lerpSpeed * Time.deltaTime);
        }

        transform.position = Vector3.Lerp(transform.position, lerpingTrans.position + followOffset, lerpSpeed * Time.deltaTime);

        if (!lookEnabled) return;

        float mouseX = lookInput.x * mouseSens * 0.01f;
        float mouseY = lookInput.y * mouseSens * 0.01f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    public void MoveCamera(Vector3 movement)
    {
        transform.position += movement;
    }

    public void SetCameraTarget(Transform newCameraTargetTrans)
    {
        _targetTrans = newCameraTargetTrans;

        _followingTarget = true;
    }

    public void RemoveCameraTarget()
    {
        _targetTrans = playerTargetTrans;

        transform.position = playerTargetTrans.position;

        _followingTarget = false;
    }

    public void ToggleLook(bool toggle)
    {
        lookEnabled = toggle;
    }
}
