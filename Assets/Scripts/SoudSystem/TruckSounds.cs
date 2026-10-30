using UnityEngine;

public class TruckSounds : MonoBehaviour
{
    [SerializeField] private SoundSO startSound;
    [SerializeField] private SoundSO engineLoop;
    [SerializeField] private SoundSO crashSound;
    [SerializeField] private float overlap = 0.05f;

    private AudioSource startSource;
    private GameObject loopObj;

    // Call when the key goes in the ignition
    public void PlayIgnition()
    {
        if (loopObj != null || SoundManager.Instance == null) return;

        double begin = AudioSettings.dspTime + 0.1;
        double startLength = 0;

        startSource = SoundManager.Instance.CreateSource(startSound, transform.position);
        if (startSource != null)
        {
            startSource.transform.SetParent(transform);
            startSource.PlayScheduled(begin);
            startLength = (double)startSound.Clip.samples / startSound.Clip.frequency
                          / Mathf.Abs(startSound.pitch);
            Destroy(startSource.gameObject, (float)(startLength + 0.6));
        }

        if (engineLoop != null && engineLoop.Clip != null)
        {
            loopObj = new GameObject("EngineLoop");
            loopObj.transform.SetParent(transform);
            loopObj.transform.localPosition = Vector3.zero;
            loopObj.AddComponent<GaplessLoop>()
                   .Begin(engineLoop, begin + startLength - overlap);
        }
    }

    // Call when the truck is put into reverse
    public void StopEngine()
    {
        if (startSource != null) Destroy(startSource.gameObject);
        if (loopObj != null) Destroy(loopObj);
        startSource = null;
        loopObj = null;
    }

    // Call when the truck crashes
    public void PlayCrash()
    {
        StopEngine();
        if (crashSound != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(crashSound, transform.position);
        }
    }
}