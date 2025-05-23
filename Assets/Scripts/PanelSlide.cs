using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSlide : MonoBehaviour
{
   public RectTransform panel;
    public float slideDuration = 0.5f;

    private Vector2 hiddenPosition;
    private Vector2 shownPosition;

    private void Start()
    {
        shownPosition = Vector2.zero;
        hiddenPosition = new Vector2(Screen.width, 0);

        // Ocultar el panel inicialmente
        panel.anchoredPosition = hiddenPosition;
    }

    public void ShowPanel()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(panel.anchoredPosition, shownPosition));
    }

    public void HidePanel()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(panel.anchoredPosition, hiddenPosition));
    }

    IEnumerator SlidePanel(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            panel.anchoredPosition = Vector2.Lerp(from, to, elapsed / slideDuration);
            yield return null;
        }
        panel.anchoredPosition = to;
    }
}
