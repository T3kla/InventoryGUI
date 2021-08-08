using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ContentSizeAggregator : MonoBehaviour
{

    public float Padding = 20;
    public RectTransform ContainerRect;
    private float calculatedHeight = 0;
    public void ApplyUpdate()
    {
        if (ContainerRect == null)
        {
            ContainerRect = transform as RectTransform;
        }

        // Aggregate all childrens' hieght at assign the content holders height to it + padding?
        calculatedHeight = 0;
        foreach (RectTransform child in transform)
        {
            if (!child.gameObject.activeSelf) continue;

            if(child.anchoredPosition.y != -calculatedHeight)
            {
                child.anchoredPosition = new Vector2(0, -calculatedHeight);
            }
            calculatedHeight += child.rect.height;
        }

        if (ContainerRect.rect.height != calculatedHeight)
        {
            ContainerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, calculatedHeight + Padding);
        }
    }
}

public static class UnityHelpers
{
    public static int DepthChecked = 0;
    public static Transform RecursiveFind(this Transform transform, string search, bool caseInsensitive = true)
    {
        return CheckChildren(transform, search, caseInsensitive);
    }

    private static Transform CheckChildren(this Transform transform, string search, bool caseInsensitive = true)
    {
        Transform transformFound = null;
        if (IsEqual(transform.name, search, caseInsensitive))
        {
            transformFound = transform;
        }

        if (transform.childCount > 0 && transformFound == null)
        {
            DepthChecked++;
            foreach (Transform child in transform)
            {
                if (transformFound != null) continue;
                transformFound = CheckChildren(child, search, caseInsensitive);
            }
        }
        return transformFound;
    }

    public static bool IsEqual(string name, string otherName, bool caseInsensitive = true)
    {
        return caseInsensitive ? name.ToLower().Equals(otherName.ToLower()) : name.Equals(otherName);
    }
}