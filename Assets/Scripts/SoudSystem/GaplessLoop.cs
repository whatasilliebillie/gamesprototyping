using UnityEngine;

public class GaplessLoop : MonoBehaviour
{
    private AudioSource[] sources = new AudioSource[2];
    private double nextTime;
    private double clipLength;
    private int next;
    private bool running;

    public void Begin(SoundSO sound, double startTime)
    {
        if (SoundManager.Instance == null) return;

        for (int i = 0; i < 2; i++)
        {
            sources[i] = SoundManager.Instance.CreateSource(sound, transform.position);
            if (sources[i] == null) return;
            sources[i].loop = false;
            sources[i].transform.SetParent(transform);
        }

        clipLength = (double)sound.Clip.samples / sound.Clip.frequency / Mathf.Abs(sound.pitch);
        nextTime = startTime;
        running = true;
        ScheduleNext();
        ScheduleNext();
    }

    private void ScheduleNext()
    {
        sources[next].PlayScheduled(nextTime);
        nextTime += clipLength;
        next = 1 - next;
    }

    private void Update()
    {
        if (!running) return;

        // A source has finished once the audio clock passes (nextTime - clipLength).
        // Only then is it safe to schedule it again.
        while (AudioSettings.dspTime >= nextTime - clipLength)
        {
            ScheduleNext();
        }
    }
}