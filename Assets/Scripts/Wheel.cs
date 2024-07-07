using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Craft2D 
{
    public class Wheel : MonoBehaviour
    {
        public bool runTime;

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

