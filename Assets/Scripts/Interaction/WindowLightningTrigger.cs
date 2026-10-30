using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindowLightningTrigger : MonoBehaviour
{
    [SerializeField] private bool singleUse = true;

    private void Reset()
    {
        // Makes the collider a trigger automatically when the script is first added
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react to the player
        if (other.GetComponentInParent<PlayerInteract>() == null) return;

        if (WindowHandler.Instance != null)
        {
            WindowHandler.Instance.UnlockLightning();
        }
        
        //Disables this script after it first fires
        if (singleUse)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}