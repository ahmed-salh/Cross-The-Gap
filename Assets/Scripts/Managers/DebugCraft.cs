using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Craft2D.Debug {

    public class DebugCraft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _barCreationStarted = false;
        private Camera _camera;

        private bool _isDragging = false;

        [SerializeField] private Button _button;

        [SerializeField] private Link _currentBar;
        [SerializeField] private GameObject _wheelToInstantiate;
        [SerializeField] private GameObject _barToInstantiate;
        [SerializeField] private Transform _barParent;
        [SerializeField] private Transform _wheelParent;
        [SerializeField] private Point _currentStartPoint;
        [SerializeField] private Wheel _currentWheel;
        [SerializeField] private Point _currentEndPoint;
        [SerializeField] private GameObject _pointToInstantiate;
        [SerializeField] private Transform _pointParent;

        private void Awake()
        {
            _camera= Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (_barCreationStarted)
            {
                Vector2 position = Vector2Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition));
                _currentEndPoint.transform.position = position;
                _currentEndPoint.pointID = _currentEndPoint.transform.position;
                _currentBar.UpdateBarTransform(_currentEndPoint.transform.position);
            }

            if (_isDragging)
            {
                _currentWheel.transform.position = (Vector2)Vector2Int.RoundToInt(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.touchCount > 1)
                return;

            if (!_barCreationStarted)
            {
                _barCreationStarted = true;
                StartBarCreation(Vector2Int.RoundToInt(_camera.ScreenToWorldPoint(eventData.position)));
            }
            //else
            //{
            //    if (eventData.button == PointerEventData.InputButton.Left)
            //    {
            //        FinishBarCreation();
            //    }
            //    else
            //    {
            //        if (eventData.button == PointerEventData.InputButton.Right)
            //        {
            //            _barCreationStarted = false;
            //            DeleteCurrentBar();
            //        }
            //    }
            //}
        }

        public void SpawnWheel()
        {
            Vector3 position = new Vector3(_button.transform.position.x, _button.transform.position.y, 0);
            _currentWheel = Instantiate(_wheelToInstantiate, position, Quaternion.identity, _wheelParent).GetComponent<Wheel>();
            _isDragging = true;

        }

        #region Methods
        public void StartBarCreation(Vector2 startPosition)
        {
            _currentBar = Instantiate(_barToInstantiate, _barParent).GetComponent<Link>();
            _currentBar.startPosition = startPosition;

            if (LevelManager.ActivePoints.ContainsKey(startPosition))
            {
                _currentStartPoint = LevelManager.ActivePoints[startPosition];
            }
            else
            {
                _currentStartPoint = Instantiate(_pointToInstantiate, startPosition, Quaternion.identity, _pointParent).GetComponent<Point>();
                LevelManager.ActivePoints.Add(startPosition, _currentStartPoint);
            }

            _currentEndPoint = Instantiate(_pointToInstantiate, startPosition, Quaternion.identity, _pointParent).GetComponent<Point>();
        }

        private void DeleteCurrentBar()
        {
            Destroy(_currentBar.gameObject);

            if (_currentStartPoint.connectedBars.Count == 0 && _currentStartPoint.runTime)
                Destroy(_currentStartPoint.gameObject);

            if (_currentEndPoint.connectedBars.Count == 0 && _currentEndPoint.runTime)
                Destroy(_currentEndPoint.gameObject);
        }

        private void FinishBarCreation()
        {
            if (LevelManager.ActivePoints.ContainsKey(_currentEndPoint.transform.position))
            {
                Destroy(_currentEndPoint.gameObject);
                _currentEndPoint = LevelManager.ActivePoints[_currentEndPoint.transform.position];
            }
            else
            {
                LevelManager.ActivePoints.Add(_currentEndPoint.transform.position, _currentEndPoint);
            }

            _currentStartPoint.connectedBars.Add(_currentBar);
            _currentEndPoint.connectedBars.Add(_currentBar);

            BoxCollider2D collider = _currentBar.GetComponent<BoxCollider2D>();
            FixedJoint2D[] joints = _currentBar.GetComponents<FixedJoint2D>();

            collider.size = _currentBar.barSpriteRenderer.size;
            joints[0].connectedBody = _currentStartPoint.GetComponent<Rigidbody2D>();
            joints[1].connectedBody = _currentEndPoint.GetComponent<Rigidbody2D>();



            //StartBarCreation(_currentEndPoint.transform.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            FinishBarCreation();
            _barCreationStarted = false;

        }
        #endregion
    }
}
