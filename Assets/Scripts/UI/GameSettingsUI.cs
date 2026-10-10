using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TMP_Dropdown windowModeDropdown;

    private void Start()
    {
        mouseSensitivitySlider.minValue = GameSettingsData.MinMouseSensitivity;
        mouseSensitivitySlider.maxValue = GameSettingsData.MaxMouseSensitivity;

        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(new List<string> { "Windowed", "Fullscreen", "Borderless" });

        GameSettingsData data = SettingsManager.Instance.Settings;

        mouseSensitivitySlider.SetValueWithoutNotify(data.mouseSensitivity);
        windowModeDropdown.SetValueWithoutNotify(data.windowMode);
    }

    public void SetMouseSensitivity()
    {
        SettingsManager.Instance.SetMouseSensitivity(mouseSensitivitySlider.value);
        SettingsManager.Instance.SaveSettings();
    }

    public void SetWindowMode()
    {
        SettingsManager.Instance.SetWindowMode(windowModeDropdown.value);
        SettingsManager.Instance.SaveSettings();
    }
}


