using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WindowHandler : MonoBehaviour
{
    public static WindowHandler Instance;

    [SerializeField] private Transform playerCameraTrans;
    [SerializeField] private Camera windowCamera;

    [SerializeField] private Transform inWindowTrans;
    [SerializeField] private Transform outWindowTrans;

    [SerializeField] private Material windowMaterial;

    //Sets the time the window is active
    [SerializeField] private float windowLifetime = 5f;

    private bool _spawnEnabled = true;

    private Renderer _inRenderer;
    private Collider _inCollider;
    private Coroutine _hideRoutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple WindowHandler's are present in the scene!");
            return;
        }

        Instance = this;

        _inRenderer = inWindowTrans.GetComponent<Renderer>();
        _inCollider = inWindowTrans.GetComponent<Collider>();
    }

    private void Start()
    {
        if (windowCamera.targetTexture != null)
        {
            windowCamera.targetTexture.Release();
        }

        windowCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        windowMaterial.mainTexture = windowCamera.targetTexture;

        SetPosition(inWindowTrans.position, inWindowTrans.forward);
    }

    public void ToggleSpawnEnabled(bool isSpawnEnabled)
    {
        _spawnEnabled = isSpawnEnabled;
    }

    public void ProcessOpenWindowInput()
    {
        if (!_spawnEnabled) return;

        Vector3 newPosition = playerCameraTrans.position;
        newPosition += playerCameraTrans.forward.normalized * 0.05f;

        SetPosition(newPosition, playerCameraTrans.forward);

        SetWindowActive(true);

        if (_hideRoutine != null) StopCoroutine(_hideRoutine);
        _hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(windowLifetime);

        SetWindowActive(false);
        _hideRoutine = null;
    }

    private void SetWindowActive(bool isActive)
    {
        if (_inRenderer != null) _inRenderer.enabled = isActive;
        if (_inCollider != null) _inCollider.enabled = isActive;
    }

    private void SetPosition(Vector3 newPosition, Vector3 forward)
    {
        Vector3 offset = WindowOffset();

        inWindowTrans.position = newPosition;
        outWindowTrans.position = newPosition + offset;

        inWindowTrans.forward = forward;
        outWindowTrans.forward = forward;
    }

    public Vector3 WindowOffset()
    {
        return outWindowTrans.position - inWindowTrans.position;
    }
}