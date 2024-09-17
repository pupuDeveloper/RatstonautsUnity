using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragDetection : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private slideRooms _slideRooms;
    private GameObject whereDragStarted;
    DraggedDirection draggedDir = DraggedDirection.None;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            whereDragStarted = eventData.pointerCurrentRaycast.gameObject;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        float dragDistance = Vector3.Distance(eventData.position, eventData.pressPosition);

        if (whereDragStarted != null && whereDragStarted.tag == "roomEdge")
        {
            draggedDir = GetDragDirection(dragVectorDirection);
            _slideRooms.slide(draggedDir, GetDragDistance(dragDistance));
            draggedDir = DraggedDirection.None;
        }
    }
    public enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left,
        None
    }
    private float GetDragDistance(float distance)
    {
        return distance;
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        return draggedDir;
    }
}
