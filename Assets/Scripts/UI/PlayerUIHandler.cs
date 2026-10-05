using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler Instance;

    [Header("Components")]
    [SerializeField] private PlayerLook playerLook;
    private PlayerInteract playerInteract;

    [Header("Pause UI")]
    [SerializeField] private PauseMenuUI pauseMenuManager;

    [Header("Inspect UI")]
    [SerializeField] private Transform inspectUIParent;

    [Header("Other")]
    [SerializeField] private Transform crosshairTrans;

    public bool IsPaused => _isPaused;
    private bool _isPaused;

    public static event Action<bool> GamePauseEvent;

    private Inspectable _inspectingUIObject;
    private bool _isInspecting;

    private FocusInteractable _focusingObject;
    private bool _isFocused;

    private bool _crosshairFollow;

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple instances of PlayerUIHandler in scene!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        playerInteract = GetComponent<PlayerInteract>();

        ToggleCursor(false);
    }

    private void Update()
    {
        if(_crosshairFollow)
        {
            crosshairTrans.position = Mouse.current.position.ReadValue();
        }
    }

    public void ProcessEscInput()
    {
        if(_isInspecting)
        {
            CloseInspect();
        }
        else if(_isFocused)
        {
            StopFocus();
        }
        else
        {
            TogglePause(!_isPaused);
        }
    }

    public void TogglePause(bool toggle)
    {
        _isPaused = toggle;

        pauseMenuManager.TogglePauseMenu(_isPaused);

        UpdateWindowEnabled();

        ToggleCursor(_isPaused);

        GamePauseEvent?.Invoke(_isPaused);
    }

    public void StartInspect(InspectInteractable inspectInteractable)
    {
        if (_isInspecting || _isPaused) return;

        Inspectable newInspectObject = Instantiate(inspectInteractable.InspectPrefab, inspectUIParent);
        _inspectingUIObject = newInspectObject;

        ToggleCursor(true);

        _isInspecting = true;

        UpdateWindowEnabled();
    }

    public void CloseInspect()
    {
        if(_inspectingUIObject != null)
        {
            Destroy(_inspectingUIObject.gameObject);
        }

        if(!_isFocused)
        {
            ToggleCursor(false);
        }
        else
        {
            ToggleCursor(true, false);
        }

        _isInspecting = false;

        UpdateWindowEnabled();
    }

    public void StartFocus(FocusInteractable focusInteractable)
    {
        if (_focusingObject != null || _isPaused) return;

        _focusingObject = focusInteractable;

        _crosshairFollow = true;

        playerInteract.SetInteractionMode(InteractionMode.Focus);
        playerLook.SetCameraTarget(focusInteractable.CameraTarget);

        ToggleCursor(true, false);

        _isFocused = true;

        UpdateWindowEnabled();
    }

    private void UpdateWindowEnabled()
    {
        if(_isPaused || _isFocused || _isInspecting)
        {
            WindowHandler.Instance.ToggleSpawnEnabled(false);
        }
        else
        {
            WindowHandler.Instance.ToggleSpawnEnabled(true);
        }
    }

    public void StopFocus()
    {
        if(_isInspecting)
        {
            CloseInspect();
        }

        if(_focusingObject != null)
        {
            _focusingObject.StopFocus();
            _focusingObject = null;
        }

        _crosshairFollow = false;
        crosshairTrans.localPosition = Vector3.zero;

        playerInteract.SetInteractionMode(InteractionMode.Default);
        playerLook.RemoveCameraTarget();

        ToggleCursor(false);

        WindowHandler.Instance.ToggleSpawnEnabled(true);

        _isFocused = false;
    }

    private void ToggleCursor(bool cursorEnabled, bool cursorVisible)
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorEnabled ? CursorLockMode.None : CursorLockMode.Locked;

        playerLook.ToggleLook(!cursorEnabled);
    }

    private void ToggleCursor(bool cursorEnabled)
    {
        ToggleCursor(cursorEnabled, cursorEnabled);
    }
}
