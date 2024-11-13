using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roomMilestones : MonoBehaviour
{
    private GameObject _gridcontent;
    private GameObject[] _gridItems;

    [Tooltip("milestone array")]
    public milestoneInfo[] _milestoneArray;

    private void Start()
    {
        _gridcontent = transform.Find("scrollableList").transform.Find("GridContent").gameObject;
        _gridItems = new GameObject[_gridcontent.GetComponentsInChildren<scaleLayoutItems>().Length];
        int i = 0;
        foreach (var item in _gridcontent.GetComponentsInChildren<scaleLayoutItems>())
        {
            if (item.transform == this.transform)
            {
                continue;
            }
            _gridItems[i] = item.transform.gameObject;
            i++;
        }
        scaleItems();
        setInfo();
    }

    private void scaleItems()
    {
        foreach (GameObject item in _gridItems)
        {
            scaleLayoutItems _scaleScript = item.transform.GetComponent<scaleLayoutItems>();
            _scaleScript.Scale(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height/8);
        }
    }
    private void setInfo()
    {
        for (int i = 0; i < _milestoneArray.Length; i++)
        {
            _gridItems[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _milestoneArray[i].lvl.ToString();
            _gridItems[i].transform.GetChild(1).GetComponent<TMP_Text>().text = _milestoneArray[1].description;
        }
    }
}
