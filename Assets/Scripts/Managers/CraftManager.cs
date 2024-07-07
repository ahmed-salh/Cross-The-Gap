using UnityEngine;

namespace Craft2D 
{
    // Responsiple for spawn items and structure
    public class CraftManager : MonoBehaviour
    {
        private Camera _camera;

        private static CraftManager _instance;

        public static CraftManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CraftManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject(nameof(CraftManager)).AddComponent<CraftManager>();
                    }

                    DontDestroyOnLoad(_instance);
                    _instance.Init();
                }

                return _instance;
            }
        }

        private bool _isDragging;

        private GameObject _currentSelection;

        private void Init()
        {
            _camera = Camera.main;
        }

        public void TempItem(GameObject item, Vector2 screenPosition, Transform parent)
        {
            Vector2 worldPoint = _camera.ScreenToWorldPoint(screenPosition);

            _currentSelection = Instantiate(item, worldPoint, Quaternion.identity, parent);

            _isDragging = true;
        }

        public void PlaceItem() 
        {
            if (_currentSelection == null)
                return;

            _isDragging = false;
            _currentSelection.transform.position = (Vector2)Vector2Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition));


            Point fixedPoint;

            if (!LevelManager.ActivePoints.TryGetValue(_currentSelection.transform.position, out fixedPoint))
            {
                Destroy(_currentSelection);

            }
            else 
            {
                HingeJoint2D joint = _currentSelection.gameObject.GetComponent<HingeJoint2D>();
                joint.connectedBody = fixedPoint.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        private void Update()
        {
            if (_isDragging && _currentSelection != null) 
            {
                Vector2 currentPosition = _currentSelection.transform.position;
                Vector2 newPosition = (Vector2)Vector2Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition));

                _currentSelection.transform.position = Vector2.Lerp(currentPosition, newPosition, 30f * Time.deltaTime);
            }
        }
    }
}
