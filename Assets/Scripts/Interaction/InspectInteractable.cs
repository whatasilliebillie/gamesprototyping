using UnityEngine;

public class InspectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Inspectable inspectPrefab;

    [SerializeField] private MemoryObject revealedMemory;
    [SerializeField] private string revealedFeedbackText;
    [SerializeField] private SoundSO interactSound;
    private bool hasRevealed;
    private bool _feedbackPending;

    public Inspectable InspectPrefab => inspectPrefab;

    public HoverIcon CurHoverIcon => HoverIcon.Eye;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public void Interact()
    {
        if (interactSound != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(interactSound, transform.position);
        }

        PlayerUIHandler.Instance.StartInspect(this);

        // Allows inspecting this object to reveal a memory
        if(!hasRevealed)
        {
            if (revealedMemory != null)
            {
                revealedMemory.Reveal();

                //Queues the Feedback text to be revealed when OnInspectClosed() is called
                _feedbackPending = true;

                hasRevealed = true;
            }
        }
    }

    // Called by PlayerUIHandler when inspecting this object ends
    public void OnInspectClosed()
    {
        if (!_feedbackPending) return;

        _feedbackPending = false;

        if (!string.IsNullOrEmpty(revealedFeedbackText) && InteractFeedbackUI.Instance != null)
        {
            InteractFeedbackUI.Instance.SetFeedback(revealedFeedbackText);
        }
    }
}
