using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler Instance;

    [Header("Components")]
    [SerializeField] private PlayerLook playerLook;

    [Header("Pause UI")]
    [SerializeField] private PauseMenuUI pauseMenuManager;

    [Header("Inspect UI")]
    [SerializeField] private Transform inspectUIParent;

    public bool IsPaused => _isPaused;
    private bool _isPaused;

    private Inspectable _inspectingUIObject;
    private bool _isInspecting;

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple instances of PlayerUIHandler in scene!");
            return;
        }

        Instance = this;
    }

    public void ProcessEscInput()
    {
        if(_isInspecting)
        {
            CloseInspect();
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

        ToggleCursor(_isPaused);
    }

    public void StartInspect(InspectInteractable inspectInteractable)
    {
        if (_isInspecting || _isPaused) return;

        Inspectable newInspectObject = Instantiate(inspectInteractable.InspectPrefab, inspectUIParent);
        _inspectingUIObject = newInspectObject;

        ToggleCursor(true);

        _isInspecting = true;
    }

    public void CloseInspect()
    {
        if(_inspectingUIObject != null)
        {
            Destroy(_inspectingUIObject.gameObject);
        }

        ToggleCursor(false);

        _isInspecting = false;
    }

    private void ToggleCursor(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;

        playerLook.ToggleLook(!visible);
    }
}
