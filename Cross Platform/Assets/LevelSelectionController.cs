using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button nextButton;
    public Button prevButton;

    private void Start()
    {
        // Attach button click events
        nextButton.onClick.AddListener(ScrollToNextPage);
        prevButton.onClick.AddListener(ScrollToPreviousPage);
    }

    public void ScrollToNextPage()
    {
        // Scroll to the next page
        float nextPage = scrollRect.horizontalNormalizedPosition + 1.0f;
        nextPage = Mathf.Clamp01(nextPage);
        scrollRect.horizontalNormalizedPosition = nextPage;
    }

    public void ScrollToPreviousPage()
    {
        // Scroll to the previous page
        float prevPage = scrollRect.horizontalNormalizedPosition - 1.0f;
        prevPage = Mathf.Clamp01(prevPage);
        scrollRect.horizontalNormalizedPosition = prevPage;
    }
}
