using UnityEngine;

public class PickUpItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemScriptable itemScriptable;

    [SerializeField] private HoverIcon _hoverIcon;
    public HoverIcon hoverIcon => _hoverIcon;

    public void SetHover(bool toggle)
    {
        
    }

    public void Interact()
    {
        PlayerInventory.Instance.AddItem(itemScriptable);

        gameObject.SetActive(false);
    }
}
