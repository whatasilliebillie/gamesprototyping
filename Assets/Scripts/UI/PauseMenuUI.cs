using UnityEngine;
using UnityEngine.InputSystem;

public partial class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerUIHandler playerUIHandler;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject audioSettingsPanel;
    [SerializeField] private GameObject gameSettingsPanel;

    public void TogglePauseMenu(bool toggle)
    {
        pauseMenuPanel.SetActive(toggle);

        if(!toggle)
        {
            audioSettingsPanel.SetActive(false);
        }

        if (!toggle)
        {
            gameSettingsPanel.SetActive(false);
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



}
