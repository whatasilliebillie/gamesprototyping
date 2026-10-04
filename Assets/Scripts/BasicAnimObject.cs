using UnityEngine;

public class BasicAnimObject : MonoBehaviour
{
    private Animator animator;

    private bool isToggled;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnim()
    {
        animator.SetTrigger("Toggle");
    }

    public void ToggleAnim(bool toggle)
    {
        if (animator == null) return;

        isToggled = toggle;

        animator.SetBool("Toggle", toggle);
    }
}