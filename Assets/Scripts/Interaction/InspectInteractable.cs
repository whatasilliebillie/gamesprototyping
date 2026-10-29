using UnityEngine;

public class InspectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Inspectable inspectPrefab;

    [SerializeField] private MemoryObject revealedMemory;
    [SerializeField] private string revealedFeedbackText;
    [SerializeField] private SoundSO interactSound;
    private bool hasRevealed;

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

        if(!hasRevealed)
        {
            if (revealedMemory != null)
            {
                revealedMemory.Reveal();

                if (!string.IsNullOrEmpty(revealedFeedbackText) && InteractFeedbackUI.Instance != null)
                {
                    InteractFeedbackUI.Instance.SetFeedback(revealedFeedbackText);
                }

                hasRevealed = true;
            }
        }
    }
}
