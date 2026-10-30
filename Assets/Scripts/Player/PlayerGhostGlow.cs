using UnityEngine;

public class GhostGlow : MonoBehaviour
{
    [SerializeField] private Light glowLight;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float fadeSpeed = 4f;

    private int _zonesInside;

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
        if (glowLight == null) return;

        float target = _zonesInside > 0 ? maxIntensity : 0f;

        glowLight.intensity = Mathf.MoveTowards(glowLight.intensity, target, fadeSpeed * Time.deltaTime);
        glowLight.enabled = glowLight.intensity > 0.01f;
    }

    public void EnterZone()
    {
        _zonesInside++;
    }

    public void ExitZone()
    {
        _zonesInside = Mathf.Max(0, _zonesInside - 1);
    }
}