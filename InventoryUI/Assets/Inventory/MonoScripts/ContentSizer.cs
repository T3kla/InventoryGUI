using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ContentSizer : MonoBehaviour
{
    public float Padding = 20;
    public RectTransform ContainerRect;
    public RectTransform ContentRect;
    public ContentSizeAggregator ContentAggregator;
    private float calculatedHeight = 0;

    public void FixedUpdate()
    {
        if (ContainerRect == null)
        {
            ContainerRect = transform as RectTransform;
        }

        if (ContentRect == null)
        {
            var contentHolder = transform.Find("Content");
            if (contentHolder != null)
            {
                ContentRect = transform.Find("Content") as RectTransform;
            }
        }

        if (ContentAggregator == null)
        {
            ContentAggregator = transform.parent.GetComponent<ContentSizeAggregator>();
        }

        if (ContentAggregator == null)
        {
            Debug.Log($"Did not find content size aggregator!! {transform.name}");
            return;
        }

        // Aggregate all childrens' hieght at assign the content holders height to it + padding?
        calculatedHeight = ContentRect.rect.height + Padding;

        if (ContainerRect.rect.height != calculatedHeight)
        {
            ContainerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, calculatedHeight);
            if (ContentAggregator != null)
            {
                ContentAggregator.ApplyUpdate();
            }
        }
    }

    public void OnDestroy()
    {
        if (ContentAggregator != null)
        {
            ContentAggregator.ApplyUpdate();
        }
    }
}
