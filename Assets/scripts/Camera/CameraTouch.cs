using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTouch : MonoBehaviour,
    IDragHandler
{
    [SerializeField] private CameraFollow cameraFollow;

    public void OnDrag(PointerEventData eventData)
    {
        cameraFollow.RotateCamera(eventData.delta);
    }
}