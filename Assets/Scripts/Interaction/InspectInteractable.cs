using UnityEngine;

public class InspectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Inspectable inspectPrefab;

    [SerializeField] private MemoryObject revealedMemory;
    private bool hasRevealed;

    public Inspectable InspectPrefab => inspectPrefab;

    public HoverIcon CurHoverIcon => HoverIcon.Eye;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public void Interact()
    {
        PlayerUIHandler.Instance.StartInspect(this);

        if(!hasRevealed)
        {
            if (revealedMemory != null)
            {
                revealedMemory.Reveal();

                hasRevealed = true;
            }
        }
    }
}
