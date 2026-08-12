using UnityEngine;

public class BasicAnimObject : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioClip toggledAudioClip;

    private bool isToggled;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleAnim(bool toggle)
    {
        isToggled = toggle;

        animator.SetBool("Toggle", toggle);

        if(toggledAudioClip != null)
        {
            SoundFXManager.Instance.PlaySoundFXClip(toggledAudioClip, transform, 1f);
        }
    }
}
