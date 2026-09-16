using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    private MeshRenderer meshRenderer;

    [SerializeField] private Material standardMaterial;
    [SerializeField] private Material highlightMaterial;

    public UnityEvent ButtonPressEvent;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetHover(bool toggle)
    {
        meshRenderer.material = toggle ? highlightMaterial : standardMaterial;
    }

    public void Interact()
    {
        ButtonPressEvent?.Invoke();

        animator.SetTrigger("Press");
    }
}
