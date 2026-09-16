using UnityEngine;
using TMPro;

public class InteractFeedbackUI : MonoBehaviour
{
    public static InteractFeedbackUI Instance;

    private Animator animator;

    [SerializeField] private TMP_Text feedbackText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of InteractFeedbackUI in scene!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFeedback(string newFeedback)
    {
        feedbackText.text = newFeedback;

        animator.Play("FeedbackDisplay", -1, 0f);
    }
}
