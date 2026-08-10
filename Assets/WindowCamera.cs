using UnityEngine;

public class WindowCamera : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTrans;

    [SerializeField] private Transform inWindowTrans;
    [SerializeField] private Transform outWindowTrans;

    private void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCameraTrans.position - inWindowTrans.position;

        transform.position = outWindowTrans.position + playerOffsetFromPortal;

        float portalRotations = Quaternion.Angle(outWindowTrans.rotation, inWindowTrans.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(portalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCameraTrans.forward;

        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
