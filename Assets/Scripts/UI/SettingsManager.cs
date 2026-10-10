using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;

    // Creates itself if no one placed it in the scene, so it works without any setup
    public static SettingsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("SettingsManager");
                _instance = go.AddComponent<SettingsManager>();
            }
            return _instance;
        }
    }

    private const string SettingsKey = "GameSettings";

    public GameSettingsData Settings { get; private set; } = new GameSettingsData();

    public event Action OnSettingsChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSettings();
        ApplyDisplay();
    }

    public void SetMouseSensitivity(float value)
    {
        Settings.mouseSensitivity = Mathf.Clamp(value,
            GameSettingsData.MinMouseSensitivity, GameSettingsData.MaxMouseSensitivity);
        OnSettingsChanged?.Invoke();
    }

    public void SetWindowMode(int mode)
    {
        Settings.windowMode = Mathf.Clamp(mode, 0, 2);
        ApplyDisplay();
        OnSettingsChanged?.Invoke();
    }

    private void ApplyDisplay()
    {
        switch (Settings.windowMode)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 1: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            default: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString(SettingsKey, JsonUtility.ToJson(Settings));
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(SettingsKey)) return;

        var loaded = JsonUtility.FromJson<GameSettingsData>(PlayerPrefs.GetString(SettingsKey));
        if (loaded != null) Settings = loaded;
    }
}