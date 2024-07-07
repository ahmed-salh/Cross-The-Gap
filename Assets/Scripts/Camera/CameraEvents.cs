using UnityEngine;
using UnityEngine.EventSystems;

namespace Craft2D 
{
    public class CameraEvents : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private CameraController _controller;

        private Vector2 _lastScreenPoint;

        private Touch firstTouch, secondTouch;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.touchCount == 2) 
            {
                firstTouch = Input.GetTouch(0);
                secondTouch = Input.GetTouch(1);

                _lastScreenPoint = 0.5f * (firstTouch.position + secondTouch.position);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.touchCount == 2)
            {
                firstTouch = Input.GetTouch(0);
                secondTouch = Input.GetTouch(1);

                Vector3 newScreenPoint = 0.5f * (firstTouch.position + secondTouch.position);

                _controller.MoveCamera(_lastScreenPoint, newScreenPoint);

                _controller.ClampPosition();

                _lastScreenPoint = 0.5f * (firstTouch.position + secondTouch.position);
            }
        }
    }
}
