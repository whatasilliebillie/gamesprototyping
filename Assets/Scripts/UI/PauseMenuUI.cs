using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public partial class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerUIHandler playerUIHandler;

    // Build index of the main menu scene 
    [SerializeField] private int mainMenuSceneIndex = 0;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject audioSettingsPanel;
    [SerializeField] private GameObject gameSettingsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void TogglePauseMenu(bool toggle)
    {
        pauseMenuPanel.SetActive(toggle);

        if (!toggle)
        {
            audioSettingsPanel.SetActive(false);
            gameSettingsPanel.SetActive(false);

            if (howToPlayPanel != null) howToPlayPanel.SetActive(false);
        }
    }

    public void ExitPauseMenu()
    {
        playerUIHandler.TogglePause(false);
    }

    public void OpenAudioSettings()
    {
        audioSettingsPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    public void ExitAudioSettings()
    {
        audioSettingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OpenGameSettings()
    {
        gameSettingsPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    public void ExitGameSettings()
    {
        gameSettingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        if (howToPlayPanel == null) return;

        howToPlayPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    public void ExitHowToPlay()
    {
        if (howToPlayPanel != null) howToPlayPanel.SetActive(false);

        pauseMenuPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        if (mainMenuSceneIndex < 0 || mainMenuSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("[PauseMenuUI] Main menu scene index " + mainMenuSceneIndex + " is not in Build Settings");
            return;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}