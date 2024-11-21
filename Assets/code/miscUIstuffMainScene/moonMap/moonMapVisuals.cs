using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonMapVisuals : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform[] moons;
    [SerializeField] private GameObject moonMapBG;
    private GameObject canvas;
    private int nextMoon = 0; //TODO: read from file
    private Vector3 startPosition;
    private GameObject currentLineObject;
    private LineRenderer currentLineRenderer;
    public float lineThickness;
    private void Start()
    {
        //rocket.transform.LookAt(moons[nextMoon], Vector3.forward);
        canvas = GameObject.Find("UI");
        moonMapBG.GetComponent<RectTransform>().sizeDelta = new Vector2 (canvas.GetComponent<RectTransform>().rect.width * 3, canvas.GetComponent<RectTransform>().rect.height);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void StartDrawingLine(Vector3 moonPos)
    {
        currentLineObject = GameObject.Find("ObjectPool/line");
        startPosition = moonPos;
        currentLineObject.transform.position = startPosition;
        currentLineRenderer = currentLineObject.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = lineThickness;
        currentLineRenderer.endWidth = lineThickness;
        PreviewLine();
    }

    private void PreviewLine() 
    {
        currentLineRenderer.SetPositions(new Vector3[]{ startPosition, rocket.transform.position});
    }
}