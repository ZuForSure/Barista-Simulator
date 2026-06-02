using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int pageCount;
    [SerializeField] private float snapDuration = 0.2f;
    [SerializeField] private float swipeThreshold = 500f;

    private List<float> pagePositions = new List<float>();
    private bool isDragging = false;
    private int currentPage = 0;

    private void Start()
    {
        float step = 1f / (pageCount - 1);

        for (int i = 0; i < pageCount; i++)
        {
            pagePositions.Add(step * i);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        float velocity = scrollRect.velocity.x;

        if (Mathf.Abs(velocity) > swipeThreshold)
        {
            if (velocity < 0)
                currentPage++;
            else
                currentPage--;
        }
        else
        {
            currentPage = GetClosestPage();
        }

        currentPage = Mathf.Clamp(currentPage, 0, pageCount - 1);

        StartCoroutine(SmoothSnap(pagePositions[currentPage]));
    }

    private int GetClosestPage()
    {
        float current = scrollRect.horizontalNormalizedPosition;

        int closestIndex = 0;
        float minDistance = Mathf.Abs(current - pagePositions[0]);

        for (int i = 1; i < pagePositions.Count; i++)
        {
            float distance = Mathf.Abs(current - pagePositions[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private IEnumerator SmoothSnap(float target)
    {
        float time = 0f;
        float start = scrollRect.horizontalNormalizedPosition;

        while (time < snapDuration)
        {
            time += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition =
                Mathf.Lerp(start, target, time / snapDuration);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }
}
