using UnityEngine;


namespace Craft2D 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector2 xBoundary;
        [SerializeField] private Vector2 yBoundary;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void MoveCamera(Vector2 lastScreenPoint, Vector2 screenPoint)
        {
            Vector2 lastWolrdPoint = _camera.ScreenToWorldPoint(lastScreenPoint);

            Vector2 wolrdPoint = _camera.ScreenToWorldPoint(screenPoint);

            Vector3 movement = lastWolrdPoint - wolrdPoint;

            _camera.transform.position += movement;
        }

        public void ClampPosition() 
        {
            _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, xBoundary.x, xBoundary.y),
                Mathf.Clamp(_camera.transform.position.y, yBoundary.x, yBoundary.y),
                _camera.transform.position.z);
        }
    }
}

