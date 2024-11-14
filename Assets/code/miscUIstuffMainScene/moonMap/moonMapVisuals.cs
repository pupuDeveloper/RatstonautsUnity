using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonMapVisuals : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform[] moons;
    private int nextMoon = 0; //TODO: read from file

    Vector3 startPosition;

    GameObject currentLineObject;

    LineRenderer currentLineRenderer;

    public float lineThickness;

    private void Start()
    {
        //rocket.transform.LookAt(moons[nextMoon], Vector3.forward);
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