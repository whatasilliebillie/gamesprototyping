using UnityEngine;

[CreateAssetMenu(fileName = "SD_NewSound", menuName = "Audio/Sound Multiple Data", order = 2)]
public class SoundMultipleSO : SoundSO
{
    public AudioClip[] RandomClips;
    public AudioClip RandomClip() => RandomClips[Random.Range(0, RandomClips.Length)];
}