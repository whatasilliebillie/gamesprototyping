using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISoundSlider : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SoundSO hoverSound;
    [SerializeField] private SoundSO dragLoopSound;

    private AudioSource loopSource;

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
    }

    private void Update()
    {
       
        if (loopSource.isPlaying && Mouse.current != null && !Mouse.current.leftButton.isPressed)
        {
            loopSource.Stop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SoundManager.Instance == null) return;
        SoundManager.Instance.PlayUISound(hoverSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (loopSource.clip == null) return;
        if (!loopSource.isPlaying) loopSource.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        loopSource.Stop();
    }
}

