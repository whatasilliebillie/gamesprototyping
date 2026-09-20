using UnityEngine;

public class SwapAnimators : MonoBehaviour
{
    [SerializeField] private Animator originalAnimator;
    [SerializeField] private Animator newAnimator;

    public void Swap()
    {
        newAnimator.gameObject.SetActive(true);

        AnimatorStateInfo stateInfo = originalAnimator.GetCurrentAnimatorStateInfo(0);

        newAnimator.Play(stateInfo.fullPathHash, 0, stateInfo.normalizedTime);

        originalAnimator.gameObject.SetActive(false);
    }
}
