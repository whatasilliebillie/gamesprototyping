using System;

[Serializable]
public class GameSettingsData
{
    public const float MinMouseSensitivity = 10f;
    public const float MaxMouseSensitivity = 300f;

    public float mouseSensitivity = 100f;

    // Dropdown order: 0 = Windowed, 1 = Fullscreen, 2 = Borderless
    public int windowMode = 2;
}