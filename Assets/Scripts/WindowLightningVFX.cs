using UnityEngine;

public class WindowLightning : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform panel;
    [SerializeField] private Material boltMaterial;
    [SerializeField] private Vector2 panelSize = Vector2.one;
    [SerializeField] private float forwardOffset = -0.01f;

    [Header("Look")]
    [SerializeField] private int boltCount = 6;
    [SerializeField] private int pointsPerBolt = 12;
    [SerializeField] private float boltWidth = 0.01f;
    [SerializeField] private float jitter = 0.03f;
    [SerializeField] private Color boltColor = new Color(0.6f, 0.85f, 1f);

    [Header("Timing")]
    [SerializeField] private float refreshInterval = 0.06f;

    private LineRenderer[] _bolts;
    private float _timer;
    private bool _playing;
    private bool _paused;

    private void Awake()
    {
        if (panel == null) panel = transform.parent;

        _bolts = new LineRenderer[boltCount];

        for (int i = 0; i < boltCount; i++)
        {
            GameObject boltObject = new GameObject("Bolt " + i);
            boltObject.transform.SetParent(transform, false);

            LineRenderer line = boltObject.AddComponent<LineRenderer>();
            line.useWorldSpace = true;
            line.positionCount = pointsPerBolt;
            line.widthMultiplier = boltWidth;
            line.numCapVertices = 2;
            line.startColor = boltColor;
            line.endColor = boltColor;
            line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            line.receiveShadows = false;

            if (boltMaterial != null) line.material = boltMaterial;

            line.enabled = false;

            _bolts[i] = line;
        }
    }

    private void Update()
    {
        if (!_playing || _paused) return;

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = refreshInterval;
            RegenerateBolts();
        }
    }

    public void Play()
    {
        _playing = true;
        _paused = false;
        _timer = refreshInterval;

        SetBoltsVisible(true);
        RegenerateBolts();
    }

    // Freezes the current bolts in place when interacting with objects.
    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        if (_playing) _paused = false;
    }

    public void Stop()
    {
        _playing = false;
        _paused = false;

        SetBoltsVisible(false);
    }

    private void SetBoltsVisible(bool visible)
    {
        for (int i = 0; i < _bolts.Length; i++)
        {
            _bolts[i].enabled = visible;
        }
    }

    private void RegenerateBolts()
    {
        if (panel == null) return;

        Vector2 half = panelSize * 0.5f;

        Vector2[] corners =
        {
            new Vector2(-half.x, -half.y),
            new Vector2( half.x, -half.y),
            new Vector2( half.x,  half.y),
            new Vector2(-half.x,  half.y)
        };

        for (int i = 0; i < _bolts.Length; i++)
        {
            int edge = Random.Range(0, 4);
            Vector2 edgeStart = corners[edge];
            Vector2 edgeEnd = corners[(edge + 1) % 4];

            float startT = Random.Range(0f, 0.7f);
            float endT = Mathf.Min(startT + Random.Range(0.2f, 0.5f), 1f);

            Vector2 a = Vector2.Lerp(edgeStart, edgeEnd, startT);
            Vector2 b = Vector2.Lerp(edgeStart, edgeEnd, endT);
            Vector2 perp = Vector2.Perpendicular((b - a).normalized);

            for (int p = 0; p < pointsPerBolt; p++)
            {
                float t = p / (float)(pointsPerBolt - 1);
                Vector2 point = Vector2.Lerp(a, b, t);

                // Ends stay pinned to the edge, the middle zigzags
                float strength = Mathf.Sin(t * Mathf.PI);
                point += perp * Random.Range(-jitter, jitter) * strength;

                Vector3 local = new Vector3(point.x, point.y, forwardOffset);
                _bolts[i].SetPosition(p, panel.TransformPoint(local));
            }
        }
    }
}