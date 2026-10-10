using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle muteAudioToggle;

    private void Start()
    {
        if (SoundManager.Instance == null) return;

        Debug.Log("banana");

        AudioSettingsData audioData = SoundManager.Instance.Settings;

        masterSlider.SetValueWithoutNotify(audioData.masterVolume);
        musicSlider.SetValueWithoutNotify(audioData.musicVolume);
        sfxSlider.SetValueWithoutNotify(audioData.sfxVolume);
        muteAudioToggle.SetIsOnWithoutNotify(audioData.muted);
    }

    public void SetMasterVolume()
    {
        SoundManager.Instance.SetMasterVolume(masterSlider.value);
    }

    public void SetMusicVolume()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        SoundManager.Instance.SetSfxVolume(sfxSlider.value);
    }

    public void SetSoundMute()
    {
        SoundManager.Instance.SetMuted(muteAudioToggle.isOn);
    }
}