using UnityEngine;
using UnityEngine.EventSystems;


namespace Craft2D 
{
    public class CraftButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject item;

        [SerializeField] private Transform itemParent;

        private bool _startedDragging;

        public void OnPointerDown(PointerEventData eventData)
        {
            _startedDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _startedDragging = false;
            CraftManager.Instance.PlaceItem();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_startedDragging) 
            {
                CraftManager.Instance.TempItem(item, eventData.position, itemParent);
                _startedDragging = false;
            }
        }
    }
}