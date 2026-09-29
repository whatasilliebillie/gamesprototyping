using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerUIHandler playerUIHandler;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject audioSettingsPanel;

    public void TogglePauseMenu(bool toggle)
    {
        pauseMenuPanel.SetActive(toggle);

        if(!toggle)
        {
            audioSettingsPanel.SetActive(false);
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
}
