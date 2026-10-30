using UnityEngine;

public class GhostGlow : MonoBehaviour
{
    [Header("Glow")]
    [SerializeField] private Light glowLight;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float fadeSpeed = 4f;

    [Header("First Phase Text")]
    [SerializeField, TextArea(2, 4)] private string firstPhaseText;

    [Header("Audio")]
    [SerializeField] private AudioClip phaseLoopClip;
    [SerializeField] private AudioClip phaseExitClip;
    [SerializeField, Range(0f, 1f)] private float loopVolume = 0.6f;
    [SerializeField, Range(0f, 1f)] private float exitVolume = 1f;
    [SerializeField] private float loopFadeSpeed = 3f;

    private AudioSource _loopSource;
    private AudioSource _exitSource;
    private float _loopTargetVolume;

    private int _zonesInside;
    private bool _hasShownFirstText;

    private void Awake()
    {
        _loopSource = CreateSource();
        _loopSource.clip = phaseLoopClip;
        _loopSource.loop = true;

        _exitSource = CreateSource();
    }

    private AudioSource CreateSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f;

        return source;
    }

    private void Start()
    {
        if (glowLight != null)
        {
            glowLight.intensity = 0f;
            glowLight.enabled = false;
        }
    }

    private void Update()
    {
        UpdateGlow();
        UpdateLoopAudio();
    }

    private void UpdateGlow()
    {
        if (glowLight == null) return;

        float target = _zonesInside > 0 ? maxIntensity : 0f;

        glowLight.intensity = Mathf.MoveTowards(glowLight.intensity, target, fadeSpeed * Time.deltaTime);
        glowLight.enabled = glowLight.intensity > 0.01f;
    }

    private void UpdateLoopAudio()
    {
        if (!_loopSource.isPlaying) return;

        _loopSource.volume = Mathf.MoveTowards(_loopSource.volume, _loopTargetVolume, loopFadeSpeed * Time.deltaTime);

        if (_loopTargetVolume <= 0f && _loopSource.volume <= 0.001f)
        {
            _loopSource.Stop();
        }
    }

    public void EnterZone()
    {
        _zonesInside++;

        // Only the first fence entered counts as "starting" to phase
        if (_zonesInside == 1)
        {
            OnPhaseStart();
        }
    }

    public void ExitZone()
    {
        if (_zonesInside == 0) return;

        _zonesInside--;

        // Only leaving the last fence counts as "stopping"
        if (_zonesInside == 0)
        {
            OnPhaseEnd();
        }
    }

    private void OnPhaseStart()
    {
        if (!_hasShownFirstText)
        {
            _hasShownFirstText = true;

            if (!string.IsNullOrEmpty(firstPhaseText) && InteractFeedbackUI.Instance != null)
            {
                InteractFeedbackUI.Instance.SetFeedback(firstPhaseText);
            }
        }

        if (phaseLoopClip != null)
        {
            _loopTargetVolume = loopVolume;

            if (!_loopSource.isPlaying)
            {
                _loopSource.volume = 0f;
                _loopSource.Play();
            }
        }
    }

    private void OnPhaseEnd()
    {
        _loopTargetVolume = 0f;

        if (phaseExitClip != null)
        {
            _exitSource.PlayOneShot(phaseExitClip, exitVolume);
        }
    }
}