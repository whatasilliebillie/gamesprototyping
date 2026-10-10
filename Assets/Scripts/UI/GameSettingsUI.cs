using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TMP_Dropdown windowModeDropdown;

    private bool _isSettingUp;

    private void Start()
    {
        GameSettingsData data = SettingsManager.Instance.Settings;
        float savedSensitivity = data.mouseSensitivity;
        int savedWindowMode = data.windowMode;

        _isSettingUp = true;

        mouseSensitivitySlider.minValue = GameSettingsData.MinMouseSensitivity;
        mouseSensitivitySlider.maxValue = GameSettingsData.MaxMouseSensitivity;

        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(new List<string> { "Windowed", "Fullscreen", "Borderless" });

        mouseSensitivitySlider.SetValueWithoutNotify(savedSensitivity);
        windowModeDropdown.SetValueWithoutNotify(savedWindowMode);

        _isSettingUp = false;
    }

    public void SetMouseSensitivity()
    {
        if (_isSettingUp) return;

        SettingsManager.Instance.SetMouseSensitivity(mouseSensitivitySlider.value);
    }

    public void SetWindowMode()
    {
        if (_isSettingUp) return;

        SettingsManager.Instance.SetWindowMode(windowModeDropdown.value);
    }
}