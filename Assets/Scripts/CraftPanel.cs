using UnityEngine;
using UnityEngine.EventSystems;

namespace Craft2D 
{
    // handle the events on the craft panel
    public class CraftPanel : MonoBehaviour, IPointerDownHandler
    {
        private bool _isDragging;

        private bool _editMode;

        public bool EditMode 
        {
            get { return _editMode; }

            set { _editMode = value; }
        }

        public void OnPointerDown(PointerEventData eventData)
        {


        }

    }
}