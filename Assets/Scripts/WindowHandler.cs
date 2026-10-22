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

    [SerializeField] private float windowLifetime = 5f;

    private bool _spawnEnabled = true;

    private Renderer _inRenderer;
    private Collider _inCollider;

    private bool _windowVisible;
    private float _timeRemaining;
    private int _holdCount;

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

    private void Update()
    {
        if (!_windowVisible || _holdCount > 0) return;

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            SetWindowActive(false);
        }
    }

    public void ToggleSpawnEnabled(bool isSpawnEnabled)
    {
        _spawnEnabled = isSpawnEnabled;
    }

    // Call when something (focus, inspect) should stop the window fading
    public void HoldWindow()
    {
        _holdCount++;
    }

    // Call when that thing ends
    public void ReleaseWindow()
    {
        _holdCount = Mathf.Max(0, _holdCount - 1);
    }

    public void ProcessOpenWindowInput()
    {
        if (!_spawnEnabled) return;

        Vector3 newPosition = playerCameraTrans.position;
        newPosition += playerCameraTrans.forward.normalized * 0.05f;

        SetPosition(newPosition, playerCameraTrans.forward);

        SetWindowActive(true);
    }

    private void SetWindowActive(bool isActive)
    {
        if (_inRenderer != null) _inRenderer.enabled = isActive;
        if (_inCollider != null) _inCollider.enabled = isActive;

        _windowVisible = isActive;

        if (isActive)
        {
            _timeRemaining = windowLifetime;
        }
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