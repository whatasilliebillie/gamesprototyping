using System.Collections;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private SoundSO sound;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool followThisObject;
    [SerializeField] private float delay;

    private AudioSource active;

    private void Start()
    {
        if (playOnStart) Play();
    }

    private void Play()
    {
        if (delay > 0f) StartCoroutine(PlayAfterDelay());
        else PlayNow();
    }

    private IEnumerator PlayAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        PlayNow();
    }
    public void PlayNow()
    {
        if (sound == null || SoundManager.Instance == null) return;
        if (sound.IsLooping && active != null) return;

        active = SoundManager.Instance.PlaySound(sound, transform.position);

        if (active != null && followThisObject)
        {
            active.transform.SetParent(transform);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        if (active != null) Destroy(active.gameObject);
        active = null;
    }
}
