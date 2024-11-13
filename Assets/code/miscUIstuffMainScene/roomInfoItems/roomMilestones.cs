using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roomMilestones : MonoBehaviour
{
    private Transform[] gridItems;

    [Tooltip("milestone array")]
    public milestoneInfo[] _milestoneArray;
    private void Start()
    {
        gridItems = transform.GetChild(0).transform.GetChild(0).GetComponentsInChildren<Transform>();
        _milestoneArray = transform.GetChild(0).transform.GetChild(0).GetComponentsInChildren<milestoneInfo>();
    }
    private void scaleItems()
    {
        foreach (Transform item in gridItems)
        {
            item.GetComponent<scaleLayoutItems>().Scale(item.transform.parent.GetComponent<RectTransform>().rect.width, item.parent.GetComponent<RectTransform>().rect.height/8);
        }
    }
    private void setInfo()
    {
        for (int i = 0; i < _milestoneArray.Length; i++)
        {
            gridItems[i].GetChild(0).GetComponent<TMP_Text>().text = _milestoneArray[i].lvl.ToString();
            gridItems[i].GetChild(1).GetComponent<TMP_Text>().text = _milestoneArray[1].description;
        }
    }
}
