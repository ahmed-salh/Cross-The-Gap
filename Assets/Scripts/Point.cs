using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Craft2D 
{
    [ExecuteInEditMode]
    public class Point : MonoBehaviour
    {
        public bool runTime;
        public List<Link> connectedBars;
        public Vector2 pointID;

        private void Start()
        {
            if (!runTime)
            {
                pointID = transform.position;
                if (!LevelManager.ActivePoints.ContainsKey(pointID))
                    LevelManager.ActivePoints.Add(pointID, this);
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (!runTime)
            {
                if (transform.hasChanged)
                {
                    transform.hasChanged = false;
                    transform.position = Vector3Int.RoundToInt(transform.position);
                }
            }

        }
    }
}

