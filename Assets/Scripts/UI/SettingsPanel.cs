using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle muteToggle;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(v => SoundManager.Instance.SetMasterVolume(v));
        musicSlider.onValueChanged.AddListener(v => SoundManager.Instance.SetMusicVolume(v));
        sfxSlider.onValueChanged.AddListener(v => SoundManager.Instance.SetSfxVolume(v));
        muteToggle.onValueChanged.AddListener(m => SoundManager.Instance.SetMuted(m));
    }

    // every time the panel opens, show the current saved value
    private void OnEnable()
    {
        if (SoundManager.Instance == null) return;

        var s = SoundManager.Instance.Settings;
        masterSlider.SetValueWithoutNotify(s.masterVolume);
        musicSlider.SetValueWithoutNotify(s.musicVolume);
        sfxSlider.SetValueWithoutNotify(s.sfxVolume);
        muteToggle.SetIsOnWithoutNotify(s.muted);
    }

    // save when the panel closes (also runs when the game quits)
    private void OnDisable()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SaveSettings();
    }

}