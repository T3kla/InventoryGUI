using UnityEngine;
using UnityEngine.EventSystems;


public class dragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform target;
    public bool shouldReturn;
    private bool isMouseDown;
    private Vector3 startMousePosition;
    private Vector3 startPosition;
    private void Update()
    {
        if (isMouseDown)
        {
            var currentPosition = Input.mousePosition;

            var diff = currentPosition - startMousePosition;

            var pos = startPosition + diff;

            target.position = pos;
        }
    }

    public void OnPointerDown(PointerEventData dt)
    {
        isMouseDown = true;
        Debug.Log("Draggable Mouse Down");

        startPosition = target.position;
        startMousePosition = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData dt)
    {
        Debug.Log("Draggable mouse up");

        isMouseDown = false;
        if (shouldReturn) target.position = startPosition;
    }
}
