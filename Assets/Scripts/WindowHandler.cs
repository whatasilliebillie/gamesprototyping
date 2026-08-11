using UnityEngine;
using UnityEngine.InputSystem;

public class WindowHandler : MonoBehaviour
{
    public static WindowHandler Instance;

    [SerializeField] private Transform playerCameraTrans;
    [SerializeField] private Camera windowCamera;

    [SerializeField] private Transform inWindowTrans;
    [SerializeField] private Transform outWindowTrans;

    [SerializeField] private Material windowMaterial;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple WindowHandler's are present in the scene!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if(windowCamera.targetTexture != null)
        {
            windowCamera.targetTexture.Release();
        }

        windowCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        windowMaterial.mainTexture = windowCamera.targetTexture;

        SetPosition(inWindowTrans.position, inWindowTrans.forward);
    }

    public void ProcessOpenWindowInput()
    {
        Vector3 newPosition = playerCameraTrans.position;
        newPosition += playerCameraTrans.forward.normalized * 0.05f;

        SetPosition(newPosition, playerCameraTrans.forward);
    }

    private void SetPosition(Vector3 newPosition, Vector3 forward)
    {
        Vector3 offset = WindowOffset();

        inWindowTrans.position = newPosition;
        outWindowTrans.position = newPosition + offset;

        inWindowTrans.forward = forward;
        outWindowTrans.forward = forward;
    }

    public Vector3 WindowOffset()
    {
        return outWindowTrans.position - inWindowTrans.position;
    }
}
