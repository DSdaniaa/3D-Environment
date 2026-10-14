using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchArea : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [SerializeField] private Joystick joystick;

    public void OnPointerDown(PointerEventData eventData)
    {
        joystick.StartJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystick.DragJoystick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.StopJoystick();
    }
}