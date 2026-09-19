using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractFeedbackUI : MonoBehaviour
{
    public static InteractFeedbackUI Instance;

    [Header("Feedback Text")]
    [SerializeField] private Animator feedbackTextAnimator;
    [SerializeField] private TMP_Text feedbackText;

    [Header("Hover Icons")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite[] hoverIconSprites;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of InteractFeedbackUI in scene!");
            return;
        }

        Instance = this;
    }

    public void SetFeedback(string newFeedback)
    {
        feedbackText.text = newFeedback;

        feedbackTextAnimator.Play("FeedbackDisplay", -1, 0f);
    }

    public void SetIcon(HoverIcon icon)
    {
        iconImage.sprite = hoverIconSprites[(int)icon];
    }
}
