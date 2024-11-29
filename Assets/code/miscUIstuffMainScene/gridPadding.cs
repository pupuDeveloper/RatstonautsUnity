using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gridPadding : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject topBarImage;
    [SerializeField] private GameObject BottomBarImage;
    int topPadding;
    private void Start()
    {
        topPadding = (int)topBarImage.GetComponent<RectTransform>().rect.height;
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        Debug.Log(topPadding);
        layoutGroup.padding = new RectOffset(0,0,topPadding,0);
    }
}
