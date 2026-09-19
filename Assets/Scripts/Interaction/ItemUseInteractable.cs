using UnityEngine;
using UnityEngine.Events;

public class ItemUseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemScriptable usableItem;

    [SerializeField] private GameObject useVisualObject;

    public UnityEvent ItemUseEvent;

    public HoverIcon hoverIcon => GetHoverIcon();

    private HoverIcon GetHoverIcon()
    {
        if(PlayerInventory.Instance.HasItem(usableItem))
        {
            return HoverIcon.Hand;
        }

        return HoverIcon.Default;
    }

    public void SetHover(bool toggle)
    {

    }

    public void Interact()
    {
        if(PlayerInventory.Instance.HasItem(usableItem))
        {
            PlayerInventory.Instance.RemoveItem(usableItem);

            useVisualObject.SetActive(true);

            ItemUseEvent?.Invoke();
        }
    }
}
