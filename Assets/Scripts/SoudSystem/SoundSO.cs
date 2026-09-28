using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SD_NewSound", menuName = "Scriptable Objects/Audio/Sound Data", order = 1)]
public class SoundSO : ScriptableObject
{
    public AudioClip Clip;
    public AudioMixerGroup MixerGroup;
    [Range(0f, 1f)] public float Volume = 1f;
    [Range(-3, 3f)] public float pitch = 1f;
    [Range(0f, 1f)] public float SpatialBlend = 1f;
    public float MaxDistance = 25f;
    public bool IsLooping;

    
}
