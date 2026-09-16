using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InterWindowInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator pastInteractAnimator;

    [SerializeField] private float waitForResult;

    private MeshRenderer meshRenderer;

    [SerializeField] private Material standardMaterial;
    [SerializeField] private Material highlightMaterial;

    [SerializeField] private bool hideWhenNotHovered;

    public UnityEvent InteractEvent;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetHover(bool toggle)
    {
        if(hideWhenNotHovered)
        {
            meshRenderer.enabled = toggle;
        }

        meshRenderer.material = toggle ? highlightMaterial : standardMaterial;
    }

    public void Interact()
    {
        pastInteractAnimator.SetTrigger("Interact");

        StartCoroutine(AnimationWait());
    }

    private IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(waitForResult);

        InteractEvent?.Invoke();

        yield break;
    }
}
