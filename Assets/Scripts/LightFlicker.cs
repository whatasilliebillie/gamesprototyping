using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float maxIntensity = 4f;
    [SerializeField] private float speed = 8f;

    private Light bulb;
    private void Awake() => bulb = GetComponent<Light>();
    private void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * speed, 0f);
        bulb.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
