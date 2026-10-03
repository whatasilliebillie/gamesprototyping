using UnityEngine;

public class PickUpItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemScriptable itemScriptable;

    [SerializeField] private HoverIcon _hoverIcon;
    public HoverIcon CurHoverIcon => _hoverIcon;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public void Interact()
    {
        PlayerInventory.Instance.AddItem(itemScriptable);

        gameObject.SetActive(false);
    }
}
