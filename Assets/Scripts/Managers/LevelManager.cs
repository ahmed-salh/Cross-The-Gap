using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Craft2D
{
    public class LevelManager : MonoBehaviour
    {
        public static Dictionary<Vector2, Point> ActivePoints = new Dictionary<Vector2, Point>();

        [SerializeField] private Transform _pointsParent;
        [SerializeField] private Transform _linksParent;
        [SerializeField] private Transform _wheelsParent;

        [SerializeField] private GameObject craftTools;

        private bool canDrive;





        private void Awake()
        {
            ClearActivePoints();
        }

        private void Update()
        {
            if (canDrive)
                Drive();
        }

        public void ClearActivePoints() 
        {
            ActivePoints.Clear();

            if (_pointsParent.childCount != 0)
            {
                for (int i = 0; i < _pointsParent.childCount; i++)
                {
                    Destroy(_pointsParent.GetChild(i).gameObject);
                }
            } 
            
            if (_linksParent.childCount != 0)
            {
                for (int i = 0; i < _linksParent.childCount; i++)
                {
                    Destroy(_linksParent.GetChild(i).gameObject);
                }
            }
            
            if (_wheelsParent.childCount != 0)
            {
                for (int i = 0; i < _wheelsParent.childCount; i++)
                {
                    Destroy(_wheelsParent.GetChild(i).gameObject);
                }
            }

                      
        }

        public void Simulate() 
        {
            craftTools.SetActive(false);

            _pointsParent.localScale *= .5f;
            _linksParent.localScale *= .5f;
            _wheelsParent.localScale *= .5f;

            if (_pointsParent.childCount != 0)
            {
                for (int i = 0; i < _pointsParent.childCount; i++)
                {
                    _pointsParent.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    _pointsParent.GetChild(i).transform.position += Vector3.left * 10;
                }
            }

            if (_linksParent.childCount != 0)
            {
                for (int i = 0; i < _linksParent.childCount; i++)
                {
                    _linksParent.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    _linksParent.GetChild(i).transform.position += Vector3.left * 10;

                }
            }

            if (_wheelsParent.childCount != 0)
            {
                for (int i = 0; i < _wheelsParent.childCount; i++)
                {
                    _wheelsParent.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    _wheelsParent.GetChild(i).transform.position += Vector3.left * 10;

                }
            }



            ActivateControl();
        }

        public void Design()
        {
            craftTools.SetActive(true);

            ClearActivePoints();

            _pointsParent.localScale *= 2;
            _linksParent.localScale *= 2;
            _wheelsParent.localScale *= 2;

            canDrive = false;

        }


        public void ActivateControl() 
        {

            if (_wheelsParent.childCount != 0)
            {
                for (int i = 0; i < _wheelsParent.childCount; i++)
                {

                    _wheelsParent.GetChild(i).gameObject.GetComponent<HingeJoint2D>().useMotor = true;
                    
                    canDrive = true;
                }
            }
        }

        private void Drive() {

            if (_wheelsParent.childCount != 0)
            {
                for (int i = 0; i < _wheelsParent.childCount; i++)
                {

                    JointMotor2D motor = _wheelsParent.GetChild(i).gameObject.GetComponent<HingeJoint2D>().motor;
                    
                    motor.motorSpeed = 500 * Input.GetAxis("Horizontal");
                    
                    _wheelsParent.GetChild(i).gameObject.GetComponent<HingeJoint2D>().motor = motor;

                }
            }
        }


        public void SaveLevel() { }

        public void LoadLevel() { }

    }
}
