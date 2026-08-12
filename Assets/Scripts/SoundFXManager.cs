using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXPrefab;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There are multiple instances of SoundFXManager in the scene!");
            return;
        }

        Instance = this;
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;

        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length); 
    }
}
