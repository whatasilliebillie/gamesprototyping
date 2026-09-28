using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{

    private bool isPaused;

    [SerializeField] private GameObject  settingsPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitSettingButton;

    private WorkerInput inputActions;


    private void Awake()
    {
        inputActions = new WorkerInput();
    }


    void Start()
    {
        resumeGameButton.onClick.AddListener(() => PauseGame(false));
        exitSettingButton.onClick.AddListener(() => ExitSettingsPanel());
        settingsButton.onClick.AddListener(() => ShowSettingsPanel());     
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        
    }


    private void OnExitUI(InputAction.CallbackContext context)
    {
        if (settingsPanel.activeSelf)
        {
            ExitSettingsPanel();
        }
        else
        {
            TogglePause();       
        }
    }

    public void PauseGame(bool value)
    {
        isPaused = value;
        Time.timeScale = value ? 0f : 1f;

        pauseMenuPanel.SetActive(value);
        if (!value) settingsPanel.SetActive(false);

        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    public void ExitSettingsPanel()
    {
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    private void OnEnable()
    {
        inputActions.OnFoot.SettingUI.performed += OnExitUI;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.OnFoot.SettingUI.performed -= OnExitUI;
        inputActions.Disable();
    }


    public void TogglePause() => PauseGame(!isPaused);
}
