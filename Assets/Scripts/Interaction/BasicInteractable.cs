using UnityEngine;
using UnityEngine.Events;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;

    [SerializeField] private string feedbackText;

    public UnityEvent InteractEvent;

    [SerializeField] private HoverIcon _hoverIcon;
    public HoverIcon hoverIcon => _hoverIcon;

    public void SetHover(bool toggle)
    {
        
    }

    public void Interact()
    {
        InteractEvent?.Invoke();

        if(feedbackText != "")
        {
            InteractFeedbackUI.Instance.SetFeedback(feedbackText);
        }

        if(animator != null)
        {
            animator.SetTrigger("Interact");
        }
    }
}
