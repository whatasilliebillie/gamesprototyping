using UnityEngine;

public class Inspectable : MonoBehaviour
{
    public void Close()
    {
        PlayerUIHandler.Instance.CloseInspect();
    }
}
