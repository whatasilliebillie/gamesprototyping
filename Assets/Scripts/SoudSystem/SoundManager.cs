using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer mixer;

    private const string MasterParam = "Master";
    private const string MusicParam = "Music";
    private const string SfxParam = "SoundFX";

    private const string SettingsKey = "AudioSettings";
    private const float MinDb = -80f;

 
    private const float MinRepeatInterval = 0.06f;

    private readonly Dictionary<SoundSO, float> _lastPlayedTime = new Dictionary<SoundSO, float>();

    public AudioSettingsData Settings { get; private set; } = new AudioSettingsData();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSettings();
    }

    private void Start()
    {
        ApplyAll();
    }

    private bool CanPlay(SoundSO soundSO)
    {
        if (soundSO.IsLooping) return true;

        float now = Time.unscaledTime;

        if (_lastPlayedTime.TryGetValue(soundSO, out float last) && now - last < MinRepeatInterval)
        {
            return false;
        }

        _lastPlayedTime[soundSO] = now;
        return true;
    }

    public void PlaySound(SoundSO soundSO, Vector3 position)
    {
        if (soundSO == null || soundSO.Clip == null)
        {
            return;
        }

        if (!CanPlay(soundSO)) return;

        var go = new GameObject(soundSO.name);
        go.transform.position = position;
        var source = go.AddComponent<AudioSource>();

        var clip = soundSO.Clip;
        source.clip = clip;
        source.outputAudioMixerGroup = soundSO.MixerGroup;
        source.volume = soundSO.Volume;
        source.pitch = soundSO.pitch;
        source.spatialBlend = soundSO.SpatialBlend;
        source.maxDistance = soundSO.MaxDistance;
        source.loop = soundSO.IsLooping;

        source.Play();

        if (!soundSO.IsLooping)
        {
            Destroy(go, clip.length / Mathf.Abs(source.pitch));
        }
    }

    public void PlayRandomSound(SoundMultipleSO soundSO, Vector3 position)
    {
        if (soundSO == null || soundSO.RandomClips == null || soundSO.RandomClips.Length == 0)
        {
            return;
        }

        if (!CanPlay(soundSO)) return;

        var go = new GameObject(soundSO.name);
        go.transform.position = position;
        var source = go.AddComponent<AudioSource>();

        var clip = soundSO.RandomClip();
        source.clip = clip;
        source.outputAudioMixerGroup = soundSO.MixerGroup;
        source.volume = soundSO.Volume;
        source.pitch = soundSO.pitch;
        source.spatialBlend = soundSO.SpatialBlend;
        source.maxDistance = soundSO.MaxDistance;
        source.loop = soundSO.IsLooping;

        source.Play();

        if (!soundSO.IsLooping)
        {
            Destroy(go, clip.length / Mathf.Abs(source.pitch));
        }
    }


    public void PlayUISound(SoundSO soundSO)
    {
        if (soundSO == null || soundSO.Clip == null)
        {
            return;
        }

        if (!CanPlay(soundSO)) return;

        var go = new GameObject(soundSO.name);
        go.transform.SetParent(transform);
        var source = go.AddComponent<AudioSource>();

        source.clip = soundSO.Clip;
        source.outputAudioMixerGroup = soundSO.MixerGroup;
        source.volume = soundSO.Volume;
        source.pitch = soundSO.pitch;
        source.spatialBlend = 0f;
        source.loop = false;
        source.ignoreListenerPause = true;

        source.Play();

        StartCoroutine(DestroyAfterRealtime(go, soundSO.Clip.length / Mathf.Abs(source.pitch)));
    }

    private IEnumerator DestroyAfterRealtime(GameObject go, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        if (go != null)
        {
            Destroy(go);
        }
    }

    public void Stop(AudioSource source)
    {
        if (source != null)
        {
            Destroy(source.gameObject);
        }
    }

    public void SetMasterVolume(float value)
    {
        Settings.masterVolume = value;
        ApplyAll();
        SaveSettings();
    }

    public void SetMusicVolume(float value)
    {
        Settings.musicVolume = value;
        ApplyAll();
        SaveSettings();
    }

    public void SetSfxVolume(float value)
    {
        Settings.sfxVolume = value;
        ApplyAll();
        SaveSettings();
    }

    public void SetMuted(bool muted)
    {
        Settings.muted = muted;
        ApplyAll();
        SaveSettings();
    }

    private void ApplyAll()
    {
        float master = Settings.muted ? 0f : Settings.masterVolume;
        SetMixerVolume(MasterParam, master);
        SetMixerVolume(MusicParam, Settings.musicVolume);
        SetMixerVolume(SfxParam, Settings.sfxVolume);
    }

    private void SetMixerVolume(string param, float linear)
    {
        float db = linear > 0.0001f ? Mathf.Log10(linear) * 20f : MinDb;
        mixer.SetFloat(param, db);
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(Settings);
        PlayerPrefs.SetString(SettingsKey, json);
        PlayerPrefs.Save();

        Debug.Log("[SoundManager] Saved: " + json);
    }

    public void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(SettingsKey))
        {
            Debug.Log("[SoundManager] No saved settings found, using defaults");
            return;
        }

        string json = PlayerPrefs.GetString(SettingsKey);
        var loaded = JsonUtility.FromJson<AudioSettingsData>(json);
        if (loaded != null) Settings = loaded;

        Debug.Log("[SoundManager] Loaded: " + json);
    }
}