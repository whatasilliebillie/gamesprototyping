using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;

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

        SaveSettings();
        OnSettingsChanged?.Invoke();
    }

    public void SetWindowMode(int mode)
    {
        Settings.windowMode = Mathf.Clamp(mode, 0, 2);
        ApplyDisplay();

        SaveSettings();
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
        string json = JsonUtility.ToJson(Settings);
        PlayerPrefs.SetString(SettingsKey, json);
        PlayerPrefs.Save();

        Debug.Log("[SettingsManager] Saved: " + json);
    }

    public void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(SettingsKey))
        {
            Debug.Log("[SettingsManager] No saved settings found, using defaults");
            return;
        }

        string json = PlayerPrefs.GetString(SettingsKey);
        var loaded = JsonUtility.FromJson<GameSettingsData>(json);
        if (loaded != null) Settings = loaded;

        Debug.Log("[SettingsManager] Loaded: " + json);
    }
}