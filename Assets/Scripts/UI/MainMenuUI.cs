using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Build index of the game scene 
    [SerializeField] private int gameSceneIndex = 1;

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject audioSettingsPanel;
    [SerializeField] private GameObject gameSettingsPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject selectionMenuPanel;


    private void Start()
    {
        // Loads the saved settings 
        SettingsManager settings = SettingsManager.Instance;

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ShowPanel(mainPanel);
    }

    public void PlayGame()
    {
        if (gameSceneIndex < 0 || gameSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("[MainMenuUI] Game scene index " + gameSceneIndex + " is not in Build Settings");
            return;
        }

        SceneManager.LoadScene(gameSceneIndex);
        ShowPanel(mainPanel != null ? mainPanel : selectionMenuPanel);
    }

    public void OpenGameSettings()
    {
        ShowPanel(gameSettingsPanel);
    }

    public void OpenAudioSettings()
    {
        ShowPanel(audioSettingsPanel);
    }

    public void OpenHowToPlay()
    {
        ShowPanel(howToPlayPanel);
    }

    public void OpenCredits()
    {
        ShowPanel(creditsPanel);
    }

    public void BackToMain()
    {
        ShowPanel(mainPanel);
    }

    public void BackToSelectionMenu()
    {
        ShowPanel(selectionMenuPanel);
    }

    public void OpenSelectionMenu()
    {
        ShowPanel(selectionMenuPanel);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Only the given panel is active
    private void ShowPanel(GameObject panel)
    {
        SetActiveIfAssigned(mainPanel, panel == mainPanel);
        SetActiveIfAssigned(selectionMenuPanel, panel == selectionMenuPanel);
        SetActiveIfAssigned(audioSettingsPanel, panel == audioSettingsPanel);
        SetActiveIfAssigned(gameSettingsPanel, panel == gameSettingsPanel);
        SetActiveIfAssigned(howToPlayPanel, panel == howToPlayPanel);
        SetActiveIfAssigned(creditsPanel, panel == creditsPanel);
    }

    private void SetActiveIfAssigned(GameObject target, bool active)
    {
        if (target != null) target.SetActive(active);
    }
}