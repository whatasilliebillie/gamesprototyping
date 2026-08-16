using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler Instance;

    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private Transform inspectUIParent;

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
    }

    public void StartInspect(InspectInteractable inspectInteractable)
    {
        if (_isInspecting) return;

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
