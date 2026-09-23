using UnityEngine;
using UnityEngine.Events;

public class ItemUseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemScriptable usableItem;

    [SerializeField] private GameObject useVisualObject;

    [SerializeField] private bool takeItem;
    private bool hasInteracted;

    [Header("Feedback")]
    [SerializeField] private HoverIcon defaultHoverIcon;
    [SerializeField] private string emptyInteractFeedbackText;

    public UnityEvent ItemUseEvent;

    public HoverIcon hoverIcon => GetHoverIcon();

    private HoverIcon GetHoverIcon()
    {
        if(!takeItem && hasInteracted)
        {
            return HoverIcon.Default;
        }

        if(PlayerInventory.Instance.HasItem(usableItem))
        {
            return HoverIcon.Hand;
        }

        return defaultHoverIcon;
    }

    public void SetHover(bool toggle)
    {

    }

    public void Interact()
    {
        if(PlayerInventory.Instance.HasItem(usableItem))
        {
            if(takeItem)
            {
                PlayerInventory.Instance.RemoveItem(usableItem);
            }

            if(useVisualObject != null)
            {
                useVisualObject.SetActive(true);
            }

            ItemUseEvent?.Invoke();
            hasInteracted = true;
        }
        else
        {
            if(emptyInteractFeedbackText != "")
            {
                InteractFeedbackUI.Instance.SetFeedback(emptyInteractFeedbackText);
            }
        }
    }
}
