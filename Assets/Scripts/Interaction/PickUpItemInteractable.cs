using UnityEngine;

public class PickUpItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemScriptable itemScriptable;

    public void SetHover(bool toggle)
    {
        
    }

    public void Interact()
    {
        PlayerInventory.Instance.AddItem(itemScriptable);

        gameObject.SetActive(false);
    }
}
