using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbuffer : MonoBehaviour
{

    private Rect lastSafeArea;
    [SerializeField] private RectTransform rectToFollow;

    private void Update()
    {
        if ( lastSafeArea != Screen.safeArea )
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea() {
        Rect safeAreaRect = Screen.safeArea;

        float scaleRatio = rectToFollow.rect.width / Screen.width;

        var left = safeAreaRect.xMin * scaleRatio;
        var right = -( Screen.width - safeAreaRect.xMax ) * scaleRatio;
        var top = -safeAreaRect.yMin * scaleRatio;
        var bottom = ( Screen.height - safeAreaRect.yMax ) * scaleRatio;

        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = new Vector2( left, bottom );
        rect.offsetMax = new Vector2( right, top );

        lastSafeArea = Screen.safeArea;
    }
}
