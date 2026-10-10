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

    public bool InteractionEnabled => _interactionEnabled;
    [SerializeField] private bool _interactionEnabled = true;

    public UnityEvent ItemUseEvent;

    public HoverIcon CurHoverIcon => GetHoverIcon();

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

    public void Interact()
    {
        if (!_interactionEnabled) return;

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

    public void ToggleInteraction(bool newEnabled)
    {
        _interactionEnabled = newEnabled;
    }
}
