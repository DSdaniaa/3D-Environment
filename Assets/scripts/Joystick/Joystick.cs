using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform handle;

    private RectTransform background;

    public Vector2 Input { get; private set; }

    private void Awake()
    {
        background = GetComponent<RectTransform>();

        // Hide joystick at the beginning
        background.gameObject.SetActive(false);
    }

    public void StartJoystick(PointerEventData eventData)
    {
        // Show joystick
        background.gameObject.SetActive(true);

        // Put joystick where the mouse/finger touched
        background.position = eventData.position;

        // Reset handle to center
        handle.anchoredPosition = Vector2.zero;

        DragJoystick(eventData);
    }

    public void DragJoystick(PointerEventData eventData)
    {
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        position.x /= background.sizeDelta.x / 2f;
        position.y /= background.sizeDelta.y / 2f;

        Input = Vector2.ClampMagnitude(position, 1f);

        handle.anchoredPosition =
            Input * (background.sizeDelta.x / 2f);
    }

    public void StopJoystick()
    {
        Input = Vector2.zero;

        handle.anchoredPosition = Vector2.zero;

        // Hide joystick
        background.gameObject.SetActive(false);
    }
}