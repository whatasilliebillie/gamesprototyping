using UnityEngine;
using UnityEngine.InputSystem;

public class WindowHandler : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTrans;
    [SerializeField] private Camera windowCamera;

    [SerializeField] private Transform inWindowTrans;
    [SerializeField] private Transform outWindowTrans;

    [SerializeField] private Material windowMaterial;

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

    private void Update()
    {
        if(Keyboard.current[Key.Enter].wasReleasedThisFrame)
        {
            Vector3 newPosition = playerCameraTrans.position;
            newPosition += playerCameraTrans.forward.normalized * 0.05f;

            SetPosition(newPosition, playerCameraTrans.forward);
        }
    }

    private void SetPosition(Vector3 newPosition, Vector3 forward)
    {
        Vector3 offset = outWindowTrans.position - inWindowTrans.position;

        inWindowTrans.position = newPosition;
        outWindowTrans.position = newPosition + offset;

        inWindowTrans.forward = forward;
        outWindowTrans.forward = forward;
    }
}
