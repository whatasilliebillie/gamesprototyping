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

    [Header("Duaration")]
    [SerializeField] private float windowLifetime = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource windowAudioSource;

    [Header("VFX")]
    [SerializeField] private WindowLightning windowLightning;
    [SerializeField] private float lightningUnlockDistance = 3f;
    [SerializeField] private bool unlockByDistance = true;

    private bool _spawnEnabled = true;

    private Renderer _inRenderer;
    private Collider _inCollider;

    private bool _windowVisible;
    private bool _timerPaused;
    private float _timeRemaining;

    private bool _lightningUnlocked;

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

        if (windowAudioSource == null)
        {
            windowAudioSource = GetComponent<AudioSource>();
        }
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
        CheckLightningUnlock();

        if (!_windowVisible || _timerPaused) return;

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            SetWindowActive(false);
        }
    }

    // Turns the VFX off until the player reaches a certain distance from it, so that it doesn't appear on startup
    private void CheckLightningUnlock()
    {
        if (!unlockByDistance || _lightningUnlocked || _timerPaused) return;

        float distance = Vector3.Distance(playerCameraTrans.position, inWindowTrans.position);

        if (distance <= lightningUnlockDistance)
        {
            UnlockLightning();
        }
    }

    // Public so a trigger zone or other script can also call it
    public void UnlockLightning()
    {
        if (_lightningUnlocked) return;

        _lightningUnlocked = true;

        if (windowLightning != null) windowLightning.Play();
    }

    public void ToggleSpawnEnabled(bool isSpawnEnabled)
    {
        _spawnEnabled = isSpawnEnabled;
    }

    // Pauses the fade timer, audio and VFX (used while paused, focused or inspecting)
    public void SetTimerPaused(bool paused)
    {
        _timerPaused = paused;

        if (paused)
        {
            if (windowAudioSource != null) windowAudioSource.Pause();
            if (windowLightning != null) windowLightning.Pause();
        }
        else
        {
            if (windowLightning != null) windowLightning.Resume();

            if (_windowVisible && windowAudioSource != null) windowAudioSource.UnPause();
        }
    }

    public void ProcessOpenWindowInput()
    {
        if (!_spawnEnabled) return;

        Vector3 newPosition = playerCameraTrans.position;
        newPosition += playerCameraTrans.forward.normalized * 0.05f;

        SetPosition(newPosition, playerCameraTrans.forward);

        //This makes the lightning appear even if the player summons it before passing through it
        _lightningUnlocked = true;

        SetWindowActive(true);

        PlayWindowAudio();
    }

    private void PlayWindowAudio()
    {
        if (windowAudioSource == null) return;

        windowAudioSource.Stop();
        windowAudioSource.Play();
    }

    private void SetWindowActive(bool isActive)
    {
        if (_inRenderer != null) _inRenderer.enabled = isActive;
        if (_inCollider != null) _inCollider.enabled = isActive;

        _windowVisible = isActive;

        if (isActive)
        {
            _timeRemaining = windowLifetime;

            if (windowLightning != null) windowLightning.Play();
        }
        else
        {
            if (windowAudioSource != null) windowAudioSource.Stop();
            if (windowLightning != null) windowLightning.Stop();
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