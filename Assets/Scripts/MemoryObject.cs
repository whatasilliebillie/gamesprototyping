using UnityEngine;

public class MemoryObject : MonoBehaviour
{
    [SerializeField] private GameObject[] blurObjects;

    public void Reveal()
    {
        foreach(GameObject blurObject in blurObjects)
        {
            blurObject.SetActive(false);
        }
    }
}
