using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundSO hoverSound;
    [SerializeField] private SoundSO dragLoopSound;

    private AudioSource loopSource;

    private Slider slider;

    private float lastTickValue;

    [SerializeField] private int tickSteps = 20;

    private void Awake()
    {
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.playOnAwake = false;
        loopSource.loop = true;
        loopSource.spatialBlend = 0f;
        loopSource.ignoreListenerPause = true;

        if (dragLoopSound != null)
        {
            loopSource.clip = dragLoopSound.Clip;
            loopSource.outputAudioMixerGroup = dragLoopSound.MixerGroup;
            loopSource.volume = dragLoopSound.Volume;
            loopSource.pitch = dragLoopSound.pitch;
        }

        slider = GetComponent<Slider>();
        lastTickValue = Normalized(slider.value);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (SoundManager.Instance == null) return;

        float normalized = Normalized(value);
        float stepSize = 1f / Mathf.Max(1, tickSteps);

        if (Mathf.Abs(normalized - lastTickValue) >= stepSize)
        {
            lastTickValue = normalized;
            SoundManager.Instance.PlayUISound(dragLoopSound);
        }
    }

    private float Normalized(float value)
    {
        return Mathf.InverseLerp(slider.minValue, slider.maxValue, value);
    }

}

