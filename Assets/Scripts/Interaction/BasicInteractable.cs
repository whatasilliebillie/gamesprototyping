using UnityEngine;
using UnityEngine.Events;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string feedbackText;

    [SerializeField] private bool singleUseInteract;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public UnityEvent InteractEvent;

    [SerializeField] private HoverIcon _hoverIcon;

    [SerializeField] private SoundSO interactSound;
    public HoverIcon CurHoverIcon => GetHoverIcon();

    private HoverIcon GetHoverIcon()
    {
        if (!_interactionEnabled)
        {
            return HoverIcon.Default;
        }

        return _hoverIcon;
    }

    public void Interact()
    {
        if (!_interactionEnabled) return;

        InteractEvent?.Invoke();

        if(feedbackText != "")
        {
            InteractFeedbackUI.Instance.SetFeedback(feedbackText);
        }

        if(singleUseInteract)
        {
            _interactionEnabled = false;
        }
    }

    public void ToggleInteract(bool toggle)
    {
        _interactionEnabled = toggle;
    }
}
