using UnityEngine;
using UnityEngine.Events;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string feedbackText;

    [SerializeField] private bool singleUseInteract;
    private bool interactEnabled = true;

    public UnityEvent InteractEvent;

    [SerializeField] private HoverIcon _hoverIcon;
    public HoverIcon hoverIcon => GetHoverIcon();

    private HoverIcon GetHoverIcon()
    {
        if (!interactEnabled)
        {
            return HoverIcon.Default;
        }

        return _hoverIcon;
    }

    public void SetHover(bool toggle)
    {
        
    }

    public void Interact()
    {
        if (!interactEnabled) return;

        InteractEvent?.Invoke();

        if(feedbackText != "")
        {
            InteractFeedbackUI.Instance.SetFeedback(feedbackText);
        }

        if(singleUseInteract)
        {
            interactEnabled = false;
        }
    }

    public void ToggleInteract(bool toggle)
    {
        interactEnabled = toggle;
    }
}
